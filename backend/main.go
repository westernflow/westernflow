package main

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"time"

	"github.com/moesif/moesifmiddleware-go"
	"github.com/spf13/viper"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"

	"github.com/gin-gonic/gin"
	"github.com/go-redis/redis/v8"

	limiter "github.com/ulule/limiter/v3"
	mgin "github.com/ulule/limiter/v3/drivers/middleware/gin"
	memory "github.com/ulule/limiter/v3/drivers/store/memory"

	"net/url"
	"uwo-tt-api/controller"
)

func wrapHandlerMoesif(f http.HandlerFunc, s map[string]interface{}) gin.HandlerFunc {
	return gin.WrapH(moesifmiddleware.MoesifMiddleware(http.HandlerFunc(f), s))
}

func getPort() string {
	// value, ok when needed
	val, ok := viper.Get("PORT").(string)

	if !ok {
		log.Printf("Port does not exist or has invalid type")
		val = "8080" // Default value
	}

	return ":" + val
}

func getMoesifOptions() map[string]interface{} {

	// value, ok when needed
	val, ok := viper.Get("MOESIF_SECRET_ID").(string)

	if !ok {
		log.Printf("Moesif key does not exist or has invalid type")
		val = ""
	}

	var moesifOptions = map[string]interface{}{
		"Application_Id": val,
		// Set to false if you don't want to capture req/resp body
		"Log_Body": true,
	}

	return moesifOptions
}

// TODO: Could use a struct to hold config information...
func loadConfig() {
	// Load environment configuration
	viper.SetConfigFile(".env")
	err := viper.ReadInConfig()

	if err != nil {
		log.Println("Error in config found!")
		// If loading .env file fails, use variables sourced into the environment
		log.Printf("Error reading config file %s. Using environment variables instead.", err)
	}

	viper.AutomaticEnv()
}

func getRedisClient() *redis.Client {
	// connect to redis
	redisURL, redisOK := viper.Get("REDIS_URL").(string)
	redisPassword, redisPasswordOK := viper.Get("REDIS_PASSWORD").(string)

	if !redisOK {
		log.Fatalf("Redis url not found")
	}

	if !redisPasswordOK {
		log.Fatalf("Redis password not found")
	}

	u, err := url.Parse(redisURL)
	if err != nil {
		panic(err)
	}

	rdb := redis.NewClient(&redis.Options{
		Addr:     u.Host,
		Password: redisPassword, // use the password
		DB:       0,             // use default DB
	})

	// check connection
	_, err = rdb.Ping(context.Background()).Result()

	if err != nil {
		log.Fatal(err)
	}

	return rdb
}

func getMongoClient() *mongo.Client {
	mode, modeOK := viper.Get("GIN_MODE").(string)
	localDB, localOK := viper.Get("LOCAL_MONGODB").(string)
	remoteDB, remoteOK := viper.Get("PROD_MONGODB").(string)

	dbURL := ""
	if modeOK && mode == "release" {
		fmt.Println("Production")

		if remoteOK {
			dbURL = remoteDB
		} else {
			log.Fatalf("Production database url not found")
		}

	} else {
		fmt.Println("Local")

		if localOK {
			dbURL = localDB
		} else {
			dbURL = "mongodb://mongodb:27017"
			log.Printf("Local database url not found, using %v", dbURL)
		}
	}

	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()

	client, err := mongo.Connect(ctx, options.Client().ApplyURI(dbURL))

	if err != nil {
		log.Fatal(err)
	}

	if err != nil {
		log.Fatal(err)
	}

	// Check the connection
	err = client.Ping(context.TODO(), nil)

	if err != nil {
		log.Fatal(err)
	}

	fmt.Println("Connected to MongoDB!")
	return client
}

// @title Unofficial UWO Timetable API
// @version 1.0
// @description This is an API based on UWO's most update time table for undergraduate courses. Search options and course data is scraped from https://studentservices.uwo.ca/secure/timetables/mastertt/ttindex.cfm and stored in a database to avoid overloading the website with scrape requests. Data is scraped daily to ensure data is up-to-date.

// @contact.name API Support
// @contact.url http://www.github.com/cbaron3/uwo-tt-api

// @host http://uwottapi.ca
// @BasePath /api/v1/
func main() {
	fmt.Println("Here")
	loadConfig()

	fmt.Println("Config loaded")

	client := getMongoClient()

	db := client.Database("uwo-tt-api")
	redisClient := getRedisClient()
	// Start a scheduler with worker task
	// s1 := gocron.NewScheduler(time.UTC)
	// s1.Every(1).Day().StartImmediately().Do(worker.ScrapeTimeTable, db)
	// s1.StartAsync()

	// Endpoint router
	router := gin.Default()

	// Define controller instance for endpoints
	c := controller.NewController()
	c.DB = db
	c.Redis = redisClient
	c.CreateTrie()

	// Get moesif configuration
	moesifOptions := getMoesifOptions()

	// Define a limit rate to 4 requests per hour.
	rate, err := limiter.NewRateFromFormatted("480-H")
	if err != nil {
		log.Fatal(err)
		return
	}

	store := memory.NewStore()

	// Create a new middleware with the limiter instance.
	middleware := mgin.NewMiddleware(limiter.New(store, rate))

	router.Use(middleware)

	// Swagger documentation
	router.NoRoute(func(c *gin.Context) {
		c.Redirect(http.StatusMovedPermanently, "/routeNotFound")
		c.Abort()
	})

	// API group
	api := router.Group("/api/v1")
	{
		// Option endpoints
		api.GET("/subjects", wrapHandlerMoesif(c.ListSubjects, moesifOptions))
		api.GET("/suffixes", wrapHandlerMoesif(c.ListSuffixes, moesifOptions))
		api.GET("/delivery_types", wrapHandlerMoesif(c.ListDeliveryTypes, moesifOptions))
		api.GET("/components", wrapHandlerMoesif(c.ListComponents, moesifOptions))
		api.GET("/start_times", wrapHandlerMoesif(c.ListStartTimes, moesifOptions))
		api.GET("/end_times", wrapHandlerMoesif(c.ListEndTimes, moesifOptions))
		api.GET("/campuses", wrapHandlerMoesif(c.ListCampuses, moesifOptions))

		// Course data endpoint
		api.GET("/courses", wrapHandlerMoesif(c.ListCourses, moesifOptions))
		api.GET("/sections", wrapHandlerMoesif(c.ListSections, moesifOptions))
		api.GET("/indexed_search_data", wrapHandlerMoesif(c.ListIndexedSearchData, moesifOptions))

		// Professor data endpoint to accept routes for /professors/:professorname
		api.GET("/professors/:profName", wrapHandlerMoesif(c.ListProfessors, moesifOptions))
		api.GET("/search/:query", wrapHandlerMoesif(c.Search, moesifOptions))
	}

	// port := getPort()
	router.Run(":8080")
}

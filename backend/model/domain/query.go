package model

import "fmt"

type TeacherSearchResults struct {
	Data struct {
		School struct {
			ID   string `json:"id"`
			Name string `json:"name"`
		} `json:"school"`
		Search struct {
			Teachers struct {
				Edges []struct {
					Cursor string `json:"cursor"`
					Node   struct {
						FirstName string `json:"firstName"`
						ID        string `json:"id"`
						LastName  string `json:"lastName"`
						Ratings   struct {
							Edges []struct {
								Node struct {
									ClarityRating    int    `json:"clarityRating"`
									Comment          string `json:"comment"`
									Date             string `json:"date"`
									DifficultyRating int    `json:"difficultyRating"`
									HelpfulRating    int    `json:"helpfulRating"`
									ID               string `json:"id"`
									QualityRating    int    `json:"qualityRating"`
								} `json:"node"`
							} `json:"edges"`
						} `json:"ratings"`
					} `json:"node"`
				} `json:"edges"`
			} `json:"teachers"`
		} `json:"search"`
	} `json:"data"`
}

func GetAllProfsQuery(first int, after string) string {
	payload := fmt.Sprintf(`{"query":"query TeacherSearchResultsPageQuery($query: TeacherSearchQuery! $schoolID: ID $first: Int $after: String) { search: newSearch { ...TeacherSearchPagination_search_1ZLmLD } school: node(id: $schoolID) { ... on School { name } id } } fragment TeacherSearchPagination_search_1ZLmLD on newSearch { teachers(query: $query, first: $first, after: $after) { edges { cursor node { ...TeacherCard_teacher id ratings { edges { node { qualityRating difficultyRating date comment helpfulRating clarityRating id } } } } } } } fragment TeacherCard_teacher on Teacher { id ...CardName_teacher } fragment CardName_teacher on Teacher { firstName lastName }","variables":{"query":{"text":"","schoolID":"U2Nob29sLTE0OTE=","fallback":false},"schoolID":"U2Nob29sLTE0OTE=","first":%d,"after":"%s"}}`, first, after)
	return payload
}

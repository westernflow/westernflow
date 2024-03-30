import psycopg2


def main():
    conn_params = {
        # "CourseManagerDbConnection" : "Host=localhost;Port=5432;Username=postgres;Password=example;Database=postgres;Include Error Detail=true"
        'dbname': 'postgres',
        'user': 'postgres',
        'password': 'example',
        'host': 'localhost'
    }

    conn = psycopg2.connect(**conn_params)
    cur = conn.cursor()
    # ping the database
    cur.execute("SELECT 1")

    # show available tables
    cur.execute("SELECT table_name FROM information_schema.tables WHERE table_schema='public';")
    print(cur.fetchall())

    # select all courses in Faculties table
    cur.execute('SELECT * FROM public."Faculties"')
    print(cur.fetchall())

if __name__ == "__main__":
    main()
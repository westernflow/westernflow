#!/bin/bash

# Database connection details
DB_HOST="localhost"
DB_PORT="5432"
DB_USER="postgres"
DB_PASS="example"
DB_NAME="postgres"

# SQL command to delete rows from tables
SQL_COMMAND="
DELETE FROM \"TimingDetails\";

DELETE FROM \"ProfessorSection\";
DELETE FROM \"ProfessorReviewReviewer\";
DELETE FROM \"CourseReviewReviewer\";

DELETE FROM \"ProfessorReviews\";
DELETE FROM \"CourseReviews\";

DELETE FROM \"Professors\";
DELETE FROM \"Reviewers\";

DELETE FROM \"Sections\";
DELETE FROM \"CourseOfferings\";
DELETE FROM \"Courses\";

DELETE FROM \"Faculties\";"

# Execute SQL command
PGPASSWORD=$DB_PASS psql -h $DB_HOST -p $DB_PORT -U $DB_USER -d $DB_NAME -c "$SQL_COMMAND"
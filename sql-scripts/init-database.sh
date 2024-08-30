#!/bin/bash

# Wait for SQL Server to start
echo "Waiting for SQL Server to be available..."

# Run the SQL script to create the database
until /opt/mssql-tools/bin/sqlcmd -S localhost  -U sa -P S3cur3P@ssW0rd! -t 60  -Q "SELECT 1" &> /dev/null

do
  echo "SQL Server is unavailable - sleeping"
  sleep 10
done

echo "SQL Server is up - executing script"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P S3cur3P@ssW0rd! -t 60 -Q "CREATE DATABASE myDatabase1"



# Video.API

## Setup

1. Install PostgreSQL
2. Create the database and the user (instruction bellow)
3. Run API service
4. Send HTTP request to create user

## Create the database and the user

```postgresql
create database mydb;

DO $$
BEGIN
create user nick with encrypted password 'chapsas';
EXCEPTION WHEN duplicate_object THEN RAISE NOTICE '%, skipping', SQLERRM USING ERRCODE = SQLSTATE;
END
$$;

grant all privileges on database mydb to nick;
```

## HTTP request to create user

```http request
POST https://localhost:5001/customers HTTP/1.1
content-type: application/json

{
    "fullname": "Nick Chapsas",
    "email": "nick@nickchapsas.com",
    "gitHubUsername": "Elfocrash",
    "dateOfBirth": "1993-04-20"
}
```

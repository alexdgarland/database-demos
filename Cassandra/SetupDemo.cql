
DROP KEYSPACE IF EXISTS DatabaseDemos;

CREATE KEYSPACE DatabaseDemos
    WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };

    
DROP TABLE IF EXISTS DatabaseDemos.Persons;

CREATE TABLE DatabaseDemos.Persons
            (
             PersonID int PRIMARY KEY,
             FirstName varchar,
             LastName varchar
            );

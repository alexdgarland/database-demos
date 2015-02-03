CREATE KEYSPACE IF NOT EXISTS DatabaseDemos
    WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };

DROP TABLE IF EXISTS DatabaseDemos.Persons;

CREATE TABLE DatabaseDemos.Persons
            (
             PersonID int PRIMARY KEY,
             FirstName varchar,
             LastName varchar
            );

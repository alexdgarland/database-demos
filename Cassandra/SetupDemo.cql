CREATE KEYSPACE IF NOT EXISTS DatabaseDemos
  WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };
  
CREATE SCHEMA IF NOT EXISTS DemoSchema
    WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };

DROP TABLE IF EXISTS DemoSchema.Persons;

CREATE TABLE DemoSchema.Persons
            (
             PersonID int PRIMARY KEY,
             FirstName varchar,
             LastName varchar
            );


DROP KEYSPACE IF EXISTS DatabaseDemos;

CREATE KEYSPACE DatabaseDemos
    WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };

    
DROP TABLE IF EXISTS DatabaseDemos.Customers;

CREATE TABLE DatabaseDemos.Customers
            (
             CustomerID int PRIMARY KEY,
             Name       varchar,
             AddressSet set<varchar>
            );

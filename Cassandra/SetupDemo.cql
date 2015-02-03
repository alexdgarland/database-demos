CREATE KEYSPACE DatabaseDemos
  WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };

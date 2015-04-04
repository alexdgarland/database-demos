
using MongoDB.Driver;

namespace MongoDBDemo
{

    class DemoSetup
    {
        private static MongoDatabase _theDatabase;

        public static MongoDatabase GetDatabase()
        {
            if (_theDatabase == null)
            {
                // Set up connection
                var connectionString = "mongodb://localhost";
                var client = new MongoClient(connectionString);
                var server = client.GetServer();
                _theDatabase = server.GetDatabase(databaseName: "test");
            }

            return _theDatabase;
        }

    }
}

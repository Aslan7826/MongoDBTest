using MongoDB.Driver;
using System;

namespace ConsoleApp1
{
    public class MongoTestConn
    {
        public void ConnTest()
        {
            try
            {
                string connectionString = $"mongodb://192.168.159.134:27016/?connectTimeoutMS=300&socketTimeoutMS=300&maxLifeTimeMS=300";
                var mongoClient = new MongoClient(connectionString);
                Console.WriteLine(mongoClient.Settings.ConnectTimeout);
                Console.WriteLine(mongoClient.Settings.SocketTimeout);
                var dbnames = mongoClient.ListDatabaseNames();
                int c = dbnames.ToList().Count;
                Console.Write(c);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            // var command = new JsonCommand<BsonDocument>("{new Date(): 1}");
            //var execute = db.RunCommand(command);
        }
    }
}
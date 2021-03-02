using MaxP.Arpro.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MySqlToMongoDB
{
    internal class MongoDBRepository
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<Event> _mongoCollection;

        internal MongoDBRepository()
        {
            // MongoDB 連線字串
            //string connectionString = "mongodb://192.168.60.52:27017";
            //"mongodb://192.168.159.128:27017";
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MongoContext"].ConnectionString;
            // 產生 MongoClient 物件
            _mongoClient = new MongoClient(connectionString);
            // 取得 MongoDatabase 物件
            _mongoDatabase = _mongoClient.GetDatabase("ArPro247");
            // 取得 Collection
            _mongoCollection = _mongoDatabase.GetCollection<Event>("Event");
        }

        internal void EntryEvent(IEnumerable<Event> eve)
        {
            _mongoCollection.InsertMany(eve);
        }
    }
}
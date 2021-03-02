using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
namespace ConsoleApp1
{
    public class UserDao
    {
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<user> _mongoCollection;

        public UserDao()
        {
            // MongoDB 連線字串
            string connectionString = "mongodb://192.168.159.132:27017";
            // 產生 MongoClient 物件
            _mongoClient = new MongoClient(connectionString);
            // 取得 MongoDatabase 物件
            _mongoDatabase = _mongoClient.GetDatabase("test");
            // 取得 Collection
            _mongoCollection = _mongoDatabase.GetCollection<user>("user");
        }

        public IQueryable<user> FindAll()
        {
           return _mongoCollection.AsQueryable();
        }
        public user GetById(string id) {
            return _mongoCollection.Find(c => c.Id.Equals(id)).FirstOrDefault();
        }
        public void Insert()
        {
            List<user> users = new List<user>();
            for (int i = 1; i < 10; i++)
            {
                users.Add(new user { Id = i.ToString(), Name = "p" + i, Price = i });
            }
            _mongoCollection.InsertMany(users);
        }
        public void Insert(string name)
        {
            string  id =FindAll().Count() != 0 ?(FindAll().Count() +1)+"":"1";
            _mongoCollection.InsertOne(new user() {  Id =id, Name = name , Price = 500 });
        }

        public bool Update(user u)
        {
            var filter = Builders<user>.Filter.Eq(s => s.Id, u.Id);
            var update = Builders<user>.Update.Set(s => s.Name, u.Name).Set(s=>s.Price,u.Price);
           return _mongoCollection.UpdateOne(filter, update).IsAcknowledged;
        }

        public bool Delete(string id) {
            var filter = Builders<user>.Filter.Eq(s => s.Id ,id);
            return _mongoCollection.DeleteOne(filter).IsAcknowledged;
        }

    }
}

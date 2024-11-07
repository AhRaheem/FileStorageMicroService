using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Persistence
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public IMongoCollection<FILE> Files => _database.GetCollection<FILE>("Files");
        public IMongoCollection<FileMetadata> Metadata => _database.GetCollection<FileMetadata>("Metadata");
    }
}

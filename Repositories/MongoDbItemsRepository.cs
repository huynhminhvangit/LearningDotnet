using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningDotnet.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LearningDotnet.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "learning";

        private const string collectionName = "items";

        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterDefinitionBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase mongoDatabase = mongoClient.GetDatabase(databaseName);
            itemsCollection = mongoDatabase.GetCollection<Item>(collectionName);
        }

        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid Id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, Id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid Id)
        {
            var filter = filterDefinitionBuilder.Eq(item => item.Id, Id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterDefinitionBuilder.Eq(e => e.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}
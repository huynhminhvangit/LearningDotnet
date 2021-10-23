using System;
using System.Collections.Generic;
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

    public void CreateItem(Item item)
    {
      itemsCollection.InsertOne(item);
    }

    public void DeleteItem(Guid Id)
    {
      var filter = filterDefinitionBuilder.Eq(item => item.Id, Id);
      itemsCollection.DeleteOne(filter);
    }

    public Item GetItem(Guid Id)
    {
      var filter = filterDefinitionBuilder.Eq(item => item.Id, Id);
      return itemsCollection.Find(filter).SingleOrDefault();
    }

    public IEnumerable<Item> GetItems()
    {
      return itemsCollection.Find(new BsonDocument()).ToList();
    }

    public void UpdateItem(Item item)
    {
      var filter = filterDefinitionBuilder.Eq(e => e.Id, item.Id);
      itemsCollection.ReplaceOne(filter, item);
    }
  }
}
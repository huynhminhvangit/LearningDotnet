using System;
using System.Collections.Generic;
using System.Linq;
using LearningDotnet.Entities;

namespace LearningDotnet.Repositories
{
  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {
      new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
      new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
    };

    public IEnumerable<Item> GetItems()
    {
      return items;
    }

    public Item GetItem(Guid Id)
    {
      return items.Where(item => item.Id == Id).SingleOrDefault();
    }

    public void CreateItem(Item item)
    {
      items.Add(item);
    }

    public void UpdateItem(Item item)
    {
      var index = items.FindIndex(e => e.Id == item.Id);
      items[index] = item;
    }

    public void DeleteItem(Guid Id)
    {
      var index = items.FindIndex(e => e.Id == Id);
      items.RemoveAt(index);
    }
  }
}
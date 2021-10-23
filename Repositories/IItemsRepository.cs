using System;
using System.Collections.Generic;
using LearningDotnet.Entities;

namespace LearningDotnet.Repositories
{
  public interface IItemsRepository
  {
    Item GetItem(Guid Id);
    IEnumerable<Item> GetItems();
    void CreateItem(Item item);
    void UpdateItem(Item item);
    void DeleteItem(Guid Id);
  }

}
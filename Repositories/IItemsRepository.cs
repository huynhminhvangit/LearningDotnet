using System;
using System.Collections.Generic;
using LearningDotnet.Entities;

namespace LearningDotnet.Repositories
{
    public interface IItemsRepository
  {
    Item GetItem(Guid Id);
    IEnumerable<Item> GetItems();
  }

}
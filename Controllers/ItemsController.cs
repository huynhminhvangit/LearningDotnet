using System.Collections.Generic;
using LearningDotnet.Repositories;
using Microsoft.AspNetCore.Mvc;
using LearningDotnet.Entities;
using System;

namespace LearningDotnet.Controllers
{
    // GET /items
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }

        // GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }

        // GET /items/{Id}
        [HttpGet("{Id}")]
        public ActionResult<Item> GetItem(Guid Id)
        {
            var item = repository.GetItem(Id);

            if(item is null) {
                return NotFound();
            }

            return item;
        }

    }
}
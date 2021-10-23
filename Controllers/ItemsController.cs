using System.Collections.Generic;
using LearningDotnet.Repositories;
using Microsoft.AspNetCore.Mvc;
using LearningDotnet.Entities;
using System;
using System.Linq;
using LearningDotnet.DTOs;

namespace LearningDotnet.Controllers
{
    // GET /items
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        // GET /items
        [HttpGet]
        public IEnumerable<ItemDTO> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDTO());
            return items;
        }

        // GET /items/{Id}
        [HttpGet("{Id}")]
        public ActionResult<ItemDTO> GetItem(Guid Id)
        {
            var item = repository.GetItem(Id);

            if(item is null) {
                return NotFound();
            }

            return item.AsDTO();
        }

    }
}
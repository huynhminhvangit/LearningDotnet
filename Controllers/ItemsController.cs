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

    // POST /items
    [HttpPost]
    public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDTO)
    {
        Item item = new ()
        {
            Id = Guid.NewGuid(),
            Name = itemDTO.Name,
            Price = itemDTO.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        repository.CreateItem(item);

        return CreatedAtAction(nameof(GetItem), new {Id = item.Id}, item.AsDTO());
    }

    // PUT /items/{Id}
    [HttpPut("{Id}")]
    public ActionResult UpdateItem(Guid Id, UpdateItemDTO itemDTO)
    {
        var existingItem = repository.GetItem(Id);

        if(existingItem is null)
        {
            return NotFound();
        }

        Item updateItem = existingItem with
        {
            Name = itemDTO.Name,
            Price = itemDTO.Price
        };

        repository.UpdateItem(updateItem);

        return NoContent();
    }

    // DELETE /items/{Id}
    [HttpDelete("{Id}")]
    public ActionResult DeleteItem(Guid Id)
    {
      var existingItem = repository.GetItem(Id);

      if(existingItem is null)
      {
        return NotFound();
      }

      repository.DeleteItem(Id);

      return NoContent();
    }

  }
}
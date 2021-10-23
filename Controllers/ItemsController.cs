using System.Collections.Generic;
using LearningDotnet.Repositories;
using Microsoft.AspNetCore.Mvc;
using LearningDotnet.Entities;
using System;
using System.Linq;
using LearningDotnet.DTOs;
using System.Threading.Tasks;

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
    public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
    {
        var items = (await repository.GetItemsAsync()).Select(item => item.AsDTO());
        return items;
    }

    // GET /items/{Id}
    [HttpGet("{Id}")]
    public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid Id)
    {
        var item = await repository.GetItemAsync(Id);

        if(item is null) {
            return NotFound();
        }

        return item.AsDTO();
    }

    // POST /items
    [HttpPost]
    public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO itemDTO)
    {
        Item item = new ()
        {
            Id = Guid.NewGuid(),
            Name = itemDTO.Name,
            Price = itemDTO.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await repository.CreateItemAsync(item);

        return CreatedAtAction(nameof(GetItemAsync), new {Id = item.Id}, item.AsDTO());
    }

    // PUT /items/{Id}
    [HttpPut("{Id}")]
    public async Task<ActionResult> UpdateItem(Guid Id, UpdateItemDTO itemDTO)
    {
        var existingItem = await repository.GetItemAsync(Id);

        if(existingItem is null)
        {
            return NotFound();
        }

        Item updateItem = existingItem with
        {
            Name = itemDTO.Name,
            Price = itemDTO.Price
        };

        await repository.UpdateItemAsync(updateItem);

        return NoContent();
    }

    // DELETE /items/{Id}
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteItemAsync(Guid Id)
    {
      var existingItem = await repository.GetItemAsync(Id);

      if(existingItem is null)
      {
        return NotFound();
      }

      await repository.DeleteItemAsync(Id);

      return NoContent();
    }

  }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static List<Item> _items = new List<Item>();
        private static int _idCounter = 1;

        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(_items);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            var item = _items.Find(i => i.Id == id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateItem(Item item)
        {
            item.Id = _idCounter++;
            _items.Add(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Item updatedItem)
        {
            var existingItem = _items.Find(i => i.Id == id);
            if (existingItem == null)
                return NotFound();

            existingItem.Name = updatedItem.Name;
            existingItem.Price = updatedItem.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _items.Find(i => i.Id == id);
            if (item == null)
                return NotFound();

            _items.Remove(item);

            return NoContent();
        }
    }

}


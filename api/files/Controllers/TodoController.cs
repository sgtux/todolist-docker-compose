using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.Services;

namespace PokeApi
{
    [Route("[controller]")]
    public class TodoController : Controller
    {
        private TodoService _service;

        public TodoController(TodoService service) => _service = service;

        [HttpGet]
        public ActionResult Get() => Ok(_service.GetAll());

        [HttpPost]
        public ActionResult Add([FromBody] TodoItem todoItem)
        {
            if (string.IsNullOrWhiteSpace(todoItem.Description))
                return BadRequest("Invalid Description");
            _service.Add(todoItem);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] TodoItem todoItem)
        {
            if (string.IsNullOrWhiteSpace(todoItem.Description))
                return BadRequest("Invalid Description");
            _service.Update(todoItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Remove(id);
            return Ok();
        }
    }
}
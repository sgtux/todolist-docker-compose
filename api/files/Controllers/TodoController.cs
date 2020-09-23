using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System;

namespace Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private TodoService _service;

        public TodoController(TodoService service) => _service = service;

        [HttpGet]
        public ActionResult Get([FromQuery] string filter) => Ok(_service.GetAll(UserId, filter));

        [HttpPost]
        public ActionResult Add([FromBody] TodoItem todoItem)
        {
            if (string.IsNullOrWhiteSpace(todoItem.Description))
                return BadRequest("Invalid Description");
            todoItem.UserId = UserId;
            _service.Add(todoItem);
            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody] TodoItem todoItem)
        {
            if (string.IsNullOrWhiteSpace(todoItem.Description))
                return BadRequest("Invalid Description");
            todoItem.UserId = UserId;
            _service.Update(todoItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Remove(id, UserId);
            return Ok();
        }

        protected int UserId
        {
            get
            {
                var userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value;
                return Convert.ToInt32(userId);
            }
        }
    }
}
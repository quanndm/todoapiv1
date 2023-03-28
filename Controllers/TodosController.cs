using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Dtos;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly MyContext _context;

        public TodosController(MyContext context)
        {
            _context = context;
        }


        // GET: api/<TodosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listTodo = await _context.Todos!.ToListAsync();
            return Ok(listTodo);
        }

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute]string id)
        {
            var todo = await _context.Todos!.SingleOrDefaultAsync(x=>x.Id == id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpGet("{id}/change_status")]
        public async Task<IActionResult> ChangeStatus([FromRoute]string id)
        {
            var todo = await _context.Todos!.SingleOrDefaultAsync(x=>x.Id == id);
            if (todo == null) return NotFound();
            todo.IsDone = !todo.IsDone;
            await _context.SaveChangesAsync();

            return Ok(new {message =  "Updated"});
        }
        // POST api/<TodosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoDto todoDto)
        {
            if(todoDto.Content == null) return BadRequest();

            var todo = new Todo()
            {
                Content = todoDto.Content,
                IsDone = false, 
                Title = todoDto.Title ?? "",
                Category = todoDto.Category ?? ""
            };

            _context.Todos!.Add(todo);
            await _context.SaveChangesAsync();

            return StatusCode(201, new {todo});
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] TodoDto todoDto)
        {   
            if(id != todoDto.Id) return BadRequest();
            if (todoDto.Content == null) return BadRequest();
            var todo = await _context.Todos!.SingleOrDefaultAsync(x=>x.Id == id);

            if(todo == null) return NotFound();

            todo.Title = todoDto.Title ?? "";
            todo.Content = todoDto.Content;
            todo.Category = todoDto.Category ?? "LATELY";
            todo.Last_update = DateTime.Now.ToString();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            var todo = await _context.Todos!.SingleOrDefaultAsync(x=>x.Id == id); 
            if(todo == null) return NotFound();

            _context.Todos!.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent() ;
        }
    }
}

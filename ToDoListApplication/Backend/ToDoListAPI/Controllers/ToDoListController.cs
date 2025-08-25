using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : Controller
    {
        private readonly IToDoListRepository toDoListRepository;

        public ToDoListController(IToDoListRepository toDoListRepository)
        {
            this.toDoListRepository = toDoListRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoListAll()
        {
            // This method is intended to retrieve all items in the to-do list.

            List<ToDoList> toDoLists = await this.toDoListRepository.GetAllToDoListsAsync();

            return Ok(toDoLists);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetToDoListById([FromRoute] int id)
        {
            // This method is intended to retrieve a specific item in the to-do list by its ID.
            ToDoList? toDoList = await this.toDoListRepository.GetToDoListByIdAsync(id);
            if (toDoList != null)
                return Ok(toDoList);
            else
                return NotFound();
        }

        [HttpPatch]
        [Route("{id:int}/isCompleted")]
        public async Task<IActionResult> UpdateToDoListIsComplete([FromRoute] int id, [FromBody] ToDoListIsComplete model)
        {
            // This method is intended to update the completion status of a specific item in the to-do list by its ID.
            ToDoList? toDoList = await this.toDoListRepository.UpdateIsCompletedAsync(id, model);

            if (toDoList != null)
                return Ok(toDoList);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDoList([FromBody] AddToDoList model)
        {
            // This method is intended to create a new item in the to-do list.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ToDoList toDoList = await this.toDoListRepository.CreateToDoListAsync(model);
            return Ok(toDoList);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateToDoList([FromRoute] int id, [FromBody] UpdateToDoList model)
        {
            //This method is intended to update an existing item in the to-do list by its ID.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ToDoList? toDoList = await this.toDoListRepository.UpdateToDoListAsync(id, model);

            if (toDoList != null)
                return Ok(toDoList);
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletetToDoList([FromRoute] int id)
        {
            // This method is intended to delete a specific item in the to-do list by its ID.
            ToDoList? toDoList = await this.toDoListRepository.DeleteToDoListAsync(id);

            if (toDoList == null)
            {
                return NotFound();
            }
            
            return Ok(toDoList);
        }

        [HttpGet]
        [Route("{date:datetime}")]
        public async Task<IActionResult> GetToDoListByDate([FromRoute] DateTime date)
        {
            // This method is intended to retrieve all items in the to-do list for a specific date.
            List<ToDoList> toDoLists = await this.toDoListRepository.GetToDoListByDate(date);

            if (toDoLists == null || toDoLists.Count == 0)
            {
                return NotFound();
            }

            return Ok(toDoLists);
        }

        [HttpGet]
        [Route("month/{month:int}")]
        public async Task<IActionResult> GetToDoListByMonth([FromRoute] int month)
        {
            // This method is intended to retrieve all items in the to-do list for a specific month.
            List<ToDoList> toDoLists = await this.toDoListRepository.GetToDoListByMonth(month);

            if (toDoLists == null || toDoLists.Count == 0)
            {
                return NotFound();
            }

            return Ok(toDoLists);
        }
    }
}

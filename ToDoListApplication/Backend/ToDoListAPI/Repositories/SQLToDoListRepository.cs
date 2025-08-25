using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Data;
using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories
{
    public class SQLToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListDbContext db;

        public SQLToDoListRepository(ToDoListDbContext db)
        {
            this.db = db;
        }

        public async Task<List<ToDoList>> GetAllToDoListsAsync()
        {
            return await db.toDoLists.ToListAsync();
        }

        public async Task<ToDoList?> GetToDoListByIdAsync(int id)
        {
            return await db.toDoLists.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<ToDoList?> UpdateIsCompletedAsync(int id, ToDoListIsComplete model)
        {
            var toDoList = await db.toDoLists.FirstOrDefaultAsync(x => x.id == id);

            if (toDoList == null)
                return null;

            toDoList.isCompleted = model.isCompleted;
            await db.SaveChangesAsync();

            return toDoList;
        }
        public async Task<ToDoList> CreateToDoListAsync(AddToDoList model)
        {
            //Convert AddToDoList model to ToDoList entity
            var toDoList = new ToDoList
            {
                title = model.title,
                description = model.description,
                isCompleted = model.isCompleted,
                createdAt = model.createdAt
            };

            await db.toDoLists.AddAsync(toDoList);
            await db.SaveChangesAsync();
            return toDoList;
        }

        public async Task<ToDoList?> UpdateToDoListAsync(int id, UpdateToDoList model)
        {
            var toDoList = await db.toDoLists.FirstOrDefaultAsync(x => x.id == id);

            if (toDoList == null)
                return null;

            toDoList.title = model.title;
            toDoList.description = model.description;
            toDoList.isCompleted = model.isCompleted;
            toDoList.createdAt = model.createdAt;

            await db.SaveChangesAsync();
            return toDoList;
        }

        public async Task<ToDoList?> DeleteToDoListAsync(int id)
        {
            var toDoList = await db.toDoLists.FirstOrDefaultAsync(x => x.id == id);

            if (toDoList == null)
                return null;

            db.toDoLists.Remove(toDoList);
            await db.SaveChangesAsync();

            return toDoList;
        }

        public async Task<List<ToDoList>> GetToDoListByDate(DateTime date)
        {
            return await db.toDoLists.Where(x => x.createdAt.Date == date.Date).ToListAsync();
        }

        public async Task<List<ToDoList>> GetToDoListByMonth(int month)
        {
            return await db.toDoLists.Where(x => x.createdAt.Month == month).ToListAsync();
        }
    }
}

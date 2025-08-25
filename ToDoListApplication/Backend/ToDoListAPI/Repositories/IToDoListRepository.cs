using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories
{
    public interface IToDoListRepository
    {
        Task<List<ToDoList>> GetAllToDoListsAsync();
        Task<ToDoList?> GetToDoListByIdAsync(int id);
        Task<ToDoList?> UpdateIsCompletedAsync(int id, ToDoListIsComplete model);
        Task<ToDoList> CreateToDoListAsync(AddToDoList model);
        Task<ToDoList?> UpdateToDoListAsync(int id, UpdateToDoList model);
        Task<ToDoList?> DeleteToDoListAsync(int id);
        Task<List<ToDoList>> GetToDoListByDate(DateTime date);
        Task<List<ToDoList>> GetToDoListByMonth(int month);
    }
}

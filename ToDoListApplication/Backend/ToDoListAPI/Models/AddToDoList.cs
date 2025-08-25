using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.Models
{
    public class AddToDoList
    {
        [Required]
        public string title { get; set; }
        public string? description { get; set; }
        [Required]
        public bool isCompleted { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Task_Web_Application.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }
        public string CreatedByEmail { get; set; }

        public string? AssignedTo { get; set; }
        public string? AssignedToEmail { get; set; }
    }
}
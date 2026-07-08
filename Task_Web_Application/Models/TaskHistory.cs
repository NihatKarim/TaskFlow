namespace Task_Web_Application.Models
{
    public class TaskHistory
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public string Action { get; set; } // Created / Updated / Deleted

        public string ChangedBy { get; set; }

        public DateTime ChangedAt { get; set; } = DateTime.Now;
    }
}
namespace ToDoList.Models
{
    public class task
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string? Status { get; set; }

        public task()
        {
        }

        public task(int id, string title, DateTime dueDate, string status)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
            Status = status; 
        }
    }
}

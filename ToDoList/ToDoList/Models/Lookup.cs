namespace ToDoList.Models
{

    public class Lookup
    {
        public int Id { get; set; }
        public string Value{ get; set; }

        public string Category { get; set; }
        public Lookup(int id, string value, string category)
        {
            Id = id;
            Value = value;
            Category = category;
        }


    }
}

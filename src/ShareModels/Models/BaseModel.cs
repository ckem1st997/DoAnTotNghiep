namespace ShareModels.Models
{
    public class BaseModel
    {
        public BaseModel(int id)
        {
            Id = id;
            Name = "Index: " + id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}

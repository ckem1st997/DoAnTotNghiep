namespace ShareModels.Models
{
    public class StackModel : BaseModel
    {
        public StackModel(int id) : base(id)
        {
            Name = "Stack " + Name;
        }
    }
}

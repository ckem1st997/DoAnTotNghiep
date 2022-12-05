namespace ShareModels.Models
{
    public class QueueModel : BaseModel
    {
        public QueueModel(int id) : base(id)
        {
            Name = "Queue " + Name;
        }
    }
}

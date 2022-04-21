using Infrastructure;

namespace Master.Models
{
    public class ResponseLogin
    {
        public string Jwt { get; set; }
        public UserMaster User { get; set; }
    }
}

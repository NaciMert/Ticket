namespace Ticket.Models
{
    public class UserInfo
    {
        public string UserNameSurname { get;  set; }
        public string UserEmail { get;  set; }
        public string UserPhone { get;  set; }
        public string Role { get;  set; }
        public string UserPassword { get;  set; }
        public int Id { get;  set; }
        public  int CustomerId { get; set; }
    }
}

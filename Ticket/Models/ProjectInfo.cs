namespace Ticket.Models
{
    public class ProjectInfo
    {
        public int Id { get; set; } 

        public string Definition { get; set; }

        public int ParentId { get; set; }

        public string ParentDefinition { get; set; }
    }
}

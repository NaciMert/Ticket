using Ticket.Core.Common;

namespace Ticket.Projects
{
    public class ProjectSummary
    {
        public int Id { get; set; }
        public string Definition { get; set; }

        public int? ParentId { get; set; }

        public string ParentDefinition { get; set; }
    }
}

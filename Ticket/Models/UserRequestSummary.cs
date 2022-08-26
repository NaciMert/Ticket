using Ticket.Core.Common;

namespace Ticket.Models
{
    public class UserRequestSummary
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ModuleId { get; set; }
        public string? ErrorDescription { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public RequestStatuies? Status { get; set; }
        public string? ErrorTitle { get; set; }

    }
}

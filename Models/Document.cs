using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkTest.Models
{
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DocumentId { get; set; }

        public DocumentStatus Status { get; set; } = DocumentStatus.Created;

        public string RegNumber {  get; set; }

        public DateTime RegDate { get; set; }

        public string Content { get; set; }

        public Guid JournalId { get; set; }

        public Guid DocumentViewId { get; set; }

        public List<Message> Messages { get; set; }

        public List<Member> Members { get; set; }
    }

    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageId { get; set; }

        public Guid? DocumentId { get; set; }

        public string Note { get; set; }

        public DateTime DeadLine { get; set; }

        public Document Document { get; set; }

        public List<Member> Members { get; set; }
    }

    public class Member
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MemberId { get; set; }

        public Guid DocumentId { get; set; }

        public Guid MessageId { get; set; }

        public Guid StaffId { get; set; }

        public string FullName { get; set; }

        public Document Document { get; set; }

        public Message Message { get; set; }
    }

    public class Counter
    {
        public Guid CounterId { get; set; }

        public Guid StaffId { get; set; }

        public string MenuIdentifier { get; set; }

        public int Count { get; set; }
    }

    [Flags]
    public enum DocumentStatus
    {
        Created,
        Draft,
        Project,
        Signed,
        ForConsidiretion,
        ForExecution,
        Done,
        InCase
    }
}

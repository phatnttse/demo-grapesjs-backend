using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public class UserNameCard : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid NameCardTemplateId { get; set; }
        public NameCardTemplate? NameCardTemplate { get; set; }

    }
}

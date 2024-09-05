using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public class NameCardTemplate : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public ICollection<UserNameCard>? UserNameCards { get; set; }

    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string CoverPhoto { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CompanyName {  get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;

        public ICollection<UserNameCard>? UserNameCards { get; set; } 

    }
}

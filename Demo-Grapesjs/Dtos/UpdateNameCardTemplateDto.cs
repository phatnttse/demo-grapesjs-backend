﻿namespace Demo_Grapesjs.Dtos
{
    public class UpdateNameCardTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}

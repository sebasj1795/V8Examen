﻿namespace Security.Application.Dto.Module
{
    public class ModuleUpdateResponseDto
    {
        public int Id { get; set; }
        public int IdApp { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string IconCss { get; set; }
        public string IconImg { get; set; }
        public byte State { get; set; }
    }
}

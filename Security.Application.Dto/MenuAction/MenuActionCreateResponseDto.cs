﻿namespace Security.Application.Dto.MenuAction
{
    public class MenuActionCreateResponseDto
    {
        public int Id { get; set; }
        public int IdMenu { get; set; }
        public int IdAction { get; set; }
        public byte State { get; set; }
    }
}

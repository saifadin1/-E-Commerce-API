﻿namespace E_Commerce_API.DTOs.Category
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; } = 0;
    }
}

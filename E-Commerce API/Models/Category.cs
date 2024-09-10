﻿using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}

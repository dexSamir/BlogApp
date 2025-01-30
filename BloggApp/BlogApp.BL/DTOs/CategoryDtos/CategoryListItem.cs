using System;
using BlogApp.Core.Entities;

namespace BlogApp.BL.DTOs.CategoryDtos;

public class CategoryListItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public DateTime CreatedTime { get; set; }
}


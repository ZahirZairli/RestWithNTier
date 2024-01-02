﻿namespace Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProductCode { get; set; }
    public double Price { get; set; }
    public DateTime Created { get; set; }
    public bool IsDeleted { get; set; }
}

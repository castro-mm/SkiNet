using System;
using Core.Base;

namespace Core.Entities;

public class DeliveryMethod : Entity
{
    public required string ShortName { get; set; }
    public required string DeliveryTime { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
}

using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for product
/// </summary>
public class OrderClass
{
    public int Batch_Id { get; set; }
    public List<OrderItem> Items { get; set; }
}
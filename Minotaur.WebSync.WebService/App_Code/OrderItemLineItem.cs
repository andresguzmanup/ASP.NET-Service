using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderItemLineItem
/// </summary>
public class OrderItemLineItem
{
    public int id { get; set; }
    public string name { get; set; }
    public string company { get; set; }
    public int product_id { get; set; }
    public int variation_id { get; set; }
    public int quantity { get; set; }
    public string tax_class { get; set; }
    public string subtotal { get; set; }
    public string subtotal_tax { get; set; }
    public string total { get; set; }
    public string total_tax { get; set; }
    public List<string> taxes { get; set; }
    public List<string> meta_data { get; set; }
    public string sku { get; set; }
    public int price { get; set; }
}
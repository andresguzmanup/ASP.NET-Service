using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderItemShippingLine
/// </summary>
public class OrderItemShippingLine
{
    public int id { get; set; }
    public string method_title { get; set; }
    public string method_id { get; set; }
    public string total { get; set; }
    public string total_tax { get; set; }
    public List<string> taxes { get; set; }
    public List<string> meta_data { get; set; }
}
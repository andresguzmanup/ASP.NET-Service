using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for OrderBatch
/// </summary>
public class OrderBatch
{
    public string status { get; set; }
    public int batch_id { get; set; }
    public List<int> order_ids { get; set; }
}
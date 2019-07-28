using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for product_batch
/// </summary>
public class product_batch
{
    public List<product> Products { get; set; }
    public int HasMoreRecords { get; set; }
    public int NoOfRecords { get; set; }
}
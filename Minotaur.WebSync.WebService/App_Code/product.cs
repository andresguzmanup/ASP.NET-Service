using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for product
/// </summary>
public class product
{
    public string ProductID { get; set; }
    public string StockKey { get; set; }
    public string SupplierProductCode { get; set; }
    public string BarcodeISBN { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string AuthorSubject { get; set; }
    public string PublisherName { get; set; }
    public string AdultContent { get; set; }
    public string Qty { get; set; }
    public string ImageURL { get; set; }
    public List<product_type> ProductType { get; set; }
    public string Description { get; set; }
    public string DVDRegion { get; set; }
    public string PriceAUD { get; set; }
    public string SupplierCode { get; set; }
    public string ReleaseDate { get; set; }
    public string Priority { get; set; }
    public List<department> Department { get; set; }
    public List<product_theme> ProductTheme { get; set; }
    public string ProductStatus { get; set; }
    public List<product_dimensions> ProductDimensions { get; set; }
    public string IsOnlyPickupFromStore { get; set; }


}
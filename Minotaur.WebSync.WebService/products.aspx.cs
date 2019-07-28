using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

public partial class products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string authHeader = Request.Headers["authorization"];
        bool authenticated = false;

        try
        {
            if (authHeader == "B0DC315C-FA26-4180-9C0B-B7FB07A67F9D")
            {
                authenticated = true;
            }
        }
        catch { }
        if (!authenticated) { return; }


        Response.ContentType = "application/json";

        int lastID = 0;
        int lastSyncTimestamp = 0;
        if (!String.IsNullOrEmpty(Request.QueryString["lastid"]))
        {
            // Query string value is there so now use it
            lastID = Convert.ToInt32(Request.QueryString["lastid"]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString["lastsync"]))
        {
            // Query string value is there so now use it
            lastSyncTimestamp = Convert.ToInt32(Request.QueryString["lastsync"]);
        }

        List<product> products = new List<product>();

        DataSet dstBatchData = GetData(string.Format("spWebSync_Product_GetBatch '{0}', '{1}'", lastID, lastSyncTimestamp));
        DataTable dt = dstBatchData.Tables[0];
        int maxProductID = Convert.ToInt32(dstBatchData.Tables[1].Rows[0]["MaxProduct_ID"]);
        int hasMoreRecords = 1;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (Convert.ToInt32(dt.Rows[i]["Product_ID"]) >= maxProductID)
            {
                //* This is the last record - ensure the web service says there are no more records...
                hasMoreRecords = 0;
            }


            product product = new product
            {
                ProductID = Convert.ToString(dt.Rows[i]["Product_ID"]).Trim()
                ,
                StockKey = Convert.ToString(dt.Rows[i]["StockKey"]).Trim()
                ,
                SupplierProductCode = Convert.ToString(dt.Rows[i]["SupplierProductCode"]).Trim()
                ,
                BarcodeISBN = Convert.ToString(dt.Rows[i]["BarcodeISBN"]).Trim()
                ,
                Title = Convert.ToString(dt.Rows[i]["Title"]).Trim()
                ,
                Author = Convert.ToString(dt.Rows[i]["Author"]).Trim()
                ,
                AuthorSubject = Convert.ToString(dt.Rows[i]["AuthorSubject"]).Trim()
                ,
                PublisherName = Convert.ToString(dt.Rows[i]["PublisherName"]).Trim()
                ,
                AdultContent = Convert.ToString(dt.Rows[i]["AdultContent"]).Trim()
                ,
                Qty = Convert.ToString(dt.Rows[i]["Qty"]).Trim()
                ,
                ImageURL = Convert.ToString(dt.Rows[i]["Image"]).Trim()
                ,
                ProductType = GetProductTypes(Convert.ToString(dt.Rows[i]["ProductType_ID"]))
                ,
                Description = Convert.ToString(dt.Rows[i]["Description"]).Trim()
                ,
                DVDRegion = Convert.ToString(dt.Rows[i]["DVDRegion"]).Trim()
                ,
                PriceAUD = Convert.ToString(dt.Rows[i]["Price"]).Trim()
                ,
                SupplierCode = Convert.ToString(dt.Rows[i]["SupplierCode"]).Trim()
                ,
                ReleaseDate = Convert.ToString(dt.Rows[i]["ReleaseDate"]).Trim()
                ,
                Priority = Convert.ToString(dt.Rows[i]["Priority"]).Trim()
                ,
                Department = GetProductDepartments(Convert.ToString(dt.Rows[i]["Product_ID"]))
                ,
                ProductTheme = GetProductTheme(Convert.ToString(dt.Rows[i]["Product_ID"]))
                ,
                ProductStatus = Convert.ToString(dt.Rows[i]["ProductStatus"])
                ,
                IsOnlyPickupFromStore = Convert.ToString(dt.Rows[i]["IsOnlyPickupFromStore"])
            };
            products.Add(product);
        }

        //* Add products and batch information to document:
        product_batch master_document = new product_batch
        {
            Products = products
            ,
            NoOfRecords = dt.Rows.Count
            ,
            HasMoreRecords = hasMoreRecords
        };


        var json = new JavaScriptSerializer().Serialize(master_document);
 


        Response.Write(json);

    }

    public List<product_type> GetProductTypes(string product_type_id)
    {
        List<product_type> producttypes = new List<product_type>();
        DataTable dt = GetData(string.Format("SELECT * FROM ProductType Where ProductType_ID ='{0}'", product_type_id)).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            producttypes.Add(new product_type
            {
                ProductTypeID = Convert.ToInt32(dt.Rows[i]["ProductType_ID"])
                ,
                ProductTypeDescription = Convert.ToString(dt.Rows[i]["Description"]).Trim()
            });
        }
        return producttypes;
    }

    public List<department> GetProductDepartments(string product_id)
    {
        List<department> productdepartments = new List<department>();
        DataTable dt = GetData(string.Format("SELECT d.department_id, d.description FROM ProductDepartment pd INNER JOIN Department d ON d.Department_ID = pd.Department_ID WHERE product_id = '{0}'", product_id)).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            productdepartments.Add(new department
            {
                DepartmentID = Convert.ToInt32(dt.Rows[i]["Department_ID"])
                ,
                Department= Convert.ToString(dt.Rows[i]["Description"]).Trim()
            });
        }
        return productdepartments;

    }

    public List<product_theme> GetProductTheme(string product_id)
    {
        List<product_theme> productthemes = new List<product_theme>();
        DataTable dt = GetData(string.Format("SELECT t.theme_id, t.description FROM ProductTheme pt INNER JOIN Theme t ON t.Theme_ID = pt.Theme_ID WHERE pt.product_id = '{0}'", product_id)).Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            productthemes.Add(new product_theme
            {
                ThemeID = Convert.ToInt32(dt.Rows[i]["Theme_ID"])
                ,
                Theme= Convert.ToString(dt.Rows[i]["Description"]).Trim()
            });
        }
        return productthemes;
    }

    private DataSet GetData(string query)
    {
        string conString = "Server=192.168.0.13;Database=Minotaur;User Id=sa; Password = S5ZlZ5XGsPZBqYWbs5xd;";//ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(conString);
        SqlDataAdapter da = new SqlDataAdapter(query, conn);
        da.Fill(ds);
        conn.Close();
        conn.Dispose();
        da.Dispose();
        return ds;

        //SqlCommand cmd = new SqlCommand(query);
        //using (SqlConnection con = new SqlConnection(conString))
        //{
        //    using (SqlDataAdapter sda = new SqlDataAdapter())
        //    {
        //        cmd.Connection = con;
        //        sda.SelectCommand = cmd;
        //        using (DataTable dt = new DataTable())
        //        {
        //            sda.Fill(dt);
        //            return dt;

        //        }
        //    }
        //}
    }
}
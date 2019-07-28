using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for order
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class order : System.Web.Services.WebService
{
    //public SqlConnection conn = new SqlConnection();
    public order()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public string SaveData(string myjson) //save data to database
    {
        order service = new order();
        //var mydata = JsonConvert.DeserializeObject<List<Product>>(myjson);// parse json string to object.
        //var s = new JavaScriptSerializer();
        //var mydata = s.Deserialize<List<Product>>(myjson);

        dynamic mydata = JsonConvert.DeserializeObject(myjson);
        foreach (var item in mydata)
        {
            Console.WriteLine(item.Id);
            ///conn.ConnectionString = @"Server=VDI-V-LEZHA1\SQLEXPRESS;Database=MyDB;Trusted_Connection=Yes";
            //string sqlText = "Insert into products values(@Id,@name,@category,@price)";
            //SqlCommand sqlCmd = new SqlCommand(sqlText, conn);
            //sqlCmd.Parameters.AddWithValue("@Id", item.Id);
            //sqlCmd.Parameters.AddWithValue("@name", item.Name);
            //sqlCmd.Parameters.AddWithValue("@category", item.Category);
            //s//qlCmd.Parameters.AddWithValue("@price", item.Price);
            //conn.Open();
            //int i = sqlCmd.ExecuteNonQuery();
            //conn.Close();
        }
        return myjson;
    }
}


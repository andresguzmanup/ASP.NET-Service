using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for OrderService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class OrderService : System.Web.Services.WebService
{

    public OrderService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public OrderBatch GetOrderId(OrderClass order)
    {
        string result = "";
        int Batch_Id = order.Batch_Id;
        List<int> order_ids = new List<int> { };
        for(int i = 0; i < order.Items.Count; i++)
        {
            OrderItem orderItem = order.Items[i];
            order_ids.Add(orderItem.id);
            string order_key = orderItem.order_key;
            string billing_first_name = orderItem.billing.first_name;
            string billing_last_name = orderItem.billing.last_name;
            //Console.WriteLine("order_key: " + order_key + ", billing.first_name: " + billing_first_name + ", billing.last_name: " + billing_last_name + "\n" + "Line_items:");
            result += "order_key: " + order_key + ", billing.first_name: " + billing_first_name + ", billing.last_name: " + billing_last_name + "\n" + "Line_items:";
            for (int j = 0; j < orderItem.line_items.Count; j++)
            {
                int line_item_id = orderItem.line_items[j].id;
                //Console.WriteLine(" " + line_item_id);
                result += " " + line_item_id;
            }
            //Console.WriteLine("\n");
            result += "\n";
        }

        //* response with json format
        OrderBatch response = new OrderBatch
        {
            status = "SUCCESS"
            ,
            batch_id = Batch_Id
            ,
            order_ids = order_ids
        };

        //var json = new JavaScriptSerializer().Serialize(response);

        return response;
    }

}

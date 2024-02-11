using APIPractice1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace APIPractice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IConfiguration configuration;
        public EmployeesController(IConfiguration configuration)
        {

            this.configuration = configuration;

        }

        [HttpGet]
        [Route("getData")]
        public dynamic GetData() 
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("AppConnectionString").ToString());
            String query = "SELECT * FROM BOOKCRUD";
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Inventory> inventoryList = new List<Inventory>();
            Response response = new Response();
            if (dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    Inventory inventory = new Inventory();
                    inventory.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    inventory.bookName = Convert.ToString(dt.Rows[i]["BOOKNAME"]);
                    inventory.genre = Convert.ToString(dt.Rows[i]["GENRE"]);
                    inventory.authorsName = Convert.ToString(dt.Rows[i]["AUTHORSNAME"]);
                    inventory.borrowersName = Convert.ToString(dt.Rows[i]["BORROWERSNAME"]);
                    inventoryList.Add(inventory);
                }
            }
            if (inventoryList.Count > 0)
            {
                return JsonConvert.SerializeObject(inventoryList);
            }
            else 
            {
                response.statusCode = 100;
                response.errorMessage = "No data found";
                return JsonConvert.SerializeObject(response);

            }
        }

        [HttpPost]
        [Route("insertData")]
        public ActionResult insertBookName(string bookName, string genre, string authorsName, string borrowersName) 
        {
            try 
            {
                SqlConnection connection = new SqlConnection(configuration.GetConnectionString("AppConnectionString").ToString());
                String query = $"INSERT INTO BOOKCRUD(BOOKNAME, GENRE, AUTHORSNAME, BORROWERSNAME) VALUES('{bookName}','{genre}','{authorsName}','{borrowersName}')";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return Ok();
            }
            catch (Exception e) 
            {
                return BadRequest(e);
            }
        }
        
    }
}

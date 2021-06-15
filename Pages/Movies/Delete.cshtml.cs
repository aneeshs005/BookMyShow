using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookMyShow.Models;
using System.Data.SqlClient;
using System.Data;

namespace BookMyShow.Pages
{
    public class DeleteModel : PageModel
    {
        public List<Movie> MovieList = new List<Movie>();
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(ILogger<DeleteModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int MovieID)
        {
            string sqlConnectString = "Data Source=(localdb)\\BookMyShowDB;Initial Catalog=BookMyShow;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(sqlConnectString);
            connection.Open();

            string sqlSelect = "SELECT * FROM Movies WHERE MovieID=@MovieID";
            SqlCommand command = new SqlCommand(sqlSelect, connection);

            command.Parameters.Add("@MovieID", SqlDbType.Int);
            command.Parameters["@MovieID"].Value = MovieID;

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MovieList.Add(new Movie(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
            }

            connection.Close();
        }
        
        public IActionResult OnPost(int MovieID)
        {
            
            string sqlConnectString = "Data Source=(localdb)\\BookMyShowDB;Initial Catalog=BookMyShow;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(sqlConnectString);

            string sqlDel = @"DELETE FROM Movies WHERE MovieID=@MovieID";
            SqlCommand command = new SqlCommand(sqlDel, connection);

            

            command.Parameters.Add("@MovieID", SqlDbType.Int);

            try
            {
                connection.Open();

                command.Parameters["@MovieID"].Value = MovieID;
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {

            }
            finally
            {
                connection.Close();
            }
            return Redirect("~/movies/edit");
        }
    }
}

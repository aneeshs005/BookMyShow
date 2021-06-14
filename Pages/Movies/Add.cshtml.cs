using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using BookMyShow.Models;

namespace BookMyShow.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;

        public AddModel(ILogger<AddModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public void OnPost(string MovieName, string Year, string Language, string Genre, String ActorName)
        {
            string sqlConnectString = "Data Source=(localdb)\\BookMyShowDB;Initial Catalog=BookMyShow;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(sqlConnectString);

            string sqlAdd = @"INSERT INTO Movies(MovieName,Year,Language,Genre,ActorName)VALUES(@MovieName,@Year,@Language,@Genre,@ActorName)";
            SqlCommand command = new SqlCommand(sqlAdd, connection);

            command.Parameters.Add("@MovieName", SqlDbType.VarChar, 100);
            command.Parameters.Add("@Year", SqlDbType.Int);
            command.Parameters.Add("@Language", SqlDbType.VarChar, 50);
            command.Parameters.Add("@Genre", SqlDbType.VarChar, 20);
            command.Parameters.Add("@ActorName", SqlDbType.VarChar, 50);

            if (command.Connection.State == ConnectionState.Open)
            {
                command.Connection.Close();
            }

            try
            {
                connection.Open();

                command.Parameters["@MovieName"].Value = MovieName;
                int year = Int32.Parse(Year);
                command.Parameters["@Year"].Value = year;
                command.Parameters["@Language"].Value = Language;
                command.Parameters["@Genre"].Value = Genre;
                command.Parameters["@ActorName"].Value = ActorName;

                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {

            }
            finally
            {
                connection.Close();
            }
        }
    }
}

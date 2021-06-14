using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using BookMyShow.Models;
using System.Data.SqlClient;

namespace BookMyShow.Pages
{
    public class ListModel : PageModel
    {
        public List<Movie> MovieList = new List<Movie>();

        private readonly ILogger<ListModel> _logger;

        public ListModel(ILogger<ListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string sqlConnectString = "Data Source=(localdb)\\BookMyShowDB;Initial Catalog=BookMyShow;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(sqlConnectString);
            connection.Open();

            string sqlSelect = "SELECT * FROM Movies";
            SqlCommand command = new SqlCommand(sqlSelect, connection);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                MovieList.Add(new Movie(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
            }

        }   
    }
}

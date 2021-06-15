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
    public class EditConfirmModel : PageModel
    {
        private readonly ILogger<EditConfirmModel> _logger;

        public EditConfirmModel(ILogger<EditConfirmModel> logger)
        {
            _logger = logger;
        }

        public List<Movie> MovieList = new List<Movie>();

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

        public IActionResult OnPost(int MovieID, string MovieName, string Year, string Language, string Genre, String ActorName)
        {
            string sqlConnectString = "Data Source=(localdb)\\BookMyShowDB;Initial Catalog=BookMyShow;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(sqlConnectString);

            string sqlUpd = @"UPDATE Movies SET MovieName=@MovieName,Year=@Year,Language=@Language,Genre=@Genre,ActorName=@ActorName WHERE MovieID=@MovieID";
            SqlCommand command = new SqlCommand(sqlUpd, connection);

            command.Parameters.Add("@MovieID", SqlDbType.Int);
            command.Parameters.Add("@MovieName", SqlDbType.VarChar, 100);
            command.Parameters.Add("@Year", SqlDbType.Int);
            command.Parameters.Add("@Language", SqlDbType.VarChar, 50);
            command.Parameters.Add("@Genre", SqlDbType.VarChar, 20);
            command.Parameters.Add("@ActorName", SqlDbType.VarChar, 50);

            try
            {
                connection.Open();

                command.Parameters["@MovieID"].Value = MovieID;
                command.Parameters["@MovieName"].Value = MovieName;
                int year = Int32.Parse(Year);
                command.Parameters["@Year"].Value = year;
                command.Parameters["@Language"].Value = Language;
                command.Parameters["@Genre"].Value = Genre;
                command.Parameters["@ActorName"].Value = ActorName;

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
            return Redirect("~/movies/edit");
        }
    }
}

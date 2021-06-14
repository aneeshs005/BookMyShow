namespace BookMyShow.Models
{
    public class Movie
    {
        public Movie(int movieID, string movieName, int year, string language, string genre, string actorname)
        {
            this.MovieID=movieID;
            this.MovieName=movieName;
            this.Year=year;
            this.Language=language;
            this.Genre=genre;
            this.ActorName=actorname;

        }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public int Year { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string ActorName { get; set; }
    }
}
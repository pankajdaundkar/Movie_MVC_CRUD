using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Movie_MVC.Models

{
    public class Movie_CRUD
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public Movie_CRUD(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(configuration.GetConnectionString("defaultConnection"));
        }
        public IEnumerable<Movie> GetAllMovies()
        {
            List<Movie> list = new List<Movie>();
            string qry = "select * from movie where isActive=1";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Movie m = new Movie();
                   m.Id = Convert.ToInt32(dr["id"]);
                   m.Title = dr["title"].ToString();
                   m.ReleaseDate = Convert.ToDateTime(dr["release_date"]);
                   m.MovieType = dr["movie_type"].ToString();
                   m.StarName = dr["star_name"].ToString();
                   m.isActive = Convert.ToInt32(dr["isActive"]);
                    list.Add(m);


                }
            }
            con.Close();
            return list;
        }
        public Movie GetMovieById(int id)
        {
            Movie m = new Movie();
            string qry = "select * from movie where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    m.Id = Convert.ToInt32(dr["id"]);
                    m.Title = dr["title"].ToString();
                    m.ReleaseDate = Convert.ToDateTime(dr["release_date"]);
                    m.MovieType = dr["movie_type"].ToString();
                    m.StarName = dr["star_name"].ToString();
                }
            }
            con.Close();
            return m;
        }
        public int AddMovie(Movie movie)
        {
            movie.isActive = 1;
            int result = 0;
            string qry = "insert into movie values(@title,@release_date,@movie_type,@star_name,@isActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@release_date", movie.ReleaseDate);
            cmd.Parameters.AddWithValue("@movie_type", movie.MovieType);
            cmd.Parameters.AddWithValue(@"star_name",movie. StarName);
            cmd.Parameters.AddWithValue("@isActive", movie.isActive);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
        public int UpdateMovie(Movie movie)
        {
            movie.isActive = 1;
            int result = 0;
            string qry = "update movie set title=@title,release_date=@release_date,movie_type=@movie_type,star_name=@star_name,isActive=@isActive where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@title", movie.Title);
            cmd.Parameters.AddWithValue("@release_date", movie.ReleaseDate);
            cmd.Parameters.AddWithValue("@movie_type", movie.MovieType);
            cmd.Parameters.AddWithValue(@"star_name", movie.StarName);
            cmd.Parameters.AddWithValue("@isActive", movie.isActive);
            cmd.Parameters.AddWithValue("@id", movie.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


        // soft delete --> record should be present in DB , but should not visible on the form
        public int DeleteMovie(int id)
        {
            int result = 0;
            string qry = "update movie set isActive=0 where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

    }
}

using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using MovieRecommender.Core.Interfaces.Repositories;
using MovieRecommender.Core.Models;
using MovieRecommender.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MovieRecommender.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesRecommenderContext _context;

        public MovieRepository(MoviesRecommenderContext context)
        {
            _context = context;
        }
        public void ImportMovies()
        {
            List<Movie> movies = new List<Movie>();
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var fileName = desktopPath + @"\links.xlsx";
             
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) 
                    {
                        movies.Add(new Movie
                        {
                            ID = Convert.ToInt32(reader.GetValue(0)),
                            TmdbID = Convert.ToInt32(reader.GetValue(2)),
                            ImdbID = Convert.ToInt32(reader.GetValue(1)),
                            Name = reader.GetValue(3).ToString()
                        });
                    }
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                foreach(var movie in movies)
                {
                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Movies] ON");

                    _context.Movies.Add(movie);
                    _context.SaveChanges();

                    _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Movies] OFF");
                }

                

                transaction.Commit();
            }
        }
    }
}

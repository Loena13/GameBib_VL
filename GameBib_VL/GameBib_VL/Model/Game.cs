using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBib_VL.Model
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public DateTime Issue_Date { get; set; }
        public int Rating { get; set; }
        public ICollection<GameGenre> GameGenres { get; set; } //Refers to GameGenre model
        public ICollection<Genre> Genres { get; set; } //Refers to Genre model and creates a collection of all genres
        public ICollection<FavoriteGame> LikedByUsers { get; set; } //Refers to FavoriteGame model and creates a collection of the liked games by users
    }
}

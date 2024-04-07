using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBib_VL.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; } //Refers to the Game model and creates a collection of games
        public ICollection<GameGenre> GameGenres { get; set; } //Refers to GameGenre model
    }
}

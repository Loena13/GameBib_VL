using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBib_VL.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassWord { get; set; }
        public static User LoggedInUser { get; set; } //Refers to the current logged in user
        public ICollection<FavoriteGame> FavoriteGames { get; set; } //Refers to the FavoriteGame model and creates a collection of favorite games

        public void AddFavoriteGame(Game game)
        {
            //Checks if favorite games collection exsist
            if (FavoriteGames == null)
                FavoriteGames = new List<FavoriteGame>();

            //Adds game to favorite game list of user
            FavoriteGames.Add(new FavoriteGame { Game = game, UserId = Id });
        }
    }
}

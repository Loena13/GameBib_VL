using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBib_VL.Model
{
    public class FavoriteGame
    {
        public int Id { get; set; }
        public User User { get; set; } //Refers to User model
        public int UserId { get; set; } //Uses reference to get the User Id
        public Game Game { get; set; } //Refers to Game model
        public int GameId { get; set; } //Uses reference to get Game id
    }
}

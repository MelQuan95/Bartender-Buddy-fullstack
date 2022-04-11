using System.ComponentModel.DataAnnotations;

namespace BartenderBuddy.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public int CocktailId { get; set; }

    }
}
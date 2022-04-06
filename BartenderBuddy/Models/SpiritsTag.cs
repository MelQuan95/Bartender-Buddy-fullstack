using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BartenderBuddy.Models
{
    public class SpiritsTag
    {
        public int id { get; set; }
        public int SpiritsId { get; set; }
        public int CocktailId { get; set; }

    }
}
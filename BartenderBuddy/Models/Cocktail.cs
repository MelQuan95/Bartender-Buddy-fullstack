using System.ComponentModel.DataAnnotations;

namespace Cocktails.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataType img { get; set; }
        public int content { get; set; }
        public int categoryId { get; set; }
        public int userProfileId { get; set; }

    }
}
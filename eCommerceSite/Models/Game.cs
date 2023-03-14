using System.ComponentModel.DataAnnotations;

namespace eCommerceSite.Models
{
    /// <summary>
    /// Represents an available game
    /// </summary>
    public class Game
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// title of game
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// price of game
        /// </summary>
        [Range(0, 1000)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
    }

    /// <summary>
    /// A single video game that has been added to the users shopping cart cookie
    /// </summary>
    public class CartGameViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
    }
}

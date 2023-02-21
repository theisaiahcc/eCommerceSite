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
        public double Price { get; set; }
        
        // To-do: Add rating
    }
}

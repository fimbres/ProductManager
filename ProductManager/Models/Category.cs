using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductManager.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("DIsplay Order")]
        [Range(1,100, ErrorMessage="The Order must be between 1 and 100 only.")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public required string UserName { get; set; }
    }
}

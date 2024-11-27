using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoList1.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;



        //References from Foreign Key
        [JsonIgnore]
        public virtual ICollection<TasksModel>? Tasks { get; set; }
    }
}

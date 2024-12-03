using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Blog_Management_API.Model
{
    public class Blogs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("userName")]
        public required string UserName { get; set; }
        [JsonPropertyName("dateCreated")]
        public  DateTime DateCreated { get { return DateTime.Now; } }
        [JsonPropertyName("text")]
        public required string Text { get; set; }
    }
}

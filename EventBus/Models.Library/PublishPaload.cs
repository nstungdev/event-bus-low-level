using System.ComponentModel.DataAnnotations;

namespace Models.Library
{
    public class PublishPaload
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Event { get; set; }
    }
}

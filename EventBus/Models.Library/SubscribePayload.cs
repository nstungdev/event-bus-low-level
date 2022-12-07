using System.ComponentModel.DataAnnotations;

namespace Models.Library
{
    public class SubscribePayload
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public Callback Callback { get; set; }
    }
}

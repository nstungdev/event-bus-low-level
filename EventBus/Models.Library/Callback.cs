using System.ComponentModel.DataAnnotations;

namespace Models.Library
{
    public class Callback
    {
        [Required]
        public string CallbackURL { get; set; }
        [Required]
        public HttpMethod HttpCallbackMethod { get; set; }
    }
}

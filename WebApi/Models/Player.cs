using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int? ShirtNo { get; set; }
        //[MaxLength(100)]
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }
    }
}

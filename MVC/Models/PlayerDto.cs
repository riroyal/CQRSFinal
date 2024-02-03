using Microsoft.AspNetCore.Mvc;
using MVC.Services;

namespace MVC.Models
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int? ShirtNo { get; set; }
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }
    }
}

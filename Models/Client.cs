using System.ComponentModel.DataAnnotations.Schema;

namespace ClientAPI.Models
{
    [Table("Clients")]
    public class Client
    {
        public int id { get; set; }
        public string phone {  get; set; }
        public string name { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
    }
}

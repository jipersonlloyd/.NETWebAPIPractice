using System.ComponentModel.DataAnnotations;

namespace APIPractice1.Model
{
    public class Inventory
    {
        [Key]
        public int id { get; set; }
        public string bookName { get; set; }
        public string genre { get; set; }
        public string authorsName { get; set; }
        public string borrowersName { get; set; }
    }
}

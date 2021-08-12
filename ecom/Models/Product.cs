using System.ComponentModel.DataAnnotations.Schema;

namespace ecom.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; private set;}
        public string Name {get; set;}
        public int Stock {get; set;}
        public float Value {get; set;}
    }
}
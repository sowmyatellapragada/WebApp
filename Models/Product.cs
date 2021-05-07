using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Models
{
    //[PhraseAndPrice(Phrase = "Small", Price = "100")]

    public class Product
    {
        public long ProductId { get; set; }
        [Required]
        [Display(Name = "Name")]

        public string Name { get; set; }
        
        // [DisplayFormat(DataFormatString = "{0:c2}",ApplyFormatInEditMode = true)]
        //[BindNever]
        [Column(TypeName = "decimal(8, 2)")]
        [Required(ErrorMessage = "Please enter a price")]
        [Range(1, 999999, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
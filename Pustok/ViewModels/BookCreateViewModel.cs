using Pustok.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pustok.ViewModels
{
    public class BookCreateViewModel
    {
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        [StringLength(maximumLength: 30, ErrorMessage = "Ala gormursen max 30 dene yazmaq olar?")]
        public string Name { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        [StringLength(maximumLength: 10, ErrorMessage = "Ala gormursen max 10 dene yazmaq olar?")]
        public string Code { get; set; }
        [StringLength(maximumLength: 250, ErrorMessage = "Ala gormursen max 250 dene yazmaq olar?")]
        public string Decs { get; set; }
        public bool IsAviable { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public List<IFormFile> ImageFiles { get; set; }
    }
}

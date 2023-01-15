using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        [StringLength(maximumLength:30,ErrorMessage ="Ala gormursen max 30 dene yazmaq olar?")]
        public string Name { get; set; }

        public string? Image { get; set; }
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
        [NotMapped]
        public List<IFormFile>? PosterImageFiles { get; set; }
        [NotMapped]
        public List<IFormFile>? HoverImageFiles { get; set; }

        [NotMapped]
        public List<IFormFile>? ImageFiles { get; set; }
        public Author? Author { get; set; }
        public Genre? Genre { get; set; }
        public List<BookImage>? BookImages { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace IRPr.Models
{
	public class Image
	{
		public int imageId { get; set; }
		public string name { get; set; }
		public int productID { get; set; }

		[NotMapped]
		public ICollection<IFormFile> imageFiles { get; set; }
	}
}

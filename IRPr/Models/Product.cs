using static System.Net.Mime.MediaTypeNames;

namespace IRPr.Models
{
	public class Product
	{
		public int ID { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }

		public double Price { get; set; }
		public ICollection<Image> images { get; set; }
		public int CategoryID { get; set; }
	}
}

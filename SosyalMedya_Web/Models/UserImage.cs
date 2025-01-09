namespace SosyalMedya_Web.Models
{
    public class UserImage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IFormFile ImagePath { get; set; }
        public DateTime Time { get; set; }
    }
}

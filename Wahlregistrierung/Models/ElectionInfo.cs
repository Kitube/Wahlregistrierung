namespace Wahlregistrierung.Models
{
    public class ElectionInfo
    {
        public int ElectionId { get; set; }
        public string Name { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        public bool IsActive { get; set; }
    }
}

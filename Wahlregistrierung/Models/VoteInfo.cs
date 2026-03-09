namespace Wahlregistrierung.Models
{
    public class VoteInfo
    {
        public int VoteId { get; set; }
        public string IdNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string VoteTime { get; set; } = "";
    }
}

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace EngMan.Models
{
    public class GuessesTheImage
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int WordId { get; set; }

        [DataMember]
        public string Path { get; set; }
    }

    public class GuessesTheImageWithTheQueryBD
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int WordId { get; set; }

        [DataMember]
        public string Original { get; set; }

        [DataMember]
        public string Translate { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Transcription { get; set; }

        [DataMember]
        public string Path { get; set; }
    }

    public class GuessesTheImageToReturn
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Word Word { get; set; }

        [DataMember]
        public string Path { get; set; }
    }

    public class GuessesTheImageToAdd
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int WordId { get; set; }

        [DataMember]
        public Image Image { get; set; }
    }
}
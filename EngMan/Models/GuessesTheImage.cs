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
        public string Word { get; set; }

        [DataMember]
        public string Path { get; set; }
    }

    public class GuessesTheImageToAdd
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Word { get; set; }

        [DataMember]
        public Image Image { get; set; }
    }
}
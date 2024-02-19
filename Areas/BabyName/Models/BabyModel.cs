using System.ComponentModel.DataAnnotations;

namespace BabyNames.Areas.BabyName.Models
{
    public class BabyModel
    {

        public int? NameID { get; set; }
        [Required(ErrorMessage ="Name is requied")]

        public string? Name { get; set; }
        
        [Required(ErrorMessage ="Meaning is requied")]
        public string? Meaning { get; set; }
        public int? Numerology { get; set; }
        [Required]

        public List<Gender> Genders { get; set; }
        public string? Gender { get; set; }
        public string? Nakshatra { get; set; }

        public int? NakshatraID { get; set; }
        public int? ZodiacID { get; set; }
        public int? ReligionID { get; set; }

        public int? CategoryID { get; set; } = 1;



        public string? ZodiacName { get; set; }
        public string? ReligionName { get; set; }
        public string? Syllables { get; set; }
        public string? CategoryName { get; set; }

    }

    public enum Gender
    {
        Boy,
        Girl,
        Unisex
    }


    public class BabyFilterModel
    {
        public int? ZodiacID { get; set; }
        public string? Gender { get; set; }
        public int? ReligionID { get; set; }
        public int? NakshatraID { get; set; }

    }


}

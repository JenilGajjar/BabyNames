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
        
        
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]

        public int? Numerology { get; set; }

        [Required]
        public string? Gender { get; set; }
        public string? Nakshatra { get; set; }

        public int? NakshatraID { get; set; }
        public int? ZodiacID { get; set; }
        public int? ReligionID { get; set; }

        public int? CategoryID { get; set; } = 1;



        public string? ZodiacName { get; set; }
        public string? ReligionName { get; set; }

        [Range(1, float.MaxValue, ErrorMessage = "Please enter valid float Number")]

        public string? Syllables { get; set; }
        public string? CategoryName { get; set; }

    }

  

    public class BabyFilterModel
    {
        public int? ZodiacID { get; set; }
        public string? Gender { get; set; }
        public int? ReligionID { get; set; }
        public int? NakshatraID { get; set; }

        public string? Name { get; set; }


    }


}

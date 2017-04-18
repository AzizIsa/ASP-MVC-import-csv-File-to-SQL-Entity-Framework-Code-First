using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Person types and validations
// These Fileds will be created in the database
public class Persons
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PersonId { get; set; }

    [Required,MaxLength(15)]
    public string FirstName { get; set; }

    [Required,MaxLength(15)]
    public string Surname { get; set; }

    [Required, RegularExpression(@"^\d{2}$", ErrorMessage = "Please Enter Correct Age")]
    public int Age { get; set; }

    [Required,MaxLength(1)]
    [RegularExpression(@"^M$|^F$", ErrorMessage = "Enter Valid Sex Please.")]
    public string Sex { get; set; }

    [MaxLength(15)]    
    [RegularExpression(@"^[0-9]{8,15}$", ErrorMessage = "Entered phone format is not valid.")]
    public string Mobile { get; set; }

    [Required]
    public bool Active { get; set; }
}

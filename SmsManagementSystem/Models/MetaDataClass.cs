using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Security.Cryptography.X509Certificates;
namespace SmsManagementSystem.Models
{
    [MetadataType(typeof(Usermetadata))]
    public partial class User
    {
 
        [Required(ErrorMessage = "The Username Field is Required", AllowEmptyStrings = false)]
        [System.Web.Mvc.Remote("IsUsernameAvailble", "Manage", ErrorMessage = "Invalid UserName")]
        [RegularExpression("(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[#?!@$%^&*\\-_]).{8,}$", ErrorMessage = "Invalid  UsersName")]
        public string UserName { get; set; }
        [RegularExpression("(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[#?!@$%^&*\\-_]).{8,}$", ErrorMessage = "Please Type Valid Password")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [RegularExpression("(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[#?!@$%^&*\\-_]).{8,}$", ErrorMessage = "Invalid  Password")]
        [Compare("Password", ErrorMessage = "Password Mismatch")]
        [Display(Name = "Retype-Password")]
        public string RPassword { get; set; }
    }


    public partial class Usermetadata
    {
        [Required(ErrorMessage = "The Name Field is Required", AllowEmptyStrings = false)]
        public string Name { get; set; }
       
        [Required(ErrorMessage = "The Gender Field is Required", AllowEmptyStrings = false)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "The Email Field is Required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid EmailAddress")]
        public string EmailID { get; set; }

    }

    [MetadataType(typeof(LoginMetadata))]
    public partial class Login
    {
    }
    public partial class LoginMetadata
    {
        
        [Required(ErrorMessage = "The Username Field is Required", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Input Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    [MetadataType(typeof(CgppPhonebookMetadata))]
    public partial class CgppPhonebook
    {
    }
    public partial class CgppPhonebookMetadata
    {

        [Required]
        [DataType(DataType.PhoneNumber)]

        public string CellphoneNum { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Position { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Remarks { get; set; }
    }


    [MetadataType(typeof(CgppOfficeMetadata))]
    public partial class CgppOffice
    {
    }
    public partial class CgppOfficeMetadata
    {
        [MaxLength(50)]
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

    }

    [MetadataType(typeof(CgppDivisionMetadata))]
    public partial class CgppDivision
    {
    }
    public partial class CgppDivisionMetadata
    {
        [MaxLength(50)]
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

    }

    [MetadataType(typeof(CgppDraftMetadata))]
    public partial class Draft
    {
    }
    public partial class CgppDraftMetadata
    {
        [Required]
        [DataType(DataType.PhoneNumber)]

        public string Sendto { get; set; }

    }









}
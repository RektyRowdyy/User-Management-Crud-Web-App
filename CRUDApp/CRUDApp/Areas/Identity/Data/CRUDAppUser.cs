using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRUDApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CRUDAppUser class
public class CRUDAppUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")] 
    public string LastName { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime DOB { get; set; }

    public string Gender { get; set; }

}


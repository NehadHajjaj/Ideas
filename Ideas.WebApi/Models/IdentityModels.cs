using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Ideas.WebApi.Models.Enum;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ideas.WebApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Gender? Gender { get; private set; }
        public DateTime? BirthDate { get; private set; }
        public string Nationality { get; private set; }
        public string BirthCountry { get; private set; }
        public string Mobile { get; private set; }
        public string AcademicQualification { get; private set; }
        public bool? IsStudent { get; private set; }


        public void SetData(UserInfo userData)
        {
	        this.FirstName = userData.FirstName;
	        this.LastName = userData.LastName;
            this.BirthCountry = userData.BirthCountry;
            //this.BirthDate = userData.BirthDate;
            this.Gender = (Gender?) userData.Gender;
            this.Mobile = userData.Mobile;
            this.AcademicQualification = userData.AcademicQualification;
            this.Nationality = userData.Nationality;
        }

        public void SetIsStudent(bool modelIsStudent)
        {
	        this.IsStudent = modelIsStudent;
        }

        public void SetNewEmail(string modelNewEmail)
        {
	        this.UserName = modelNewEmail;
	        this.Email = modelNewEmail;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("IdeasDatabaseConnectionNew", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
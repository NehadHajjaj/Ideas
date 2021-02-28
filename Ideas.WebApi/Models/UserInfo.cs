using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ideas.WebApi.Models
{
	public class UserInfo
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Gender { get; set; }
		public string Nationality { get; set; }
		public string BirthCountry { get; set; }
		public string Mobile { get; set; }
		public string AcademicQualification { get; set; }

    }
}
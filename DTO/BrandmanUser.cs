using System;

namespace Brandman.UITests.DTO {
	[Serializable]
	public class BrandmanUser {
		public string GeneralZipCode { get; set; }
		public string LemooreZipCode { get; set; }
		public string TravisZipCode { get; set; }
		public string DegreeType { get; set; }
		public string AreaOfInterest { get; set; }
		public string Program { get; set; }
		public string Session { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool isLemoore { get; set; }
		public bool isTravis { get; set; }
		public string NursingDegreeType { get; set; }
		public string NursingProgram { get; set; }
		public string NursingSession { get; set; }
		public string NursingAreaOfInterest { get; set; }
	}
}
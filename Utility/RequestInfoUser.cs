using System;

namespace Brandman.UITests.Utility {
	public static class RequestInfoUser {
		#region Methods
		public static string GetEmail(string initialEmail) {
			if (!string.IsNullOrEmpty(initialEmail)) {
				var emailArray = initialEmail.Split('@');
				return emailArray[0] + DateTime.Now.ToString("ddMMyyyyhhmm") + "@" + emailArray[1];
			}
			return initialEmail;
		}
		#endregion
	}
}
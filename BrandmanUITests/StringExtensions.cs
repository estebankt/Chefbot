using System;
using System.Linq;

namespace Brandman.UITests {
	public static class StringExtensions {
		#region Private Methods
		private static string[] Merge(string[] front, string[] back) {
			var ret = front;
			if (back?.Length > 0) {
				ret = new string[front.Length + back.Length];
				Array.Copy(front, 0, ret, 0, front.Length);
				Array.Copy(back, 0, ret, front.Length, back.Length);
			}
			return ret;
		}
		private const string ScrubChars = "/-&,().\n\r";
		private static string Scrub(string val) {
			return ScrubChars.ToCharArray()
				.Aggregate(val,
					(current, c) =>
						current.Replace(new string(c, 1), string.Empty)
				)
				.ToLower();
		}
		#endregion

		#region Public Methods
		/// <summary>
		///     Merge a set of string parameters to the beginning of an existing array, returning a new array.
		/// </summary>
		/// <param name="original">Required</param>
		/// <param name="additional">Optional</param>
		/// <returns></returns>
		public static string[] Prepend(this string[] original, params string[] additional) {
			return Merge(additional, original);
		}
		/// <summary>
		///     Merge a set of string parameters to the end of an existing array, returning a new array.
		/// </summary>
		/// <param name="original">Required</param>
		/// <param name="additional">Optional</param>
		/// <returns></returns>
		public static string[] Append(this string[] original, params string[] additional) {
			return Merge(original, additional);
		}
		/// <summary>
		/// </summary>
		/// <param name="val"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool ContainsSimilar(this string val, string other) {
			var otherScrubbed = Scrub(other);
			var meScrubbed = Scrub(val);
			return meScrubbed.Contains(otherScrubbed);
		}
		#endregion
	}
}
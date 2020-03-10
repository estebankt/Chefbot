using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Oshyn.Framework.UITesting.Element;
using Oshyn.Framework.UITesting.Info;
using Oshyn.Framework.UITesting.Page;

namespace Brandman.UITests.Pages {
	/// <summary>
	///     Base page for Brandman
	/// </summary>
	public class BasePage : AssertingPageBase {
		#region Fields
		private static By _errorBy;
		private static readonly Dictionary<string, int> _seenLinks = new Dictionary<string, int>();
		#endregion

		#region Properties
		private IWebElement[] FooterLinkElements {
			get {
				var control = Settings.Controls.GetByFullKey("AnyPage.FooterLinks");
				return UnseenLinkElements(Driver.FindElements(control.GetSearchBy()).ToArray());
			}
		}
		private IWebElement[] MenuLinkElements {
			get {
				var control = Settings.Controls.GetByFullKey("AnyPage.MobileMenuLinks");
				return UnseenLinkElements(Driver.FindElements(control.GetSearchBy()).ToArray());
			}
		}
		private IWebElement[] ScreenLinkElements {
			get {
				var control = Settings.Controls.GetByFullKey("AnyPage.Links");
				return UnseenLinkElements(Driver.FindElements(control.GetSearchBy())
					.Except(MenuLinkElements)
					.Except(FooterLinkElements)
					.ToArray());
			}
		}
		#endregion

		#region Constructors
		protected BasePage(
			string pageName,
			string urlPath,
			IWebDriver driver,
			ISettings settings
		)
			: base(pageName, urlPath, driver, settings) { }
		protected BasePage(string pageName, IWebDriver driver, ISettings settings)
			: base(pageName, driver, settings) { }
		protected BasePage(IWebDriver driver, ISettings settings)
			: base(driver, settings) { }
		#endregion

		#region Private Methods
		private static IWebElement[] UnseenLinkElements(IWebElement[] proposedElements) {
			List<IWebElement> ret = null;
			if (null != proposedElements) {
				ret = new List<IWebElement>(proposedElements.Length);
				foreach (var element in proposedElements) {
					var href = element.GetAttribute("href");
					if (null != href) {
						if (TryAddUnseenLink(href)) {
							ret.Add(element);
						}
					}
				}
			}
			return ret?.ToArray();
		}
		private static bool TryAddUnseenLink(string linkUrl) {
			var ret = false;
			lock (_seenLinks) {
				if (!_seenLinks.ContainsKey(linkUrl)) {
					_seenLinks.Add(linkUrl, 1);
					ret = true;
				}
			}
			return ret;
		}
		#endregion

		#region Public Methods
		// ReSharper disable once OptionalParameterHierarchyMismatch
		public override IWebElement AssertClick(string controlKey, string message = null, int? timeoutSeconds = null) {
			var ret = base.AssertClick(controlKey, message, timeoutSeconds);
			TryAddUnseenLink(Driver.Url);
			return ret;
		}

		public void AssertAllScreenLinksAreValid() {
			_errorBy = _errorBy ?? GetControlByKey("AnyPage.ErrorElement").GetSearchBy();
			AssertAllLinksLoad(ScreenLinkElements, _errorBy);
		}
		public void AssertAllMenuLinksAreValid() {
			_errorBy = _errorBy ?? GetControlByKey("AnyPage.ErrorElement").GetSearchBy();
			AssertAllLinksLoad(MenuLinkElements, _errorBy);
		}
		public void AssertAllFooterLinksAreValid() {
			_errorBy = _errorBy ?? GetControlByKey("AnyPage.ErrorElement").GetSearchBy();
			AssertAllLinksLoad(FooterLinkElements, _errorBy);
		}

		/// <summary>
		///     Navigates to all elements in a collection as new tabs, comparing links text to destination hero
		///     This method extracts the href attributes and travels to target pages.
		///     ///
		/// </summary>
		/// <param name="controlListKey">Key of the control list</param>
		/// <param name="targetKey">target key </param>
		/// <param name="textIndex">It allows to validate the source/target link texts</param>
		public void CanNavigateToAllItemsAtList(string controlListKey, string targetKey, int? textIndex = null) {
			var col = ExtendedElement.BuildFromParentKey(this, controlListKey);
			foreach (var item in col) {
				MemorizeExistingTabs();
				var linkText = textIndex.HasValue
					? item.Element.Text.Split('\n')[textIndex.Value]
					: item.Element.Text;
				TryControlClick(item.Element);
				SwitchToNewestTab();
				var destinationElement = AssertElementExists(targetKey,
					$"element [{linkText}] is a broken link");
				var destinationText = destinationElement.Text;
				if (textIndex.HasValue) {
					linkText.ContainsSimilar(destinationText);
				}
				ReturnToPriorTab(true);
			}
		}
		#endregion
	}
}
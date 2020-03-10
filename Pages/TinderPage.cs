using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Brandman.UITests.DTO;
using Brandman.UITests.Utility;
using OpenQA.Selenium;
using Oshyn.Framework.UITesting.Info;
using Oshyn.Framework.UITesting.Selenium;

namespace Brandman.UITests.Pages {
	public class TinderPage : BasePage {

        #region Constructor

        public TinderPage
        (
            IWebDriver driver,
            ISettings settings
        )
            : base("TinderPage", String.Empty, driver, settings)
        {
            settings.BrowsingDelayMilliseconds = 500;
        }
        #endregion

        #region Private Methods
        private void SetTextBox(string element, string value)
        {
            var webElement = AssertElementExists(element);
            webElement.SetTextBoxValue(value);
        }
        #endregion

        #region Public Methods

        public void LoginFacebook()
        {
            Thread.Sleep(2000);
            AssertClick("login_facebook_button");

            string popupHandle = string.Empty;
            string currentWindow = Driver.CurrentWindowHandle;
            ReadOnlyCollection<string> handles = Driver.WindowHandles;

            foreach (string handle in handles)
            {
                if (handle != currentWindow)
                {
                    popupHandle = handle; break;
                }
            }
            Driver.SwitchTo().Window(popupHandle);
            SetTextBox("login_email", "estebankt@hotmail.com");
            SetTextBox("login_password", "Aristoteles0112");
            TryClick("submit_facebook_button");
            Driver.SwitchTo().Window(currentWindow);
            Thread.Sleep(2000);
            AssertClick("allow_location_button");
            AssertClick("disable_notifications_button");
        }

        public void LikePerson()
        {
            AssertClick("like_button");
            Thread.Sleep(randomtime());
        }

        private int randomtime()
        {
            Random random = new Random();
            return (500 + random.Next(500, 1000));
        }

        private bool randomLike()
        {
            var ret = false;
            Random random = new Random();
            if (random.Next(0, 10) > 8)
                ret = true;
            return ret;
        }

        public void CheckForPopUpsAndClose()
        {
            var listOfElements = Driver.FindElements(By.XPath("//*[@id='modal-manager']/div/div/div[2]/button[2]"));
            if (listOfElements.Any())
            {
                listOfElements.First().Click();
            }
        }

        private void switchToNewWindow()
        {
            string popupHandle = string.Empty;
            string currentWindow = Driver.CurrentWindowHandle;
            ReadOnlyCollection<string> handles = Driver.WindowHandles;

            foreach (string handle in handles)
            {
                if (handle != currentWindow)
                {
                    popupHandle = handle; break;
                }
            }
            Driver.SwitchTo().Window(popupHandle);
        }
        #endregion

        public bool CheckforOutOfLikes()
        {
            var ret = false;
            var listOfElements = Driver.FindElements(By.XPath("//*[@id='modal - manager']/div/div/div[3]/button[2]"));
            if (listOfElements.Any())
            {
                listOfElements.First().Click();
                ret = true;
            }

            return ret;
        }
    }
}
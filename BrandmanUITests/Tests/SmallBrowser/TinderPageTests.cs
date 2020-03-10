using Brandman.UITests.DTO;
using Brandman.UITests.Pages;
using NUnit.Framework;
using System.Configuration;
using System.Threading;
using System.Web.UI.WebControls;

namespace Brandman.UITests.Tests.SmallBrowser {
	public class TinderPageTests : TestBaseSmall<TinderPage> {

		#region Fields
		private DtoFactory<BrandmanUser> _factory;
		private BrandmanUser _newUser;
		#endregion

		#region Instrumentation
		[OneTimeSetUp]
		public void OneTimeSetUp() {
			_factory = new DtoFactory<BrandmanUser>();
			_newUser = _factory.LoadFromFile(@".\TestData\BrandmanUser.xml");
		}
        #endregion

        #region Tests
        [Test]
        public void LoginAndSwipe()
        {
            Page.LoginFacebook();
            for (int i = 0; i < 100; i++)
            {
                Page.CheckForPopUpsAndClose();
                if (Page.CheckforOutOfLikes())
                {
                    break;
                }
                Page.LikePerson();
            }
        }


        #endregion
    }
}
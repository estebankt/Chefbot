using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Oshyn.Framework.UITesting.Page;

namespace Brandman.UITests.Tests {
	public abstract class TestBaseSmall<T> : TestBase<T> where T : class, IPage {
		[SetUp]
		public void Setup() {
			Setup(PageBase.SmallBrowser);
		}
        [TearDown]
        public void TearDownLocal()
        {
            TearDown();
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success &&
                Page?.Driver != null)
            {
                Page.Driver.Close();
                Page.Driver.Dispose();
                Page = null;
            }
        }
    }
}
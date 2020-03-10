using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Oshyn.Framework.UITesting.Page;
using FW = Oshyn.Framework.UITesting.NUnit;

namespace Brandman.UITests.Tests {
	public abstract class TestBase<T> : FW.TestBase<T> where T : class, IPage {
		[OneTimeSetUp]
		public void SetupAll() {
			OneTimeSetUp();
		}
		[TearDown]
		public void Teardown() {
			TearDown();
			if (TestContext.CurrentContext.Result.Outcome != ResultState.Success &&
				Page?.Driver != null) {
				Page.Driver.Close();
				Page.Driver.Dispose();
				Page = null;
			}
		}
		[OneTimeTearDown]
		public void TearDownAll() {
			if (Page?.Driver != null) {
				Page.Driver.Close();
				Page.Driver.Dispose();
				Page.Driver = null;
				Page = null;
			}
			OneTimeTearDown();
		}
	}
}
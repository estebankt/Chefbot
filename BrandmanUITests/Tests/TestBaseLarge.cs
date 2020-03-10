using NUnit.Framework;
using Oshyn.Framework.UITesting.Page;

namespace Brandman.UITests.Tests {
	public abstract class TestBaseLarge<T> : TestBase<T> where T : class, IPage {
		[SetUp]
		public void Setup() {
			Setup(PageBase.LargeBrowser);
		}
	}
}
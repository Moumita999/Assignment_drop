using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumWebDriverTests.Pages;

namespace SeleniumWebDriverTests
{
    [TestFixture]
    public class TelenorTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void TestOpentelenorsite()
        {
            driver.Navigate().GoToUrl("https://www.telenor.se");
            driver.Manage().Window.Maximize();
            Assert.IsTrue(driver.Title.Contains("Telenor"));

            var homePage = new TelenorHomePage(driver);
            homePage.AcceptCookies();
            homePage.ClickHandla();
            homePage.ClickBredband();

            var searchPage = new TelenorSearchPage(driver);
            searchPage.SearchAddress("Storgatan 1, Uppsala");
            searchPage.SelectDropdownOption();

            string headingText = searchPage.GetGridItemHeadingText();
            Assert.That(headingText, Does.Contain("Bredband via 5G"), "Text content contains 'Bredband via 5G'");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
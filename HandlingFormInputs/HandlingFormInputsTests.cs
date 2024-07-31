using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;

namespace HandlingFormInputs
{
    public class HandlingFormInputsTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArguments("--disable-search-engine-choice-screen");
            options.AddArgument("--no-first-run");
            options.AddArgument("--no-default-browser-check");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-default-apps");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
        }

        [Test]
        public void Test_RegisterUser()
        {
            // Click on My Account link
            driver.FindElement(By.LinkText("My Account")).Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            // Fill in the form
            driver.FindElement(By.CssSelector("input[type='radio'][value='f']")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys("Fiona");
            driver.FindElement(By.Name("lastname")).SendKeys("Apple");
            driver.FindElement(By.Id("dob")).SendKeys("09/13/1977");

            // Generate a unique email address
            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
            string email = $"fiona.apple{num}@example.com";

            driver.FindElement(By.Name("email_address")).SendKeys(email);
            driver.FindElement(By.Name("company")).SendKeys("Example Inc.");
            driver.FindElement(By.Name("street_address")).SendKeys("123 Main Str.");
            driver.FindElement(By.Name("suburb")).SendKeys("London");
            driver.FindElement(By.Name("postcode")).SendKeys("223242");
            driver.FindElement(By.Name("city")).SendKeys("London");
            driver.FindElement(By.Name("state")).SendKeys("London");

            // Select country from dropdown
            SelectElement countryDropdown = new SelectElement(driver.FindElement(By.Name("country")));
            countryDropdown.SelectByText("United Kingdom");

            driver.FindElement(By.Name("telephone")).SendKeys("2432424112");
            driver.FindElement(By.Name("newsletter")).Click();
            driver.FindElement(By.Name("password")).SendKeys("fiona_123456");
            driver.FindElement(By.Name("confirmation")).SendKeys("fiona_123456");

            // Submit the form
            driver.FindElement(By.Id("tdb4")).Click();

            // Assert account creation success
            Assert.IsTrue(driver.PageSource.Contains("Your Account Has Been Created!"), "Account creation failed.");

            // Click on Log Off link
            driver.FindElement(By.LinkText("Log Off")).Click();

            // Click on Continue button
            driver.FindElement(By.LinkText("Continue")).Click();

            Console.WriteLine("User Account Created with email: " + email);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}

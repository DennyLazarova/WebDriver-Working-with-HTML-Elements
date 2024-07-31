using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace WorkingWithWebTable
{
    public class WebTablesTests
    {
        WebDriver driver;

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
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); 

            
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void WorkingWithTableElements()
        {
            // Locate the table
            IWebElement productstable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            // Use FindElements to get a collection of rows
            IReadOnlyCollection<IWebElement> tableRows = productstable.FindElements(By.XPath("//tbody//tr"));

            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.csv";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (IWebElement row in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.XPath(".//td"));

                if (tableData.Count > 1)  // Ensure there are at least 2 cells to avoid IndexOutOfRangeException
                {
                    string data = tableData[0].Text + ", " + tableData[1].Text;
                    File.AppendAllText(path, data + "\n");

                    Assert.IsTrue(File.Exists(path));
                    Assert.IsTrue(new FileInfo(path).Length > 0);
                }
            }
        }
    }
}

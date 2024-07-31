using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DropDownManipulations
{
    public class DropDownManipulations
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
        public void ExtractInformationBasedOnDropDownOptions()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/manufacturers.txt";

            SelectElement dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

            IList<IWebElement> options = dropdown.Options;

            List<string> optionsAsString = new List<string>();

            foreach (var option in options)
            {
                optionsAsString.Add(option.Text);
            }

            optionsAsString.RemoveAt(0);

            foreach (var option in optionsAsString)
            {
                dropdown = new SelectElement(driver.FindElement(By.XPath("//form[@name='manufacturers']//select")));

                dropdown.SelectByText(option);
                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {option} has no product");
                }
                else
                {
                    IWebElement productsTable = driver.FindElement(By.ClassName("productListingData"));

                    File.AppendAllText(path, $"\n\n The manufacture {option} products are listed below -- \n");

                    IReadOnlyCollection<IWebElement> tableRows = productsTable.FindElements(By.XPath("//tbody/tr"));

                    foreach (var row in tableRows)
                    {
                        File.AppendAllText(path, row.Text + "\n");
                    }

                }
            }
        }
    }
}
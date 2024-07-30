using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace WorkingWithWebTable
{
    public class WebTablesTests
    {
        WebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://practice.bpbonline.com/ ");
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
            //locate the table
            IWebElement productstable = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            IReadOnlyCollection<IWebElement> tableRows = (IReadOnlyCollection<IWebElement>)productstable.FindElement(By.XPath("//tbody//tr"));

            string path = System.IO.Directory.GetCurrentDirectory() + "/productinformation.scv";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (IWebElement row in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.XPath(".//td"));

                foreach (var tData in tableData)
                {
                    string data = tData.Text;
                    string[] productInfo = data.Split("\n");

                    File.AppendAllText(path, productInfo[0].Trim() + ", " + productInfo[1].Trim() + "\n");

                    Assert.IsTrue(File.Exists(path));
                    Assert.IsTrue(new FileInfo(path).Length > 0);
                }

            }
        }
    }
}
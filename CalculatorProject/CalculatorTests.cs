using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CalculatorProject
{
    public class CalculatorTests
    {
        WebDriver driver;
        IWebElement textBoxNumber1;
        IWebElement textBoxNumber2;
        IWebElement dropdownOperation;
        IWebElement calcButton;
        IWebElement resetButton;
        IWebElement divResult;

        [OneTimeSetUp]

        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/ ");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }


        [SetUp]
        public void Setup()
        {
                   
            textBoxNumber1 = driver.FindElement(By.Id("number1"));
            textBoxNumber2 = driver.FindElement(By.Id("number2"));
            dropdownOperation = driver.FindElement(By.XPath("//label[@for='operation']//following-sibling::select"));
            calcButton = driver.FindElement(By.Id("calcButton"));
            resetButton = driver.FindElement(By.Id("resetButton"));
            divResult = driver.FindElement(By.Id("result"));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }



        //параметризирани тестове
        //ще създадем метод, който ще изпълнява основното действие- ще извършва операцията и ще асъртва резултата
        public void PerformTestLogic(string firstNumber, string secondNumber, string operation, string expected)
        {
            //click reset button
            resetButton.Click();

            if (!string.IsNullOrEmpty(firstNumber))
            {
                textBoxNumber1.SendKeys(firstNumber);
            }

            if (!string.IsNullOrEmpty(secondNumber))
            {
                textBoxNumber2.SendKeys(secondNumber);
            }

            if (!string.IsNullOrEmpty(operation))
            {
               new SelectElement(dropdownOperation).SelectByText(operation);
            }

            calcButton.Click();

            Assert.That(divResult.Text, Is.EqualTo(expected));
        }

        [Test]
        [TestCase ("5", "10", "+ (sum)", "Result: 15")]
        [TestCase ("5", "5", "+ (sum)", "Result: 10")]
        [TestCase ("5", "15", "+ (sum)", "Result: 20")]

        public void Test1(string firstNumber, string secondNumber, string operation, string expected)
        {
            PerformTestLogic(firstNumber, secondNumber, operation, expected);
        }
    }
}
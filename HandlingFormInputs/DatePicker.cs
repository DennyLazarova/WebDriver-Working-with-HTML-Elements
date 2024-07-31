using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HandlingFormInputs
{
    internal class DatePicker
    {
        private IWebDriver driver;
        private By monthDropdown = By.ClassName("ui-datepicker-month");
        private By yearDropdown = By.ClassName("ui-datepicker-year");

        public DatePicker(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void OpenDatePicker(By triggerLocator)
        {
            // Click the element that triggers the date picker
            driver.FindElement(triggerLocator).Click();
        }

        public void SelectMonth(string month)
        {
            IWebElement monthElement = driver.FindElement(monthDropdown);
            SelectElement selectMonth = new SelectElement(monthElement);
            selectMonth.SelectByText(month);
        }

        public void SelectYear(string year)
        {
            IWebElement yearElement = driver.FindElement(yearDropdown);
            SelectElement selectYear = new SelectElement(yearElement);
            selectYear.SelectByText(year);
        }

        public void SelectDay(int day)
        {
            By dayLocator = By.XPath(string.Format("//table[@class='ui-datepicker-calendar']//a[text()='{0}']", day));
            IWebElement dayElement = driver.FindElement(dayLocator);
            dayElement.Click();
        }

        public void SelectDate(string month, string year, int day)
        {
            SelectMonth(month);
            SelectYear(year);
            SelectDay(day);
        }
    }
}


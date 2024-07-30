using OpenQA.Selenium;

namespace HandlingFormInputs
{
    internal class SelectElement
    {
        private IWebElement webElement;

        public SelectElement(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        internal void SelectByText(string v)
        {
            throw new NotImplementedException();
        }
    }
}
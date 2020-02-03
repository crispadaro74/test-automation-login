using OpenQA.Selenium;
using System;

namespace NUnitTestProjectHudlLogin.PageObjects
{
    /// <summary>
    /// Page Object encapsulates the Home Page.
    /// </summary>
    class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsLoggedUser(string mail)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath($"//div[text()='{mail}']"));
            return true;
        }
    }
}

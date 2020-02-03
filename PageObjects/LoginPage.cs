using OpenQA.Selenium;

namespace NUnitTestProjectHudlLogin.PageObjects
{
    /// <summary>
    /// Page Object encapsulates the Login page.
    /// </summary>
    class LoginPage
    {
        private readonly IWebDriver driver;

        public enum LoginMode
        {
            Click,
            Enter
        }

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            
            this.InitElements();
        }

        private IWebElement UserName { get; set; }

        private IWebElement Password { get; set; }

        private IWebElement LogIn { get; set; }

        private IWebElement NeedHelp { get; set; }

        public void LoginUser(string email, string pwd, LoginMode mode = LoginMode.Click)
        {
            this.UserName.SendKeys(email);
            this.Password.SendKeys(pwd);
            if (mode == LoginMode.Enter)
            {
                this.Password.SendKeys(Keys.Enter);
            }
            else
            {
                this.LogIn.Click();
            }
        }

        public void LoginUser(string email, string pwd, string forward)
        {
            this.UserName.SendKeys(email);
            this.Password.SendKeys(pwd);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"var element = document.getElementById('forward'); element.value = '{forward}';");
            this.LogIn.Click();
        }

        public bool IsLoginFailed()
        {
            this.driver.FindElement(By.CssSelector(".login-error-container"));
            return true;
        }

        public void ResetPassword()
        {
            this.NeedHelp.Click();
        }

        public void ResetPasswordWithMail(string email)
        {
            this.UserName.SendKeys(email);
            this.NeedHelp.Click();
        }

        public bool IsResetPassword(string email)
        {
            this.driver.FindElement(By.Id("resetBtn"));
            return this.driver.FindElement(By.Id("forgot-email")).GetAttribute("value") == email;
        }

        public void ClearCredentials()
        {
            this.UserName.SendKeys(string.Empty);
            this.Password.SendKeys(string.Empty);
        }

        public void SelectRememberMe()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"var element = document.getElementById('remember-me'); element.checked=true;");
        }

        public bool IsSelectedRememberMe()
        {
            return this.driver.FindElement(By.Id("remember-me")).Selected;
        }

        /// <summary>
        /// Initialize elements.
        /// </summary>
        private void InitElements()
        {
            UserName = this.driver.FindElement(By.Id("email"));
            Password = this.driver.FindElement(By.Id("password"));
            LogIn = this.driver.FindElement(By.Id("logIn"));
            NeedHelp = this.driver.FindElement(By.Id("forgot-password-link"));
        }
    }
}

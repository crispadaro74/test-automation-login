using NUnit.Framework;
using NUnitTestProjectHudlLogin.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NUnitTestProjectHudlLogin
{
    /// <summary>
    /// 1. Setup an automation environment on your local machine using selenium
    /// 2. Automate any cases that you would think are good to test the functionality
    ///    of validating logging into hudl.com with your credentials.
    /// 3. Push your tests to a github repository (a public repo is fine) and share the link
    /// </summary>
    public class LoginTest
    {
        private IWebDriver driver;
        const string START_URL = "https://www.hudl.com/login";
        const string RIGHT_EMAIL = "<INSERT_MAIL_HERE>";
        const string RIGHT_PWD = "<INSERT_PWD_HERE>";
        const string WRONG_EMAIL = "no.name@gmail.com";
        const string WRONG_PWD = "Zer0Zer0Zer0";
        const string CASUAL_PWD = "Zer01Zer01Zer01";
        const string FORWARD_EXAMPLE = "explore";

        [SetUp]
        public void Setup()
        {
            // Download ChromeDriver and place it in the "C:\WebDriver\bin" path
            driver = new ChromeDriver(@"C:\WebDriver\bin");
        }

        /// <summary>
        /// Verify if a user will be able to login with valid credentials and clicking
        /// on the Login button.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_CorrectCredentials_SuccessLoginWithClick()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(RIGHT_EMAIL, RIGHT_PWD);
            HomePage hp = new HomePage(driver);
            Assert.IsTrue(hp.IsLoggedUser(RIGHT_EMAIL));
        }

        /// <summary>
        /// Verify if a user will be able to login with valid credentials and
        /// pressing Enter key.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_CorrectCredentials_SuccessLoginWithEnter()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(RIGHT_EMAIL, RIGHT_PWD, LoginPage.LoginMode.Enter);
            HomePage hp = new HomePage(driver);
            Assert.IsTrue(hp.IsLoggedUser(RIGHT_EMAIL));
        }

        /// <summary>
        /// Verify if a user cannot login with an invalid username.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_WrongEMail_FailLogin()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(WRONG_EMAIL, CASUAL_PWD);
            Assert.IsTrue(loginPage.IsLoginFailed());
        }

        /// <summary>
        /// Verify if a user cannot login with a valid username and an invalid password.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_WrongPwd_FailLogin()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(RIGHT_EMAIL, WRONG_PWD);
            Assert.IsTrue(loginPage.IsLoginFailed());
        }

        /// <summary>
        /// Verify if a user cannot login with blank credentials.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_BlankCredentials_FailLogin()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(string.Empty, string.Empty);
            Assert.IsTrue(loginPage.IsLoginFailed());
        }

        /// <summary>
        /// Verify if a user cannot login with a blank username.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_BlankEMail_FailLogin()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(string.Empty, CASUAL_PWD);
            Assert.IsTrue(loginPage.IsLoginFailed());
        }

        /// <summary>
        /// Verify if a user cannot login with a valid username and a blank password.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_BlankPwd_FailLogin()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(RIGHT_EMAIL, string.Empty);
            Assert.IsTrue(loginPage.IsLoginFailed());
        }

        /// <summary>
        /// Verify if a user will be able to login with a valid username, valid password
        /// and a target landing page.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void Enter_CorrectCredentialsAndForward_SuccessLogin()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginUser(RIGHT_EMAIL, RIGHT_PWD, FORWARD_EXAMPLE);
            HomePage hp = new HomePage(driver);
            Assert.IsTrue(hp.IsLoggedUser(RIGHT_EMAIL) && driver.Url.Contains(FORWARD_EXAMPLE));
        }

        /// <summary>
        /// Verify the "Need help?" functionality, without indicating the email.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void ClearCredentials_Then_Click_NeedHelp_ResetPwdWithNoMail()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.ClearCredentials();
            loginPage.ResetPassword();
            Assert.IsTrue(loginPage.IsResetPassword(string.Empty));
        }

        /// <summary>
        /// Verify the "Need help?" functionality, indicating the e-mail.
        /// </summary>
        [Test]
        [Category("Functional")]
        public void ClearCredentials_Then_Click_NeedHelp_ResetPwdWithMail()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.ResetPasswordWithMail(RIGHT_EMAIL);
            Assert.IsTrue(loginPage.IsResetPassword(RIGHT_EMAIL));
        }

        [Test]
        [Category("GUI")]
        public void Default_RememberMe_IsNotSelected()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            Assert.IsFalse(loginPage.IsSelectedRememberMe());
        }

        [Test]
        [Category("GUI")]
        public void Select_RememberMe_IsSelected()
        {
            driver.Url = START_URL;
            LoginPage loginPage = new LoginPage(driver);
            loginPage.SelectRememberMe();
            Assert.IsTrue(loginPage.IsSelectedRememberMe());
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}
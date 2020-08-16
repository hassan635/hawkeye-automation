using System;
using MTData_Automation.Helpers;
using OpenQA.Selenium;

namespace MTData_Automation.POM
{
    public class LoginPage
    {
        private IWebDriver _driver = null;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public LoginPage PerformLogin(string loginUrl, string username, string password)
        {
            try
            {
                Console.WriteLine("Attempting to login to Hawk-Eye...");
                _driver.Navigate().GoToUrl(loginUrl);
                _driver.FindElement(By.Id("Username"), 5).SendKeys(username);
                _driver.FindElement(By.Id("Password"), 5).SendKeys(password);
                _driver.FindElement(By.CssSelector("input[value='Log in']"), 5).Click();
                _driver.FindElement(By.CssSelector("li[class='modeGrid']"), 5).Click();
                Console.WriteLine("Login successful!");
                return this;
            }
            catch(ElementNotInteractableException e)
            {
                throw new ElementNotInteractableException("Login failed due to error: " + e.Message);
            }
            catch(Exception e)
            {
                throw new Exception("Login failed due to error: " + e.Message); ;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using MTData_Automation.Helpers;
using OpenQA.Selenium;

namespace MTData_Automation.POM
{
    public class VehiclePage
    {
        private IWebDriver _driver = null;

        public VehiclePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public VehiclePage SearchForVehicle(string vehicleName)
        {
            try
            {
                Console.WriteLine($"Attempting to search for vehicle {vehicleName} ...");
                IWebElement assetSearchInput = _driver.FindElement(By.Id("assetSearch"), 5);
                assetSearchInput.Clear();
                assetSearchInput.SendKeys(vehicleName);
                _driver.FindElement(By.XPath($"//td[contains(text(),'{vehicleName}')]"), 5).Click();
                Console.WriteLine("Vehicle search successful!");
                return this;
            }
            catch (ElementNotInteractableException e)
            {
                throw new ElementNotInteractableException("Vehicle search failed due to error: " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Vehicle search failed due to error: " + e.Message); ;
            }
        }

        public VehiclePage ViewVehicleHistory(string historyStartDate, string historyEndDate)
        {
            try
            {
                Console.WriteLine("Attempting to lookup vehicle history...");
                _driver.FindElement(By.Id("popupSetting"), 5).Click();
                _driver.FindElement(By.XPath("//div[contains(text(),'View History')]"), 5).Click();
                IWebElement IFrame = _driver.FindElement(By.CssSelector("iframe[src^='/V8/TrackingHistory/Add']"), 5);
                _driver.SwitchTo().Frame(IFrame);

                IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
                executor.ExecuteScript($"document.getElementById('StartDate').setAttribute('value', '{historyStartDate}');");
                executor.ExecuteScript($"document.getElementById('EndDate').setAttribute('value', '{historyEndDate}');");
                _driver.FindElement(By.Id("buttonShow"), 5).Click();
                Console.WriteLine("Vehicle history lookup successful");
                _driver.SwitchTo().DefaultContent();
                return this;
            }
            catch (ElementNotInteractableException e)
            {
                throw new ElementNotInteractableException("Vehicle history lookup failed due to error: " + e.Message); ;
            }
            catch (Exception e)
            {
                throw new Exception("Vehicle history lookup failed due to error: " + e.Message); ;
            }
        }

        public IReadOnlyCollection<IWebElement> GetVehicleHistoryRecords()
        {
            try
            {
                return _driver.FindElements(By.CssSelector("div[class='objbox'] > table > tbody > tr"), 5);
            }
            catch (ElementNotInteractableException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ReturnToVehicleList()
        {
            try
            {
                _driver.FindElement(By.CssSelector("div.tab.tabTracking"), 5).Click();
            }
            catch (ElementNotInteractableException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

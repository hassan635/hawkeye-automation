using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using NUnit.Framework;
using MTData_Automation.Helpers;
using MTData_Automation.POM;

namespace MTData_Automation
{
    class AutomatedTest
    {
        static void Main(string[] args)
        {
            IWebDriver driver = null;

            try
            {
                List<TestParams> testParamsList = TestParamsProvider.GetParams();

                driver = new ChromeDriver();

                LoginPage loginPage = new LoginPage(driver);
                loginPage.PerformLogin(
                    TestSettingsManager.GetSetting("appUrl"),
                    TestSettingsManager.GetSetting("username"),
                    TestSettingsManager.GetSetting("password"));

                VehiclePage vehiclePage = new VehiclePage(driver);

                foreach (TestParams testParams in testParamsList)
                {
                    vehiclePage
                        .SearchForVehicle(testParams.VehicleName)
                        .ViewVehicleHistory(testParams.HistoryStartDate, testParams.HistoryEndDate);

                    Assert.IsTrue(vehiclePage.GetVehicleHistoryRecords().Count > 1);

                    TakeScreenshot.Perform(driver, testParams.VehicleName);

                    vehiclePage.ReturnToVehicleList();
                }
            }
            catch (AssertionException a)
            {
                Console.WriteLine(a.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                driver.Quit();
            }

        }
    }
}

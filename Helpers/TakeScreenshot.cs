using System.Threading;
using OpenQA.Selenium;

namespace MTData_Automation.Helpers
{
    public static class TakeScreenshot
    {
        public static void Perform(IWebDriver driver, string imageName)
        {
            Thread.Sleep(3000);
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(imageName + ".png",
            ScreenshotImageFormat.Png);
        }
    }
}

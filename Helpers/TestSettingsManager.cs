using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace MTData_Automation.Helpers
{
    public static class TestSettingsManager
    {
        public static string GetSetting(string key)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestSettings.json");
            string paramsJson = File.ReadAllText(path);
            JObject parsedTestCases = JObject.Parse(paramsJson);
            return parsedTestCases[key].ToObject<string>();
        }
    }
}

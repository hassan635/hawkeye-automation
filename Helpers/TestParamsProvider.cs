using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MTData_Automation.Helpers
{
    public static class TestParamsProvider
    {
        public static List<TestParams> GetParams()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestParams.json");
            string paramsJson = File.ReadAllText(path);
            JObject parsedTestCases = JObject.Parse(paramsJson);
            List<JToken> testCases = parsedTestCases["testCases"].Children().ToList();

            List<TestParams> TestParamsList = new List<TestParams>();
            foreach (JToken testCase in testCases)
            {
                TestParams testParam = testCase.ToObject<TestParams>();
                TestParamsList.Add(testParam);
            }
            return TestParamsList;
        }
    }
}

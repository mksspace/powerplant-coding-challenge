using Newtonsoft.Json;
using Powerplant_coding_challenge.Controllers;
using Powerplant_coding_challenge.Model;
using Powerplant_coding_challenge.Service;
using System.Threading.Channels;

namespace TestPowerplant_coding_challenge
{

    public class UnitTestPowerplanLogic
    {
        //For debuging
        [Fact]
        public void TestPayloadCalulation()
        {
            string parentDirName = new FileInfo(AppDomain.CurrentDomain.BaseDirectory).Directory.FullName;
            string payloadsample = File.ReadAllText(Path.Combine(parentDirName, "Resources", "payloadSample.json"));

            PowerplantsInput ppInput = JsonConvert.DeserializeObject<PowerplantsInput>(payloadsample);
            Assert.True(ppInput.powerplants.Any(), "error while parsing payloadSample");

            List<PowerplantsResult> results = PowerplantLogic.Calculate(ppInput);
            Assert.True(results.Any());

            results.ForEach(p => Console.WriteLine(p));
        }

        [Fact]
        public static void TestPowerplantCalculationBehaviorIsCorrect() 
        {
            int load = 500;
            Powerplant powerplant = new()
            {
                Efficiency = 1,
                Name = "Test",
                Type = "gasfired",
                Pmax = 200,
                Pmin = 100
            };

            var resultGreaterLoad = PowerplantLogic.CalculatePowerplan(500, powerplant, new List<Powerplant>() { powerplant });
            var resultSmallerLoad = PowerplantLogic.CalculatePowerplan(90, powerplant, new List<Powerplant>() { powerplant });
            var resultBetweenLoad = PowerplantLogic.CalculatePowerplan(150, powerplant, new List<Powerplant>() { powerplant });

            Assert.Equal(300, resultGreaterLoad.loadLeft);
            Assert.Equal(0  , resultBetweenLoad.loadLeft);
            Assert.Equal(0  , resultSmallerLoad.loadLeft);


        }
    }
}
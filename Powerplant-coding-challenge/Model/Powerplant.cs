using Newtonsoft.Json;

namespace Powerplant_coding_challenge.Model
{

    public class Powerplant
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("efficiency")]
        public double Efficiency { get; set; }

        [JsonProperty("pmin")]
        public int Pmin { get; set; }

        [JsonProperty("pmax")]
        public int Pmax { get; set; }

        public double RealCost => Efficiency == 0 ? 0 : price / Efficiency;

        private double price = 0;

        public void SetValues(Fuels fuels)
        {
            switch (Type)
            {
                case "gasfired":
                    price = fuels.GaseuroMWh + (0.3 * fuels.GaseuroMWh * fuels.Co2euroton); //here we can also evaluate the co2 cost
                    break;

                case "turbojet":
                    price = fuels.KerosineeuroMWh;
                    break;
                case "windturbine":
                    price = 0.0001;
                    Pmax = Pmax * fuels.Wind / 100;
                    break;
            }
        }
    }
}

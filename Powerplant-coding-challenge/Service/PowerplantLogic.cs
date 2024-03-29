using Powerplant_coding_challenge.Model;

namespace Powerplant_coding_challenge.Service
{
    public class PowerplantLogic : IPowerplantLogic
    {

        public static List<PowerplantsResult> Calculate(PowerplantsInput root)
        {
            var powerplants = root.powerplants;
            powerplants.ForEach(p => p.SetValues(root.fuels));

            var ppsByMerit = powerplants.OrderBy(powerplant => powerplant.RealCost).ToList();

            List<PowerplantsResult> results = new List<PowerplantsResult>();

            int load = root.load;

            var ppsByMeritQueued = new Queue<Powerplant>(ppsByMerit);

            while (ppsByMeritQueued.Any())
            {
                var p = ppsByMeritQueued.Dequeue();
                var result = CalculatePowerplan(load, p, ppsByMerit);
                load = result.loadLeft;
                results.Add(result.powerplantsResult);

            }

            return results;
        }

        /// <summary>
        /// This methode will calculate the electricy quantity that powerplant "p" needs to provide
        /// </summary>
        /// <param name="load"></param>
        /// <param name="p"></param>
        /// <param name="left"> this collection is used select the best powerplant when the minimal production of powerplant "p" is to high </param>
        /// <returns></returns>
        public static (PowerplantsResult powerplantsResult, int loadLeft) CalculatePowerplan(int load, Powerplant p, List<Powerplant> left)
        {
            var result = new PowerplantsResult();

            if (load == 0)
            {
                result = new PowerplantsResult() { name = p.Name, p = 0 };
            }

            else if (load < p.Pmin)
            {
                var lastPowerplant = left
                    .OrderBy(powerplant => powerplant.Pmin * powerplant.RealCost)
                    .ThenBy(powerplant => load * powerplant.RealCost)
                    .First();
                result = new PowerplantsResult() { name = lastPowerplant.Name, p = lastPowerplant.Pmin };
                load = 0;
            }

            else if (load < p.Pmax)
            {
                result = new PowerplantsResult() { name = p.Name, p = load };
                load = 0;
            }

            else
            {
                result = new PowerplantsResult() { name = p.Name, p = p.Pmax };
                load = load - p.Pmax;
            }

            return (result, load);
        }

        public List<PowerplantsResult> Compute(PowerplantsInput root)
        {
            return Calculate(root);
        }
    }
}
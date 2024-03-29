namespace Powerplant_coding_challenge.Model
{
    public class PowerplantsInput
    {
        public int load { get; set; }
        public Fuels fuels { get; set; }
        public List<Powerplant> powerplants { get; set; }
    }
}

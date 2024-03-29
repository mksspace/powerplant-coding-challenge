using Powerplant_coding_challenge.Model;

namespace Powerplant_coding_challenge.Service
{
    public interface IPowerplantLogic
    {
        List<PowerplantsResult> Compute(PowerplantsInput root);
    }
}

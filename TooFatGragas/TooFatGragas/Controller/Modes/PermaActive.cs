using EloBuddy.SDK;
using TooFatGragas.Model;

namespace TooFatGragas.Controller.Modes
{
    internal class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (Barrel == null) return; // HAS TO BE LIKE THIS CAUSE EB SUX CAN'T USE NULL PROPAGATION
            if (Barrel.Position.CountEnemiesInRange(0x113) >= 1)
            {
                Q.Cast(Barrel.Position); // auto destroy
            }
        }
    }
}
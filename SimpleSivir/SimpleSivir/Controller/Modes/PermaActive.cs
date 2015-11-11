using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SimpleSivir.Helpers;
using SimpleSivir.Model;

namespace SimpleSivir.Controller.Modes
{
    internal class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Config.Misc.AutoQ;
        }

        public override void Execute()
        {
            if (Q.IsReady())
            {
                foreach (var target in EntityManager.Heroes.Enemies.Where(
                    x => x.IsValidTarget(Q.Range) && !x.HasBuffOfType(BuffType.Invulnerability)))
                {
                    var pred = Q.GetPrediction(target);
                    if (pred.HitChance >= HitChance.Immobile ||
                        pred.HitChance >= HitChance.Dashing)
                    {
                        Q.Cast(pred.CastPosition);
                    }
                }
            }
        }
    }
}
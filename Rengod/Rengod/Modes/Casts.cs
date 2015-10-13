using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Rengod.Core;

namespace Rengod.Modes
{
    internal class Casts : Model
    {
        public static void useE(AIHeroClient target)
        {
            if (E.IsReady() && _Player.Distance(target) <= E.Range && !Passive())
            {
                var pred = E.GetPrediction(target);
                if (pred.HitChance >= HitChance.High && !pred.CollisionObjects.Any())
                    E.Cast(pred.CastPosition);
            }
        }

        public static void useQ(AIHeroClient target)
        {
            if (!ObjectManager.Player.Spellbook.IsAutoAttacking &&
                _Player.Distance(target) <= Q.Range &&
                Q.IsReady())
            {
                Q.Cast();
            }
        }

        public static void useW(AIHeroClient target)
        {
            if (W.IsReady() && _Player.Distance(target) <= W.Range/3)
            {
                W.Cast();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace Veigar
{
    class Casts
    {
        public static void WCast(AIHeroClient target)
        {
            HitChance okay = HitChance.Medium;
            if (!target.IsValidTarget(Spells.W.Range) || !Spells.W.IsReady()) return;

            var pred = Spells.W.GetPrediction(target);
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                okay = Utils.getPredict(MenuX.Combo, "SliderCPredict");
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
                okay = Utils.getPredict(MenuX.Harass, "SliderHPredict");
            if (pred.HitChance >= okay)
                Spells.W.Cast(pred.CastPosition);

        }

        public static void QCast(AIHeroClient target)
        {
            HitChance okay = HitChance.Medium;

            if (!target.IsValidTarget(Spells.Q.Range) || !Spells.Q.IsReady()) return;

            var pred = Spells.W.GetPrediction(target);
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                okay = Utils.getPredict(MenuX.Combo, "SliderCPredict");
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
                okay = Utils.getPredict(MenuX.Harass, "SliderHPredict");
            var prediction = Spells.Q.GetPrediction(target);
            if (prediction.HitChance >= okay)
            {
                Spells.Q.Cast(prediction.CastPosition);
            }
        }

        /*
        Do again :{
        */
        public static void ECast(AIHeroClient target)
        {
            HitChance okay = HitChance.Medium;
            if (!target.IsValidTarget(Spells.E.Range) || !Spells.E.IsReady()) return;
            var prediction = Spells.E.GetPrediction(target);
            var pos = prediction.CastPosition;

            var pred = Spells.W.GetPrediction(target);
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                okay = Utils.getPredict(MenuX.Combo, "SliderCPredict");
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass)
                okay = Utils.getPredict(MenuX.Harass, "SliderHPredict");

            if (pos.Distance(Brain._Player.Position) <= Spells.E.Range &&
                prediction.HitChance >= okay)
            {
                var extends = Utils.getSliderValue(MenuX.Misc, "extension");
                Spells.E.Cast(pos.Extend(Utils._Player, extends).To3D());
            }
        }

        public static void RCast(AIHeroClient target)
        {
            if (target.HasBuffOfType(BuffType.Invulnerability)) return;
            if (!Spells.R.IsReady() || !target.IsValidTarget(Spells.R.Range)) return;
            if (DamageLib.RDamage(target) < target.Health) return;

            Spells.R.Cast(target);
        }

        public static void WTest()
        {
            foreach (var target in HeroManager.Enemies.Where(
                x => x.IsValidTarget(Spells.W.Range) && !x.HasBuffOfType(BuffType.Invulnerability)))
            {
                var pred = Spells.W.GetPrediction(target);
                if (pred.HitChance >= HitChance.Immobile || pred.HitChance >= HitChance.Dashing)
                {
                    Spells.W.Cast(pred.CastPosition);
                }
            }
        }
    }
}

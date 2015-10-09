using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace SwagZilean
{
    internal class Flags
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void Combo()
        {
            Brain.AutoR(); // ult on combo orly?
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null || !target.IsValid) return;

            var useQ = Utils.isChecked(MenuX.Combo, "comboQ");
            var useW = Utils.isChecked(MenuX.Combo, "comboW");
            var useE = Utils.isChecked(MenuX.Combo, "comboE");
            var hitC = Utils.getPredict(MenuX.Combo, "dPrediction");
            var cType = Utils.getSliderValue(MenuX.Combo, "whatcombo");
            if (useE && Spells.E.IsReady())
            {
                switch (cType)
                {
                    case 0:
                        Spells.E.Cast(_Player);
                        break;
                    case 1:
                        Spells.E.Cast(target);
                        break;
                    case 2:
                        var mostAd =
                            EntityManager.Heroes.Allies.OrderBy(x => x.FlatPhysicalDamageMod).
                                Where(x => x.Distance(_Player) <= Spells.E.Range);
                        Spells.E.Cast(mostAd.First());
                        break;
                }
            }
            if (useQ && Spells.Q.IsReady() && target.IsValidTarget())
            {
                var pred = Spells.Q.GetPrediction(target);
                if (pred.HitChance >= hitC)
                    Spells.Q.Cast(pred.CastPosition);
            }
            if (useW && Spells.W.IsReady() && Spells.Q.IsOnCooldown)
            {
                Spells.W.Cast();
                if (Spells.Q.IsReady())
                {
                    var pred = Spells.Q.GetPrediction(target);
                    if (pred.HitChance >= hitC)
                        Spells.Q.Cast(pred.CastPosition);
                }
            }
        }

        public static void Harass()
        {
            Brain.AutoR(); // ult on harass orly?
            var target = TargetSelector.GetTarget(Spells.Q.Range, DamageType.Magical);
            if (target == null || !target.IsValid) return;

            var useQ = Utils.isChecked(MenuX.Harass, "harassQ");
            var useW = Utils.isChecked(MenuX.Harass, "harrasW");
            var useE = Utils.isChecked(MenuX.Harass, "harrasE");
            var manaS = Utils.getSliderValue(MenuX.Harass, "hManaSlider");

            if (Spells.Q.IsReady() &&
                _Player.Distance(target) <= Spells.Q.Range &&
                useQ &&
                manaS <= _Player.ManaPercent)
            {
                var pred = Spells.Q.GetPrediction(target);
                if (pred.HitChance >= Utils.getPredict(MenuX.Combo, "dPrediction"))
                {
                    Spells.Q.Cast(pred.CastPosition);
                }
            }

            if (Spells.W.IsReady() &&
                useW &&
                manaS <= _Player.ManaPercent && Spells.Q.IsOnCooldown)
            {
                Spells.W.Cast();
            }

            if (Spells.E.IsReady() &&
                _Player.Distance(target) <= Spells.E.Range &&
                useE &&
                manaS <= _Player.ManaPercent)
            {
                Spells.E.Cast(target);
            }
        }

        public static void Flee()
        {
            if (Spells.E.IsReady())
                Spells.E.Cast(_Player);
            if (Spells.W.IsReady())
                Spells.W.Cast();
        }

        public static void LaneClear()
        {
            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                Spells.Q.Range);
            var m = minion.FirstOrDefault();
            if (m == null && m.Name.ToLower().Contains("ward")) return;
            var useQ = Utils.isChecked(MenuX.LaneClear, "laneQ");
            var useW = Utils.isChecked(MenuX.LaneClear, "laneW");
            var mana = Utils.getSliderValue(MenuX.LaneClear, "lManaSlider");
            var bestFarm =
                Utils.GetBestCircularFarmLocation(
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Distance(_Player) <= Spells.Q.Range)
                        .Select(xm => xm.ServerPosition.To2D())
                        .ToList(), Spells.Q.Width, Spells.Q.Range);
            if (useQ && Spells.Q.IsReady() &&
                _Player.ManaPercent >= mana)
            {
                Spells.Q.Cast(bestFarm.Position.To3D());
            }
            if (useW && Spells.W.IsReady() &&
                _Player.ManaPercent >= mana)
            {
                Spells.W.Cast();
            }
        }
    }
}
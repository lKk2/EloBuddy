using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using Rengod.Core;
using Rengod.Util;

namespace Rengod.Modes
{
    internal class Combo : Model
    {
        public static void useCombo()
        {
            InitSmite();
            var useQ = Misc.isChecked(ComboMenu, "ComboQ");
            var useW = Misc.isChecked(ComboMenu, "ComboW");
            var useE = Misc.isChecked(ComboMenu, "ComboE");

            var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            if (target == null || !target.IsValidTarget()) return;

            var cType = Misc.getSliderValue(ComboMenu, "cPrio");
            Items.useYoumu();
            SummonerUsage.useSummoner(target);
            if (Ferocity == 5)
            {
                switch (cType)
                {
                    case 0:
                        if (!RengarR)
                            Casts.useE(target);
                        break;
                    case 1:
                        Casts.useQ(target);
                        break;
                    case 2:
                        if (!RengarR && !_Player.IsDashing() && !Passive())
                            Casts.useW(target);
                        break;
                }
            }
            if (Ferocity < 5)
            {
                if (useQ)
                    Casts.useQ(target);

                if (RengarR) return;

                if (!_Player.IsDashing() && useW)
                {
                    Casts.useW(target);
                    Items.useHydra(target);
                }

                if (useE)
                    Casts.useE(target);
            }
        }
    }
}
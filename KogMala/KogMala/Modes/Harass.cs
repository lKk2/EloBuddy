using KogMala.AllahuAkbar;
using KogMala.Utils;

namespace KogMala.Modes
{
    internal class Harass : Model
    {
        public static void useHarass()
        {
            var useQ = Misc.isChecked(HarassMenu, "harassQ");
            var useW = Misc.isChecked(HarassMenu, "harassW");
            var useE = Misc.isChecked(HarassMenu, "harassE");
            var useR = Misc.isChecked(HarassMenu, "harassR");

            if (useE && E.IsReady())
                Casts.SmartE();
            if (useQ && Q.IsReady())
                Casts.SmartQ();
            if (useW && W.IsReady())
                Casts.SmartW();
            if (useR && R.IsReady())
                Casts.SmartR();
        }
    }
}
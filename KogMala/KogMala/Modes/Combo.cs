using KogMala.AllahuAkbar;
using KogMala.Utils;

namespace KogMala.Modes
{
    internal class Combo : Model
    {
        public static void useCombo()
        {
            var useQ = Misc.isChecked(ComboMenu, "comboQ");
            var useW = Misc.isChecked(ComboMenu, "comboW");
            var useE = Misc.isChecked(ComboMenu, "comboE");
            var useR = Misc.isChecked(ComboMenu, "comboR");

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
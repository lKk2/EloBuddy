using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace KogMala.Utils
{
    internal class Misc
    {
        public static bool isChecked(Menu obj, string value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderValue(Menu obj, string value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }
    }
}
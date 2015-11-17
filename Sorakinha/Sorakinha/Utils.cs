using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Sorakinha
{
    internal static class Utils
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

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
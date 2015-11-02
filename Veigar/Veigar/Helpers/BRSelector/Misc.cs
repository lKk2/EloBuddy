using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace BRSelector.Util
{
    public static class Misc
    {
        public static bool IsChecked(Menu obj, string value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }

        public static int GetSliderValue(Menu obj, string value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }

        public static void SetChecked(Menu obj, string name, bool value)
        {
            obj[name].Cast<CheckBox>().CurrentValue = value;
        }

        public static void SetSliderValue(Menu obj, string name, int value)
        {
            obj[name].Cast<Slider>().CurrentValue = value;
        }
    }
}

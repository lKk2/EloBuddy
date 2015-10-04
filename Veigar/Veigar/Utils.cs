using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Veigar
{
    class Utils
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

        public static HitChance getPredict(Menu obj, string value)
        {
            var s = getSliderValue(obj, value);
            switch (s)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }
    }
}

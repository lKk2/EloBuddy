using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace Veigar.Model
{
    public abstract class Model
    {
        /*
        Spells 
        */
        public static Spell.Skillshot Q { get; set; }
        public static Spell.Skillshot W { get; set; }
        public static Spell.Skillshot E { get; set; }
        public static Spell.Targeted R { get; set; }

        public static SpellSlot Ignite;

        /*
        Items
        */


        /*
        Misc 
        */
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }
    }
}

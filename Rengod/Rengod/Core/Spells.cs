using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Rengod.Util;

namespace Rengod.Core
{
    internal class Spells : Model
    {
        public static void SetSpells()
        {
            Q = new Spell.Active(SpellSlot.Q, 250);
            W = new Spell.Active(SpellSlot.W, 500);
            E = new Spell.Skillshot(SpellSlot.E, 1000, SkillShotType.Linear, (int) 0.25f, 1500, 70);
            R = new Spell.Active(SpellSlot.R, 2000);
            Ignite = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
        }

        public static void Heal()
        {
            var useHeal = Misc.isChecked(MiscMenu, "useHeal");
            var hpTo = Misc.getSliderValue(MiscMenu, "hpHeal");
            if (_Player.IsRecalling() || _Player.IsInShopRange() || Ferocity < 5 || RengarR || !useHeal) return;

            if (_Player.HealthPercent <= hpTo && W.IsReady())
                W.Cast();
        }
    }
}
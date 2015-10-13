using EloBuddy;
using EloBuddy.SDK;
using Rengod.Core;

namespace Rengod.Util
{
    internal class SummonerUsage : Model
    {
        public static void useSummoner(AIHeroClient target)
        {
            var igUsage = Misc.isChecked(ComboMenu, "useIG");
            var smUsage = Misc.isChecked(ComboMenu, "useSmite");
            if (igUsage && _Player.Distance(target) <= 600 &&
                _Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) >= target.Health)
            {
                _Player.Spellbook.CastSpell(Ignite, target);
            }
            if (smUsage && Smite != SpellSlot.Unknown &&
                _Player.Spellbook.CanUseSpell(Smite) == SpellState.Ready)
            {
                _Player.Spellbook.CastSpell(Smite, target);
            }
        }
    }
}
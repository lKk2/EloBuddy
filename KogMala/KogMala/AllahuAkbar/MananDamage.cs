using EloBuddy;
using EloBuddy.SDK;

namespace KogMala.AllahuAkbar
{
    internal class MananDamage : Model
    {
        public static void SetMana()
        {
            Qmana = _Player.Spellbook.GetSpell(SpellSlot.Q).SData.Mana;
            Wmana = _Player.Spellbook.GetSpell(SpellSlot.W).SData.Mana;
            Emana = _Player.Spellbook.GetSpell(SpellSlot.E).SData.Mana;
            Rmana = _Player.Spellbook.GetSpell(SpellSlot.R).SData.Mana;
        }

        public static void SetDmg(AIHeroClient target)
        {
            Qdmg = _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 80, 130, 180, 230, 280}[Q.Level] + 0.5*_Player.FlatMagicDamageMod));
            // DamageLibrary.GetSpellDamage(_Player, target, SpellSlot.Q);
            Wdmg = _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 2, 3, 4, 5, 6}[W.Level] + (0.1*_Player.FlatMagicDamageMod/100)));
            // DamageLibrary.GetSpellDamage(_Player, target, SpellSlot.W);
            Edmg = _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 60, 110, 160, 210, 260}[E.Level] + 0.7*_Player.FlatMagicDamageMod));
            //DamageLibrary.GetSpellDamage(_Player, target, SpellSlot.E);
            Rdmg = _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float)
                    (new[] {0, 160, 240, 320}[R.Level] + (0.3*_Player.FlatMagicDamageMod) +
                     (0.5*_Player.FlatPhysicalDamageMod)));
            //DamageLibrary.GetSpellDamage(_Player, target, SpellSlot.R);
        }
    }
}
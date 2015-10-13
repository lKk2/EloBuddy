using EloBuddy;
using EloBuddy.SDK;
using Rengod.Core;

namespace Rengod.Util
{
    internal class DmgLib : Model
    {
        public static float GetComboDmg(AIHeroClient target)
        {
            var damage = 0f;
            if (Q.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.Q);
            if (W.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.W);
            if (E.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.E);
            if (R.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.R);
            damage += _Player.GetAutoAttackDamage(target, true)*2;
            return damage;
        }
    }
}
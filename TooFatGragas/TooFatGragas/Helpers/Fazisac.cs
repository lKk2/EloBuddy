using System;
using System.Drawing;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;

namespace TooFatGragas.Helpers
{
    internal class Fazisac : Model.Model
    {
        public static float GetTotalDmg(AIHeroClient target)
        {
            var damage = Player.Instance.GetAutoAttackDamage(target);
            if (Q.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.Q);
            if (W.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.W);
            if (E.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.E);
            if (R.IsReady())
                damage += _Player.GetSpellDamage(target, SpellSlot.R);
            return damage;
        }

        public static void IsacDraw(EventArgs args)
        {
            if (Config.Drawings.DrawQ)
                new Circle {Color = Q.IsReady() ? Color.White : Color.Red, Radius = Q.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            if (Config.Drawings.DrawE)
                new Circle {Color = E.IsReady() ? Color.White : Color.Red, Radius = E.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            if (Config.Drawings.DrawR)
                new Circle {Color = R.IsReady() ? Color.White : Color.Red, Radius = R.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            DamageIndicator.HealthbarEnabled = Config.Drawings.IndicatorHealthbar;
            DamageIndicator.PercentEnabled = Config.Drawings.IndicatorPercent;
            if (!Config.Insec.getDraw) return;
            var target = AdvancedTargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (target == null) return;
            var tpos = Drawing.WorldToScreen(target.Position);
            if (R.IsReady() && R.IsInRange(target) && R.Level > 0)
            {
                Drawing.DrawText(tpos.X, tpos.Y, Color.DarkGoldenrod, "Insec Target");

                Drawing.DrawCircle(target.Position, 150, Color.AliceBlue);
                var isac = _Player.Position.Extend(target.Position, _Player.Distance(target) + 150);
                Drawing.DrawCircle(isac.To3D(), 100, Color.Blue);
            }
        }
    }
}
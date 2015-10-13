using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using Rengod.Core;

namespace Rengod.Util
{
    internal class Drawings : Model
    {
        public static void OnDraw(EventArgs args)
        {
            var drawQ = Misc.isChecked(DrawingMenu, "drawQ");
            var drawW = Misc.isChecked(DrawingMenu, "drawW");
            var drawE = Misc.isChecked(DrawingMenu, "drawE");
            var drawR = Misc.isChecked(DrawingMenu, "drawR");
            var drawC = Misc.isChecked(DrawingMenu, "drawC");
            var drawK = Misc.isChecked(DrawingMenu, "drawK");

            if (drawQ)
                new Circle {Color = Q.IsReady() ? Color.White : Color.Red, Radius = Q.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            if (drawW)
                new Circle {Color = W.IsReady() ? Color.White : Color.Red, Radius = W.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            if (drawE)
                new Circle {Color = E.IsReady() ? Color.White : Color.Red, Radius = E.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            if (drawR)
                new Circle {Color = R.IsReady() ? Color.White : Color.Red, Radius = R.Range, BorderWidth = 2f}.Draw(
                    _Player.Position);
            if (drawC)
                Drawing.DrawText(50, 50, Color.DeepPink,
                    "Current Combo Prioritize: " + MenuX.prio[Misc.getSliderValue(ComboMenu, "cPrio")]);
            if (drawK)
            {
                foreach (var h in EntityManager.Heroes.Enemies)
                {
                    var pos = h.HPBarPosition;
                    if (!h.IsDead && h.Health <= DmgLib.GetComboDmg(h))
                    {
                        Drawing.DrawText(pos.X + 100, pos.Y - 5, Color.Tomato, "K");
                    }
                }
            }
            if (RengarR)
            {
                foreach (var buff in _Player.Buffs)
                {
                    if (buff.Name.ToLower() == "rengarr")
                    {
                        var mypos = Drawing.WorldToScreen(_Player.Position);
                        var timer = buff.EndTime - Game.Time;
                        var fancy = string.Format("{0:0}", timer);
                        Drawing.DrawText(mypos[0] - 10, mypos[1] - 140, Color.DeepPink, "" + fancy);
                        break;
                    }
                }
            }
        }
    }
}
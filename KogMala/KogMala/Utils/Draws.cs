using System;
using System.Drawing;
using EloBuddy.SDK.Rendering;
using KogMala.AllahuAkbar;

namespace KogMala.Utils
{
    internal class Draws : Model
    {
        public static void OnDraw(EventArgs args)
        {
            var drawQ = Misc.isChecked(DrawsMenu, "drawQ");
            var drawW = Misc.isChecked(DrawsMenu, "drawW");
            var drawE = Misc.isChecked(DrawsMenu, "drawE");
            var drawR = Misc.isChecked(DrawsMenu, "drawR");

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
        }
    }
}
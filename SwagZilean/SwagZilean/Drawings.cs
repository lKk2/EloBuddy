using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Rendering;

namespace SwagZilean
{
    internal class Drawings
    {
        public static void OnDraw(EventArgs args)
        {
            if (Brain._Player.IsDead) return;

            if (Utils.isChecked(MenuX.Draw, "drawQ"))
                new Circle {Color = Color.White, Radius = Spells.Q.Range, BorderWidth = 2f}.Draw(Brain._Player.Position);
            if (Utils.isChecked(MenuX.Draw, "drawE"))
                new Circle {Color = Color.White, Radius = Spells.E.Range, BorderWidth = 2f}.Draw(Brain._Player.Position);
            if (Utils.isChecked(MenuX.Draw, "drawR"))
                new Circle {Color = Color.White, Radius = Spells.R.Range, BorderWidth = 2f}.Draw(Brain._Player.Position);

            if (Utils.isChecked(MenuX.Draw, "cMode"))
            {
                Drawing.DrawText(50, 50, Color.DeepPink,
                    "Current Combo: " + MenuX.CombosZileans[Utils.getSliderValue(MenuX.Combo, "whatcombo")], 30);
            }
        }
    }
}
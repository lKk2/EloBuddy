using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Kassawin
{
    class DrawX
    {
        public static void OnDraw(EventArgs args)
        {
            if (Utils.isChecked(MenuX.Misc, "drawQ"))
                new Circle { Color = System.Drawing.Color.White, Radius = Spells.Q.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
            if (Utils.isChecked(MenuX.Misc, "drawW"))
                new Circle { Color = System.Drawing.Color.White, Radius = Spells.W.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
            if (Utils.isChecked(MenuX.Misc, "drawE"))
                new Circle { Color = System.Drawing.Color.White, Radius = Spells.E.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
            if (Utils.isChecked(MenuX.Misc, "drawR"))
                new Circle { Color = System.Drawing.Color.White, Radius = Spells.R.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
        }
    }
}

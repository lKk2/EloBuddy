using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.Direct3D9;

namespace kTwitch2.Model
{
    class Drawings : Model
    {
        public static Font Font;

        static Drawings()
        {
            Font = new Font(
                Drawing.Direct3DDevice,
                new FontDescription
                {
                    FaceName = "Segoi UI",
                    Height = 45,
                    OutputPrecision = FontPrecision.Default,
                    Quality = FontQuality.Default
                });

            Drawing.OnDraw += OnOnDraw;
        }

        public void Init() { }
        private static void OnOnDraw(EventArgs args)
        {
            var drawW = isChecked(DrawingsMenu, "drawW");
            var drawR = isChecked(DrawingsMenu, "drawR");
            var drawTimer = isChecked(DrawingsMenu, "drawTimer");

            if (_Player.IsDead) return;

            if (drawW)
                new Circle() {Color = W.IsReady() ? System.Drawing.Color.White : System.Drawing.Color.Red, Radius = W.Range, BorderWidth = 2f}.Draw(_Player.Position);

            if (drawR)
                new Circle() { Color = R.IsReady() ? System.Drawing.Color.White : System.Drawing.Color.Red, Radius = R.Range, BorderWidth = 2f }.Draw(_Player.Position);

            if (drawTimer)
            {
                var stealthTime = GetRemainingTime();
                var mypos = Drawing.WorldToScreen(_Player.Position);
                var fancy = string.Format("{0:0}", stealthTime);
                if (stealthTime > 0)
                {

                    Font.DrawText(null,
                        "" + fancy,
                        (int) mypos[0], (int) mypos[1]/2, Color.DeepPink);

                }
            }

        }

        private static float GetRemainingTime()
        {
            var buff = _Player.GetBuff("twitchhideinshadows");
            if (buff == null) return 0f;
            return buff.EndTime - Game.Time;
        }
    }
}

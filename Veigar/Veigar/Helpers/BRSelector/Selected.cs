using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using Color = System.Drawing.Color;

namespace BRSelector.Model
{
   internal class Selected
    {
        static Selected()
        {
            ClickBuffer = 100f;
            Game.OnWndProc += OnGameWndProc;
            Drawing.OnDraw += OnDraw;
        }
        public static float ClickBuffer { get; set; }
        public static AIHeroClient Target { get; set; }

        public static AIHeroClient GetTarget(float range, DamageType damageType, bool ignoreShields, Vector3 from)
        {
            try
            {
                if (Target != null &&
                    AdvancedTargetSelector.IsValidTarget(
                        Target, range, damageType, ignoreShields, from))
                {
                    return Target;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        private static void OnDraw(EventArgs args)
        {
            try
            {
                if (Target != null && Target.IsValidTarget() && Target.Position.IsOnScreen())
                {
                    Drawing.DrawCircle(Target.Position, 150, Color.Red);
                   }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void OnGameWndProc(WndEventArgs args)
        {
            try
            {
                if (args.Msg != (ulong)WindowMessages.LeftButtonDown)
                {
                    return;
                }

                Target =
                    Targets.Items.Select(t => t.Hero)
                        .Where(h => h.IsValidTarget() && h.Distance(Game.CursorPos) < h.BoundingRadius + ClickBuffer)
                        .OrderBy(h => h.Distance(Game.CursorPos))
                        .FirstOrDefault();

                // quando tiver um menu ficar melhor nele neh parsa
                Drawing.OnDraw += OnDraw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

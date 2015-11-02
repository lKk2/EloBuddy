using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using Veigar.Controller;
using Veigar.Helpers;

namespace Veigar.Model
{
    class Veigar : Model
    {

        static Veigar()
        {
            if (Player.Instance.Hero != Champion.Veigar) return;
            Spells.Initialize();
            ModeManager.Initialize();
            MenuX.Initialize();
            BRSelector.Selector.Init();
            Chat.Print("Loaded^>^", Color.DeepPink);
            Drawing.OnDraw += OnDraw;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            DamageIndicator.Initialize(DamageLib.GetTotalDamage);
            HasIgnite = Player.Instance.GetSpellSlotFromName("SummonerDot") != SpellSlot.Unknown;   
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloserEventArgs)
        {
            if (sender.IsEnemy && !sender.IsDead && !sender.IsAlly && !sender.IsZombie)
            {
                if (E.IsReady() && MenuX.Misc.GapCloser)
                {
                    E.Cast(gapcloserEventArgs.End.Shorten(_Player.Position, 150));
                }
            }
        }

        public static void Initialize() { }

        private static void OnDraw(EventArgs args)
        {
            if (MenuX.Drawings.DrawQ)
                new Circle() { Color = Q.IsReady() ? System.Drawing.Color.White : System.Drawing.Color.Red, Radius = Q.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (MenuX.Drawings.DrawW)
                new Circle() { Color = W.IsReady() ? System.Drawing.Color.White : System.Drawing.Color.Red, Radius = W.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (MenuX.Drawings.DrawE)
                new Circle() { Color = E.IsReady() ? System.Drawing.Color.White : System.Drawing.Color.Red, Radius = E.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (MenuX.Drawings.DrawR)
                new Circle() { Color = R.IsReady() ? System.Drawing.Color.White : System.Drawing.Color.Red, Radius = R.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (MenuX.Drawings.DrawAutoQ && MenuX.AutoQ.isKey)
                Drawing.DrawText(_Player.HPBarPosition.X, _Player.HPBarPosition.Y + 50, Color.White, "FarmQ Active");

            DamageIndicator.HealthbarEnabled = MenuX.Drawings.IndicatorHealthbar;
            DamageIndicator.PercentEnabled = MenuX.Drawings.IndicatorPercent;
        }
    }
}

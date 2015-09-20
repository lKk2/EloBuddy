using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;


namespace kLastHit
{
    internal class Program
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Bootstrap.Init(null);
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
           // new Circle() {Color = Color.Blue, Radius = 10}.Draw(_Player.Position);

            //foreach (var minion in ObjectManager.Get<Obj_AI_Minion>())

            foreach (var minion in ObjectManager.Get<Obj_AI_Minion>().Where(x => x.CountEnemiesInRange(2500) >= _Player.CountEnemiesInRange(2500) && x.IsEnemy))
            {
                if (!minion.IsValidTarget(2500))
                {
                    continue;
                }
                
                if (minion.Health <= _Player.GetAutoAttackDamage(minion, true))
                {
                    Drawing.DrawCircle(minion.Position, minion.BoundingRadius, Color.BlueViolet);
                }
            }
        }
    }
}
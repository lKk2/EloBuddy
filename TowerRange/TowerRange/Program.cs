using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;
using SharpDX;

namespace TowerRange
{
    internal class Program
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static Dictionary<int, Obj_AI_Turret> turretCache = new Dictionary<int, Obj_AI_Turret>();
        private static Dictionary<int, AttackableUnit> turretTarget = new Dictionary<int, AttackableUnit>();
        private static Menu TowerRange, Options;

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;

            
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Drawing.OnEndScene += Drawing_OnEndScene;
            Bootstrap.Init(null);

            TowerRange = MainMenu.AddMenu("TowerRange", "towerrange");
            TowerRange.AddGroupLabel("TowerRange 1.0 d(0.o)b");
            TowerRange.AddSeparator();
            TowerRange.AddLabel("Made by Kk2 (:");

            InitializeCache();
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            var turretRange = 875 + _Player.BoundingRadius;
            foreach (var entry in turretCache)
            {
                var turret = entry.Value;
                var circlePadding = 20;
                if (turret == null || !turret.IsValid || turret.IsDead)
                {
                    turretCache.Remove(entry.Key);
                    continue;
                }
                var distToTurret = _Player.ServerPosition.Distance(turret.Position);
                if (distToTurret < turretRange + 500)
                {
                    var tTarget = turretTarget[turret.NetworkId];
                    if (tTarget.IsValidTarget(float.MaxValue))
                    {
                        if (tTarget is AIHeroClient)
                        {
                            Drawing.DrawCircle(tTarget.Position, tTarget.BoundingRadius + circlePadding,
                                Color.FromArgb(255, 255, 0, 0));
                        }
                        else
                        {
                            Drawing.DrawCircle(tTarget.Position, tTarget.BoundingRadius + circlePadding,
                                Color.FromArgb(255, 0, 255, 0));
                        }
                    }
                    if (tTarget != null && (tTarget.IsMe || (turret.IsAlly && tTarget is AIHeroClient)))
                    {
                        Drawing.DrawCircle(turret.Position, turretRange, Color.FromArgb(255, 255, 0, 0));
                    }
                    else
                    {
                        var alpha = distToTurret > turretRange ? (turretRange + 500 - distToTurret)/2 : 250;
                        Drawing.DrawCircle(turret.Position, turretRange, Color.FromArgb((int)alpha, 0, 255, 0));
                    }
                }

            }
        }

        private static void InitializeCache()
        {
            foreach (var obj in ObjectManager.Get<Obj_AI_Turret>())
            {
                if (!turretCache.ContainsKey(obj.NetworkId))
                {
                        turretCache.Add(obj.NetworkId, obj);
                        turretTarget.Add(obj.NetworkId, null);
                }
            }
        }
    }
}
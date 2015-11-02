using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using Veigar.Controller.Modes;
using Veigar.Helpers;
using Veigar.Model;

namespace Veigar.Controller
{
    internal class ModeManager
    {
        private static HashSet<ModeBase> Modes { get; set; }

        static ModeManager()
        {
            Modes = new HashSet<ModeBase>();

            Modes.UnionWith(new ModeBase[]
            {
                 new Combo(),
                 new LastHit(), 
                 new Harass(),
                 new LaneClear(),
                // new JungleClear(),
                new PermaActive()
            });
            Game.OnTick += GameOnOnTick;
        }

        public static void Initialize()
        {

        }


        private static void GameOnOnTick(EventArgs args)
        {
            Orbwalker.ForcedTarget = null;
            Modes.ForEach(mode =>
            {
                try
                {
                    if (mode.ShouldBeExecuted())
                    {
                        mode.Execute();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.Error, "Error executing mode '{0}'\n{1}", mode.GetType().Name, ex);
                }
            });
        }
    }
}

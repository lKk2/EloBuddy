using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Utils;
using SimpleSivir.Controller.Modes;
using SimpleSivir.Helpers;
using SimpleSivir.Model;

namespace SimpleSivir.Controller
{
    internal class ModeManager
    {
        static ModeManager()
        {
            Modes = new HashSet<ModeBase>();

            Modes.UnionWith(new ModeBase[]
            {
                new Combo(),
                new Harass(),
                new LaneClear(),
                new JungleClear(),
                new PermaActive()
            });
            Game.OnUpdate += GameOnUpdate;
        }

        private static HashSet<ModeBase> Modes { get; set; }

        public static void Initialize()
        {
        }


        private static void GameOnUpdate(EventArgs args)
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
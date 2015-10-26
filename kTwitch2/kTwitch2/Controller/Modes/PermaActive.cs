using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EloBuddy.SDK;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            var use = isChecked(MiscMenu, "stealM");
            /* Mob Steal*/
            var mob =
                EntityManager.MinionsAndMonsters.Minions.Where(x => x.Distance(_Player) <= E.Range)
                    .OrderByDescending(x => x.Health).FirstOrDefault();
            if (mob == null || !use) return;
            if (E.IsReady())
            {
                if ((mob.BaseSkinName.Contains("Dragon") ||
                    mob.BaseSkinName.Contains("Baron") || 
                    mob.BaseSkinName.Contains("SRU_Blue") ||
                    mob.BaseSkinName.Contains("SRU_Red")) &&
                    mob.Health + 50 + (mob.PercentBaseHPRegenMod/2) <= DmgLib.EDamage(mob) &&
                    mob.HasBuff("twitchdeadlyvenom"))
                {
                    E.Cast();
                }
            }
            /* Pot Usage */
            ItemManager.UsePotions();
        }
    }
}

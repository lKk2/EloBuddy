using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;

namespace BRSelector.Model
{
    class Humanizer
    {
        public static IEnumerable<Targets.Heroes> FilterTargets(IEnumerable<Targets.Heroes> targets)
        {
            var finalTargets = targets.ToList();
            try
            {
                var fowDelay = 350; // podendo mudar né joveN
                if (fowDelay > 0)
                {
                    finalTargets =
                        finalTargets.Where(item => Game.Time - item.LastVisibleChange > fowDelay / 1000f).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return finalTargets;
        }
    }
}

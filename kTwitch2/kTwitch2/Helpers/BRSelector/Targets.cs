using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace BRSelector.Model
{
    internal class Targets
    {
        static Targets()
        {
            try
            {
                Items = new HashSet<Heroes>();
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    Items.Add(new Heroes(enemy));
                }
                Game.OnPreTick += GameOnOnPreTick;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void GameOnOnPreTick(EventArgs args)
        {
            try
            {
                foreach (var item in Items.Where(item => item.Visible != !item.Hero.IsVisible))
                {
                    item.Visible = item.Hero.IsVisible;
                    item.LastVisibleChange = Game.Time;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static HashSet<Heroes> Items { get; private set; }
        public class Heroes
        {
            public Heroes(AIHeroClient hero)
            {
                Hero = hero;
                LastVisibleChange = Game.Time;
            }
            public AIHeroClient Hero { get; private set; }
            public float LastVisibleChange { get; set; }
            public float Value { get; set; }
            public bool Visible { get; set; }
        }
    }
}

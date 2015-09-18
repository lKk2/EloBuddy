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

namespace IgniteBuddy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        /**
        BURN MOTHERFUCKER
        **/
        public static Spell.Targeted IG;
        public static Menu IgMenu, SelectionsMenu;

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.GetSpellSlotFromName("summonerdot") == SpellSlot.Unknown)
            {
                return;
            }
            Bootstrap.Init(null);
            IG = new Spell.Targeted(ObjectManager.Player.GetSpellSlotFromName("summonerdot"), 550);
            IgMenu = MainMenu.AddMenu("IgniteBuddy", "ignitebuddy");
            IgMenu.AddGroupLabel("IgniteBuddy");
            IgMenu.AddSeparator();
            IgMenu.AddLabel("Let them burn <3");
            IgMenu.AddLabel("Made by Kk2");

            SelectionsMenu = IgMenu.AddSubMenu("Options", "Options");
            SelectionsMenu.AddGroupLabel("Ignite OPTIONS");
            SelectionsMenu.AddSeparator();
            SelectionsMenu.Add("Active", new CheckBox("Active [Must be to work, orly?]"));
            SelectionsMenu.Add("KS", new CheckBox("KillSteal with Ignite"));
            //SelectionsMenu.Add("Lowest", new CheckBox("Use on Lowest Life"));

            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (ObjectManager.Player.IsDead || !IG.IsReady() || !SelectionsMenu["Active"].Cast<CheckBox>().CurrentValue) return;

            //if (SelectionsMenu["Lowest"].Cast<CheckBox>().CurrentValue)
            //{
            //    var target =
            //        HeroManager.Enemies.Where(hero => hero.IsValidTarget(IG.Range)).OrderBy(hero => hero.Hero).First();
            //    IG.Cast(target);
            //}

            if (SelectionsMenu["KS"].Cast<CheckBox>().CurrentValue)
            {
                var target2 = ObjectManager.Get<AIHeroClient>()
                    .Where(
                        h =>
                            h.IsValidTarget(IG.Range) &&
                            h.Health <
                            ObjectManager.Player.GetSummonerSpellDamage(h, DamageLibrary.SummonerSpells.Ignite));

                IG.Cast(target2.First());
            }

        }
    }
}
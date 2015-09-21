using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace Sorakinha
{
    internal class Program
    {
        /**
        Declaring the Spells
        **/
        public static Spell.Skillshot Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;
        public static Spell.Active R;

        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "soraka") return; // check if hero is not soraka 
            Bootstrap.Init(null);
            MenuX.CallMeNigga(); // calls the menu!
            Chat.Print("Sorakinha Loaded!", System.Drawing.Color.DeepPink);

            // Spells Infos.
            Q = new Spell.Skillshot(SpellSlot.Q, 950, SkillShotType.Circular, (int)0.283f, 1100, (int)210f);
            W = new Spell.Targeted(SpellSlot.W, 550);
            E = new Spell.Skillshot(SpellSlot.E, 925, SkillShotType.Circular, (int)0.5f, 1750, (int)70f);
            R = new Spell.Active(SpellSlot.R);

            Drawing.OnEndScene += Drawing_OnEndScene;
            Game.OnTick += Game_OnTick;
            Orbwalker.OnPreAttack += Brain.Orbwalker_OnPreAttack;
            Gapcloser.OnGapcloser += Brain.Gapcloser_OnGapCloser;
            Interrupter.OnInterruptableSpell += Brain.Interrupter_OnInterruptableSpell;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var autoW = MenuX.Healing["useW"].Cast<CheckBox>().CurrentValue;
            if (autoW && W.IsReady())
            {
                Brain.AutoW();
            }
            if (R.IsReady())
            {
                Brain.AutoR();
            }
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Brain.Combo();
                    return;
                case Orbwalker.ActiveModes.Harass:
                    Brain.Harass();
                    return;
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            var hBar = MenuX.Drawing["drawH"].Cast<CheckBox>().CurrentValue;
            if (hBar)
            {
                foreach (var hero in HeroManager.Allies)
                {
                    var pos = hero.HPBarPosition;
                    if (!hero.IsDead && !hero.IsMe &&
                        hero.HealthPercent <= MenuX.Healing["wpct" + hero.ChampionName].Cast<Slider>().CurrentValue)
                    {
                        Drawing.DrawText(pos.X + 110, pos.Y - 5, Color.Tomato, "H");
                    }
                }
            }
            var QRange = MenuX.Drawing["drawQ"].Cast<CheckBox>().CurrentValue;
            var ERange = MenuX.Drawing["drawE"].Cast<CheckBox>().CurrentValue;
            if (QRange)
            {
                Drawing.DrawCircle(_Player.Position, Q.Range, Q.IsReady() ? Color.Aqua : Color.Red);
            }
            if (ERange)
            {
                Drawing.DrawCircle(_Player.Position, E.Range, E.IsReady() ? Color.Aqua : Color.Red);
            }
        }
}
}

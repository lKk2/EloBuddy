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
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace kTwitch
{
    internal class Program
    {
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static Item HPOT;
        public static Dictionary<SpellSlot, Spell.SpellBase> Spells = new Dictionary<SpellSlot, Spell.SpellBase>()
        {
            {SpellSlot.Q, new Spell.Active(SpellSlot.Q)},
            {SpellSlot.W, new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Circular, 250, 1400, 120)}, // 250? delay?
            {SpellSlot.E, new Spell.Active(SpellSlot.E, 1200)},
            {SpellSlot.R, new Spell.Active(SpellSlot.R)}
        };

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            Bootstrap.Init(null);
            Chat.Print("kTwitch Loaded!");
            MenuX.CallMeNigga();
            TargetSelector2.init();
            Game.OnTick += Game_OnTick;
            Drawing.OnEndScene += Drawing_OnEndScene;
        }

        private static void Game_OnTick(EventArgs args)
        {
            HPOT = new Item((int)ItemId.Health_Potion);
            if (_Player.HealthPercent <= 60 && HPOT.IsOwned())
            {
                HPOT.Cast();
            }
            if (Spells[SpellSlot.E].IsReady() && !_Player.IsDead)
            {
                Brain.KStiloso();
            }
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Brain.Combo();
                    return;
                case Orbwalker.ActiveModes.Harass:
                    Brain.Harass();
                    return;
                case Orbwalker.ActiveModes.LaneClear:
                   // Brain.LaneClear();
                    return;
                case Orbwalker.ActiveModes.JungleClear:
                   // Brain.LaneClear();
                    return;
            }
        }

        private static void Drawing_OnEndScene(EventArgs args)
        {
            var stealthTime = GetRemainingTime();
            if (stealthTime > 0)
            {
                Drawing.DrawCircle(_Player.Position, stealthTime * _Player.MoveSpeed, Color.BlueViolet);
            }
        }

        private static float GetRemainingTime()
        {
            var buff = ObjectManager.Player.GetBuff("twitchhideinshadows");
            //if (buff == null && Instance.State == SpellState.Ready) return Spells[SpellSlot.Q].Level + 3 + 1.5f + 1f;
            if (buff == null) return 0;
            return buff.EndTime - Game.Time;
        }
    }
}

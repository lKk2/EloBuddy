using System;
using BRSelector;
using EloBuddy;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using TooFatGragas.Controller;
using TooFatGragas.Helpers;

namespace TooFatGragas.Model
{
    internal class Gragas : Model
    {
        static Gragas()
        {
            if (Player.Instance.Hero != Champion.Gragas) return;
            new Spells().Init();
            Chat.Print("EOQ");
            GameObject.OnCreate += GameObjectOnOnCreate;
            GameObject.OnDelete += GameObjectOnOnDelete;
            Gapcloser.OnGapcloser += GapcloserOnOnGapcloser;
            ModeManager.Initialize();
            Selector.Init();
            Config.Initialize();
            DamageIndicator.Initialize(Fazisac.GetTotalDmg);
            Drawing.OnDraw += Fazisac.IsacDraw;
        }

        private static void GapcloserOnOnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloserEventArgs)
        {
            if (E.IsReady() && E.GetPrediction(sender).HitChance >= HitChance.High && sender.IsEnemy && !sender.IsZombie &&
                Config.Insec.getGap)
            {
                E.Cast(sender);
            }
        }

        private static void GameObjectOnOnDelete(GameObject sender, EventArgs args)
        {
            if (sender.IsMe && sender.Name == "Gragas_Base_Q_Ally.troy")
            {
                Barrel = null;
            }
        }

        private static void GameObjectOnOnCreate(GameObject sender, EventArgs args)
        {
            if (sender.IsMe && sender.Name == "Gragas_Base_Q_Ally.troy")
            {
                Barrel = sender;
            }
        }

        public void Init()
        {
        }
    }
}
using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace SimpleSivir
{
    internal class Brain
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W;
        public static Spell.Active E;
        public static Spell.Active R;
        public static Item Potion = new Item(2003);

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        public static void runSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1245, SkillShotType.Linear, (int)0.25f, 1030, 90);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Active(SpellSlot.E);
            R = new Spell.Active(SpellSlot.R, 1000);
        }

        public static void Game_OnTick(EventArgs args)
        {
            Potions();
            AutoQ();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo();
                    return;
                case Orbwalker.ActiveModes.Harass:
                    Harass();
                    return;
            }
        }

        #region PotionSSSSSOP

        public static void Potions()
        {
            if (MenuX.MiscMenu["usePot"].Cast<CheckBox>().CurrentValue && !_Player.IsInShopRange() &&
                !_Player.HasBuff("recall"))
            {
                if (Potion.IsReady() && !_Player.HasBuff("RegenerationPotion"))
                {
                    if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                        Potion.Cast();
                    else if (_Player.Health < _Player.MaxHealth * 0.6)
                        Potion.Cast();
                }
            }
        }

        #endregion

        #region AutoQ

        public static void AutoQ()
        {
            if (MenuX.MiscMenu["autoQ"].Cast<CheckBox>().CurrentValue)
            {
                foreach (
                    var target in
                        HeroManager.Enemies.Where(
                            x => x.IsValidTarget(Q.Range) && !x.HasBuffOfType(BuffType.Invulnerability)))
                {
                    if (Q.GetPrediction(target).HitChance >= HitChance.Immobile || Q.GetPrediction(target).HitChance >= HitChance.Dashing)
                        Q.Cast(target);
                }
            }
        }

        #endregion

        #region Combo

        public static void Combo()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null) return;
            if (MenuX.ComboMenu["useQc"].Cast<CheckBox>().CurrentValue &&
                Q.IsReady())
            {
                Q.Cast(target);
            }
            if (MenuX.ComboMenu["useWc"].Cast<CheckBox>().CurrentValue &&
                W.IsReady())
            {
                W.Cast();
            }
            if (MenuX.ComboMenu["autoR"].Cast<CheckBox>().CurrentValue && R.IsReady())
            {
                if (_Player.CountEnemiesInRange(800) > 2)
                {
                    R.Cast();
                }
                else if (target.IsValidTarget() && _Player.GetAutoAttackDamage(target) * 2 > target.Health && !Q.IsReady() &&
                         target.CountEnemiesInRange(800) < 3)
                    R.Cast();
            }
        }

        #endregion

        #region Harass

        public static void Harass()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null) return;
            if (MenuX.HarassMenu["useQh"].Cast<CheckBox>().CurrentValue &&
                Q.IsReady() &&
                MenuX.HarassMenu["mppc"].Cast<Slider>().CurrentValue <= _Player.ManaPercent)
            {
                Q.Cast(target);

            }
            if (MenuX.HarassMenu["useWh"].Cast<CheckBox>().CurrentValue &&
                W.IsReady() &&
                MenuX.HarassMenu["mppc"].Cast<Slider>().CurrentValue <= _Player.ManaPercent)
            {
                W.Cast();
            }
        }

        #endregion

        #region KillSteal

        public static void KillSteal()
        {
            if (MenuX.KsMenu["useQks"].Cast<CheckBox>().CurrentValue)
            {
                var target = ObjectManager.Get<AIHeroClient>()
                    .FirstOrDefault(enemy => enemy.IsValidTarget(Q.Range) &&
                                             enemy.Health < _Player.GetSpellDamage(enemy, SpellSlot.Q));
                if (target.IsValidTarget(Q.Range))
                {
                    Q.Cast(target);
                }
            }
            if (MenuX.KsMenu["useWks"].Cast<CheckBox>().CurrentValue)
            {
                var target = ObjectManager.Get<AIHeroClient>()
                    .FirstOrDefault(enemy => enemy.IsValidTarget(W.Range) &&
                                             enemy.Health < _Player.GetSpellDamage(enemy, SpellSlot.W));
                if (target.IsValidTarget(W.Range))
                {
                    W.Cast();
                }
            }

        }

        #endregion

        #region Drawings

        public static void Drawing_OnDraw(EventArgs args)
        {
            if (_Player.IsDead) return;

            var RTimer = MenuX.MiscMenu["drawRTimer"].Cast<CheckBox>().CurrentValue;
            var QRange = MenuX.MiscMenu["drawQ"].Cast<CheckBox>().CurrentValue;
            var MinionMark = MenuX.MiscMenu["drawAA"].Cast<CheckBox>().CurrentValue;
            if (QRange)
            {
                Drawing.DrawCircle(_Player.Position, Q.Range, Q.IsReady() ? Color.Aqua : Color.Red);
            }

            if (MinionMark)
            {
                foreach (
                    var m in
                        ObjectManager.Get<Obj_AI_Minion>()
                            .Where(x => x.CountEnemiesInRange(2500) >= _Player.CountEnemiesInRange(2500) && x.IsEnemy))
                {
                    if (!m.IsValidTarget(2500))
                        continue;
                    if (m.Health <= _Player.GetAutoAttackDamage(m, true))
                    {
                        Drawing.DrawCircle(m.Position, m.BoundingRadius, Color.White);
                    }
                }
            }

            if (RTimer)
            {
                foreach (var buff in _Player.Buffs)
                {
                    if (buff.Name.ToLower() == "sivirr")
                    {
                        var mypos = Drawing.WorldToScreen(_Player.Position);
                        Drawing.DrawText(mypos[0] - 10, mypos[1] - 140, Color.Gold, "" + (buff.EndTime - Game.Time));
                        break;
                    }
                }
            }
        }

        #endregion

        #region ProcessSpell/GapCloser

        public static void AIHeroClient_On_ProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (MenuX.ShieldMenu["autoE"].Cast<CheckBox>().CurrentValue && !_Player.IsDead)
            {
                string[] Spells = {"AhriSeduce"
                                          , "InfernalGuardian"
                                          , "EnchantedCrystalArrow"
                                          , "InfernalGuardian"
                                          , "EnchantedCrystalArrow"
                                          , "RocketGrab"
                                          , "BraumQ"
                                          , "CassiopeiaPetrifyingGaze"
                                          , "DariusAxeGrabCone"
                                          , "DravenDoubleShot"
                                          , "DravenRCast"
                                          , "EzrealTrueshotBarrage"
                                          , "FizzMarinerDoom"
                                          , "GnarBigW"
                                          , "GnarR"
                                          , "GragasR"
                                          , "GravesChargeShot"
                                          , "GravesClusterShot"
                                          , "JarvanIVDemacianStandard"
                                          , "JinxW"
                                          , "JinxR"
                                          , "KarmaQ"
                                          , "KogMawLivingArtillery"
                                          , "LeblancSlide"
                                          , "LeblancSoulShackle"
                                          , "LeonaSolarFlare"
                                          , "LuxLightBinding"
                                          , "LuxLightStrikeKugel"
                                          , "LuxMaliceCannon"
                                          , "UFSlash"
                                          , "DarkBindingMissile"
                                          , "NamiQ"
                                          , "NamiR"
                                          , "OrianaDetonateCommand"
                                          , "RengarE"
                                          , "rivenizunablade"
                                          , "RumbleCarpetBombM"
                                          , "SejuaniGlacialPrisonStart"
                                          , "SionR"
                                          , "ShenShadowDash"
                                          , "SonaR"
                                          , "ThreshQ"
                                          , "ThreshEFlay"
                                          , "VarusQMissilee"
                                          , "VarusR"
                                          , "VeigarBalefulStrike"
                                          , "VelkozQ"
                                          , "Vi-q"
                                          , "Laser"
                                          , "xeratharcanopulse2"
                                          , "XerathArcaneBarrage2"
                                          , "XerathMageSpear"
                                          , "xerathrmissilewrapper"
                                          , "yasuoq3w"
                                          , "ZacQ"
                                          , "ZedShuriken"
                                          , "ZiggsQ"
                                          , "ZiggsW"
                                          , "ZiggsE"
                                          , "ZiggsR"
                                          , "ZileanQ"
                                          , "ZyraQFissure"
                                          , "ZyraGraspingRoots"
                                      };
                for (int i = 0; i <= 61; i++)
                {
                    if (args.SData.Name == Spells[i])
                    {
                        if (sender is AIHeroClient && sender.IsEnemy && args.Target.IsMe && !args.SData.IsAutoAttack() &&
                            E.IsReady())
                        {
                            E.Cast();
                        }
                    }
                }
            }
        }

        public static void AntiGapCloser_OnEnemyGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs args)
        {
            if (MenuX.ShieldMenu["AntiGap"].Cast<CheckBox>().CurrentValue && E.IsReady() &&
                sender.IsValidTarget(1000))
            {
                E.Cast();
            }
        }

        #endregion
    }
}
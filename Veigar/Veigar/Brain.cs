using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Color = System.Drawing.Color;

namespace Veigar
{
    class Brain
    {
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        public static void Init()
        {
            Bootstrap.Init(null);
            LoadSpells();
            MenuX.CallMenu();
            Chat.Print("Veigar^.^ Loaded", Color.Purple);
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += OnDraw;

        }
        #region Drawings

        private static void OnDraw(EventArgs args)
        {
            if (isChecked(MenuX.Drawing, "drawQ"))
                new Circle() {Color = Color.White, Radius = Q.Range, BorderWidth = 2f}.Draw(_Player.Position);
            if (isChecked(MenuX.Drawing, "drawW"))
                new Circle() {Color = Color.White, Radius = W.Range, BorderWidth = 2f}.Draw(_Player.Position);
            if (isChecked(MenuX.Drawing, "drawE"))
                new Circle() {Color = Color.White, Radius = E.Range, BorderWidth = 2f}.Draw(_Player.Position);

            if (isChecked(MenuX.Drawing, "writeKillable"))
            {
                foreach (var enemy in HeroManager.Enemies.Where(h => h.IsValid && h.IsHPBarRendered))
                {
                    var barPos = enemy.HPBarPosition;
                    var damage = getDamage(enemy);

                    if (damage > enemy.Health)
                    {
                        Drawing.DrawText(barPos.X,
                            barPos.Y + 35,
                            Color.Red,
                            "Killable with Combo");
                    }
                }
            }

        }


        private static float getDamage(Obj_AI_Base enemy)
        {
            float x = 0;
            if (Q.IsReady() && Q.IsLearned)
            {
                x = x + QDamage(enemy);
            }
            if (W.IsLearned && W.IsReady())
            {
                x = x + WDamage(enemy);
            }
            if (R.IsLearned && R.IsReady())
            {
                x = x + RDamage(enemy);
            }
            return x;
        }

        #endregion
        private static void Game_OnTick(EventArgs args)
        {
            if (isChecked(MenuX.Misc, "autoE")) autoE();
            KillSteal();
            Potions();
            
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Harass();
                    break;
                case Orbwalker.ActiveModes.LastHit:
                    LastHit();
                    break;
                case Orbwalker.ActiveModes.Flee:
                    Flee();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear();
                    break;
            }
        }

        #region Spells
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Skillshot E;
        public static Spell.Targeted R;
        public static Spell.Targeted Ignite;

        private static void LoadSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, (int)0.25f, 2000, 70);
            Q.AllowedCollisionCount = 0;
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, (int)1.25f, 2000, (int)225f); // 2k to test
            W.AllowedCollisionCount = int.MaxValue;
            E = new Spell.Skillshot(SpellSlot.E, 700, SkillShotType.Circular, (int)0.5f, int.MaxValue, 40);
            E.AllowedCollisionCount = int.MaxValue;
            R = new Spell.Targeted(SpellSlot.R, 650);
            if (_Player.GetSpellSlotFromName("summonerdot") != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(_Player.GetSpellSlotFromName("summonerdot"), 550);
            }

        }
        #endregion

        #region Misc
        private static void autoE()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (target == null) return;
            var pred = E.GetPrediction(target);
            if (pred.HitChance >= HitChance.High)
            {
                E.Cast(pred.CastPosition);
            }
        }

        private static void Flee()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (target == null) return;
            ECast(target);
        }
        #endregion

        #region Combo

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (target == null) return;
            if (isChecked(MenuX.Combo, "useRCombo"))
                RCast(target);
            if (isChecked(MenuX.Combo, "useECombo"))
                ECast(target);
            if (isChecked(MenuX.Combo, "useQCombo"))
                QCast(target);
            if (isChecked(MenuX.Combo, "useWCombo"))
            {
                if (isChecked(MenuX.Combo, "useWComboS"))
                    WTest();
                else
                {
                    WCast(target);
                }
            }
        }
        #endregion

        #region Casts
        private static void WCast(AIHeroClient target)
        {
            if (!target.IsValidTarget(W.Range) || !W.IsReady()) return;
            if (!_Player.HasBuffOfType(BuffType.Stun) && !_Player.HasBuffOfType(BuffType.Taunt) &&
                !_Player.HasBuffOfType(BuffType.Snare)) return;

            var pred = W.GetPrediction(target);
            if (pred.HitChance >= HitChance.Medium)
            {
                W.Cast(pred.CastPosition);
            }
        }
        private static void QCast(AIHeroClient target)
        {
            if (!target.IsValidTarget(Q.Range) || !Q.IsReady()) return;

            var prediction = Q.GetPrediction(target);
            if (prediction.HitChance >= HitChance.Medium)
            {
                Q.Cast(prediction.CastPosition);
            }
        }

        private static void ECast (AIHeroClient target)
        {
            if (!target.IsValidTarget(E.Range) || !E.IsReady()) return;
            var prediction = E.GetPrediction(target);
            var pos = prediction.CastPosition;

            if (pos.Distance(_Player.Position) < E.Range &&
                prediction.HitChance >= HitChance.Medium)
            {
                if (_Player.IsFacing(target) && target.IsFacing(_Player))
                {
                    E.Cast(pos.Extend(_Player.Position, 300).To3D());
                }
                else if (_Player.IsFacing(target) && !target.IsFacing(_Player))
                {
                    E.Cast(pos.Extend(_Player.Position, 150).To3D());
                }
                else if (!_Player.IsFacing(target) && target.IsFacing(_Player))
                {
                    E.Cast(pos.Extend(_Player.Position, 300).To3D());
                }
            }
        }
        private static void RCast (AIHeroClient target)
        {
            if (target.HasBuffOfType(BuffType.Invulnerability)) return;
            if (!R.IsReady() || !target.IsValidTarget(R.Range)) return;
            if (RDamage(target) < target.Health) return;

            R.Cast(target);
        }

        private static void WTest()
        {
            foreach (var target in HeroManager.Enemies.Where(
                x => x.IsValidTarget(W.Range) && !x.HasBuffOfType(BuffType.Invulnerability)))
            {
                var pred = W.GetPrediction(target);
                if (pred.HitChance >= HitChance.Immobile || pred.HitChance >= HitChance.Dashing)
                {
                    W.Cast(pred.CastPosition);
                }
             }
        }
        #endregion

        #region Veigar Damages LIB
        private static float RDamage (Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {250, 375, 500}[R.Level - 1] + 1.2*_Player.FlatMagicDamageMod
                    ));
        }

        private static float WDamage(Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {120, 170, 220, 270, 320}[W.Level - 1] + 1.0*_Player.FlatMagicDamageMod
                    ));
        }
        private static float QDamage(Obj_AI_Base target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {80, 125, 170, 215, 260}[Q.Level - 1] + 0.6*_Player.FlatMagicDamageMod
                    ));
        }
        #endregion

        #region Harass

        private static void Harass()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (target == null) return;
            if (isChecked(MenuX.Harass, "useQH") && getSliderValue(MenuX.Harass, "minManaH") <= _Player.ManaPercent)
                QCast(target);
            if (isChecked(MenuX.Harass, "useEH") && getSliderValue(MenuX.Harass, "minManaH") <= _Player.ManaPercent)
                ECast(target);
            if (isChecked(MenuX.Harass, "useWH") && getSliderValue(MenuX.Harass, "minManaH") <= _Player.ManaPercent)
            {
                if (isChecked(MenuX.Harass, "useWHS"))
                    WTest();
                else
                {
                    WCast(target);
                }
            }
        }
        
        #endregion

        #region KS
        private static void KillSteal()
        {
            foreach (var target in HeroManager.Enemies.Where(
                x => x.IsValidTarget(Q.Range) && !x.HasBuffOfType(BuffType.Invulnerability)))
            {
                if (!target.IsDead) return;

                if (target.Health <= QDamage(target) && Q.IsReady() && isChecked(MenuX.KillSteal, "ksQ"))
                {
                    Q.Cast(target);
                }
                if (target.Health <= WDamage(target) && W.IsReady() && isChecked(MenuX.KillSteal, "ksW"))
                {
                    W.Cast(target);
                }
                if (target.Health <= RDamage(target) && R.IsReady() && isChecked(MenuX.KillSteal, "ksR"))
                {
                    R.Cast(target);
                }
                if (target.Health <= _Player.GetSummonerSpellDamage(target, DamageLibrary.SummonerSpells.Ignite) &&
                    Ignite.IsReady() &&
                    isChecked(MenuX.KillSteal, "ksIG"))
                {
                    Ignite.Cast(target);
                }
            }
        }
        #endregion

        #region LastHit

        private static void LastHit()
        {
            if (isChecked(MenuX.LastHit, "farmQ") &&
                getSliderValue(MenuX.LastHit, "farmSlider") <= _Player.HealthPercent)
            {
                foreach (var m in ObjectManager.Get<Obj_AI_Minion>().Where(
                    x => x.IsEnemy && x.Distance(_Player) <= Q.Range
                    ).OrderBy(m => m.MaxHealth))
                {
                    if (m.IsDead) return;
                    var pred = Q.GetPrediction(m);
                    if (m.Health <= QDamage(m))
                        Q.Cast(pred.CastPosition);
                }
            }
        }
        #endregion

        #region LaneClear

        private static void LaneClear()
        {
            if (Q.IsReady() && isChecked(MenuX.LaneClear, "useQL") && getSliderValue(MenuX.LaneClear, "minML") <= _Player.HealthPercent)
            {
                foreach (var m in ObjectManager.Get<Obj_AI_Minion>().Where(
                    x => x.IsEnemy && x.Distance(_Player) <= Q.Range
                    ).OrderBy(m => m.MaxHealth))
                {
                    if (m.IsDead) return;
                    var pred = Q.GetPrediction(m);
                    if (m.Health <= QDamage(m))
                        Q.Cast(pred.CastPosition);
                }
            }
            var minions = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position.To2D(), Q.Range,
                true);
            if (minions.Count > 3 && W.IsReady() && isChecked(MenuX.LaneClear, "useWL") && getSliderValue(MenuX.LaneClear, "minML") <= _Player.HealthPercent)
            {
                foreach (var minion in minions.Where(x => x.IsValidTarget(W.Range + W.Width)))
                {
                    var pred = W.GetPrediction(minion);
                    if (pred.HitChancePercent >= 70 && WDamage(minion) > minion.Health)
                    {
                        W.Cast(pred.CastPosition);
                    }
                }
            }
            


        }

        #endregion

        #region Potions
        private static void Potions()
        {
            if (isChecked(MenuX.Misc, "AutoPot") && !_Player.IsInShopRange() && !_Player.HasBuff("recall"))
            {
                var hpPot = new Item(2003);
                var manaPot = new Item(2004);
                var biscuit = new Item(2010);
                if ((hpPot.IsReady() || biscuit.IsReady()) && (!_Player.HasBuff("RegenerationPotion") || _Player.HasBuff("ItemMiniRegenPotion")))
                {
                    if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                    {
                        if (Item.HasItem(hpPot.Id))
                        {
                            hpPot.Cast();
                        }
                        else
                        {
                            biscuit.Cast();
                        }
                    }
                    else if (_Player.Health < _Player.MaxHealth*0.6)
                    {
                        if (Item.HasItem(hpPot.Id))
                        {
                            hpPot.Cast();
                        }
                        else
                        {
                            biscuit.Cast();
                        }
                    }
                }
                if (manaPot.IsReady() && !_Player.HasBuff("FlaskOfCrystalWater"))
                {
                    if (_Player.Mana < _Player.MaxMana*0.6)
                    {
                        manaPot.Cast();
                    }
                }
            }
        }
        #endregion

        #region Utils
        public static bool isChecked(Menu obj, String value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }
        public static int getSliderValue(Menu obj, String value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }
        #endregion
    }
}

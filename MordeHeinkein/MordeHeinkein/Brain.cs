using System;
using System.Drawing;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace MordeHeinkein
{
    internal class Brain
    {
        #region Init

        public static void Init()
        {
            Bootstrap.Init(null);
            SetSpells();
            MenuX.GetMenu();
            Chat.Print("MordeHeinken Loaded!", Color.Purple);
            Game.OnTick += Game_OnTick;
            Orbwalker.OnPreAttack += OrbBeforeAttack;
            Game.OnUpdate += OnUpdate;
            MenuX.skinSelect.OnValueChange +=
                delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
                {
                    _Player.SetSkin(_Player.ChampionName, args.NewValue);
                };
            _Player.SetSkin(_Player.ChampionName, 5);
            Drawing.OnDraw += OnDraw;
        }

        private static void Game_OnTick(EventArgs args)
        {
            /*
            Potion
            */
            Potions();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Combo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Harass();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    LaneClear();
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    JungleClear();
                    break;
            }
        }

        #endregion

        #region PotionSSSSSOP

        public static void Potions()
        {
            if (isChecked(MenuX.Misc, "UsePot") && !_Player.IsInShopRange() &&
                !_Player.HasBuff("recall"))
            {
                if (Potion.IsReady() && !_Player.HasBuff("RegenerationPotion"))
                {
                    if (_Player.CountEnemiesInRange(700) > 0 && _Player.Health + 200 < _Player.MaxHealth)
                        Potion.Cast();
                    else if (_Player.Health < _Player.MaxHealth*0.6)
                        Potion.Cast();
                }
            }
        }

        #endregion

        #region Dmg LIB

        private static float RDamage(AIHeroClient target)
        {
            return _Player.CalculateDamageOnUnit(target, DamageType.Magical,
                (float) (new[] {0, 24, 29, 34}[R.Level] + 0.4*_Player.FlatMagicDamageMod));
        }

        #endregion

        #region Custom Casts

        private static void CastW()
        {
            if (W.IsReady() && _Player.Spellbook.GetSpell(SpellSlot.W).Name != "mordekaisercreepingdeath2")
            {
                foreach (var ally in HeroManager.Allies.Where(
                    a => !a.IsDead &&
                         !a.IsMe &&
                         a.Position.Distance(_Player.Position) < W.Range &&
                         a.CountEnemiesInRange(400) > 0))
                {
                    W.Cast(ally);
                }
                if (_Player.CountEnemiesInRange(400) > 0)
                {
                    W.Cast(_Player);
                }
            }
        }

        #endregion

        #region Custom Vars

        private static bool isAttacking;
        private static float RAttackDelay = 1200;

        private static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        private static Obj_AI_Base Dragon
        {
            get
            {
                if (_Player.Spellbook.GetSpell(SpellSlot.R).Name != "mordekaisercotgguide") return null;

                return ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(
                    m => m.Distance(_Player.Position) < 10000 &&
                         m.IsAlly &&
                         m.HasBuff("mordekaisercotgpetbuff2"));
            }
        }

        #endregion

        #region Events

        private static void OrbBeforeAttack(AttackableUnit unit, Orbwalker.PreAttackArgs args)
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo ||
                Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Harass ||
                Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.LaneClear ||
                Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.JungleClear)
            {
                if (unit.IsEnemy)
                {
                    isAttacking = true;
                }
                else
                {
                    isAttacking = false;
                }
            }
        }

        private static void OnUpdate(EventArgs args)
        {
            if (!isChecked(MenuX.Misc, "AutoPilot")) return;
            if (Dragon != null && R.IsReady())
            {
                var target = TargetSelector.GetTarget(4500, DamageType.Physical);
                if (target != null && (Environment.TickCount >= RAttackDelay))
                {
                    R.Cast(target);
                    RAttackDelay = Environment.TickCount + Dragon.AttackDelay * 1000;
                }
            }
        }

        #endregion

        #region Flags

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            if (Q.IsReady())
            {
                if (isAttacking &&
                target.IsValidTarget(_Player.GetAutoAttackRange()) &&
                isChecked(MenuX.Combo, "useQC"))
                {
                    Q.Cast();
                }
            }
            if (E.IsReady() &&
                target.IsValidTarget() &&
                isChecked(MenuX.Combo, "useEC"))
            {
                E.Cast(target);
            }

            /* 
            Logic W
            */
            if (isChecked(MenuX.Combo, "useWC") &&
                target.IsValidTarget())
                 CastW();


            if (target.IsValidTarget() &&
                (target.HealthPercent <= (RDamage(target) - (target.SpellBlock*0.1)) &&
                 isChecked(MenuX.Combo, "useRC")))
            {
                R.Cast(target);
            }
        }

        private static void Harass()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            if (target != null &&
                target.IsValidTarget())
            {
                if (_Player.HealthPercent >= getSliderValue(MenuX.Harass, "HPSliderH") &&
                    isChecked(MenuX.Harass, "useEH") &&
                    E.IsReady())
                {
                    E.Cast(target);
                }
                if (_Player.HealthPercent >= getSliderValue(MenuX.Harass, "HPSliderH") &&
                    isChecked(MenuX.Harass, "useQH") &&
                    Q.IsReady() &&
                    isAttacking)
                {
                    Q.Cast();
                }
                if (_Player.HealthPercent >= getSliderValue(MenuX.Harass, "HPSliderH") &&
                    isChecked(MenuX.Harass, "useWH"))
                {
                    CastW();
                }
            }
        }

        private static void LaneClear()
        {
            var minions = EntityManager.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position.To2D(), E.Range,
                true);
            if (minions.Count >= 3)
            {
                foreach (var minion in minions.Where(x => x.IsValidTarget(E.Range)).OrderBy(e => e.MaxHealth))
                {
                    if (E.IsReady() && isChecked(MenuX.LaneClear, "UseEL"))
                        E.Cast(minion.Position);

                    if (Q.IsReady() &&
                        isAttacking &&
                        minion.IsValidTarget(_Player.GetAutoAttackRange()) &&
                        isChecked(MenuX.LaneClear, "UseQL"))
                        Q.Cast();
                }
            }
        }

        private static void JungleClear()
        {
            foreach (var m in EntityManager.GetJungleMonsters(_Player.Position.To2D(), 1000f))
            {
                if (m.IsValidTarget())
                {
                    if (E.IsReady() && isChecked(MenuX.JungleClear, "UseEJ"))
                        E.Cast(m);
                    if (W.IsReady() && isChecked(MenuX.JungleClear, "UseWJ"))
                        W.Cast(_Player);
                    if (Q.IsReady() &&
                        isChecked(MenuX.JungleClear, "UseQJ") &&
                        isAttacking)
                        Q.Cast();
                }
            }
        }

        #endregion

        #region Spells

        public static Spell.Active Q;
        public static Spell.Targeted W;
        public static Spell.Skillshot E;
        public static Spell.Targeted R;
        public static Item Potion = new Item(2003);
        public static Spell.Targeted Ignite;

        private static void SetSpells()
        {
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Targeted(SpellSlot.W, 1000);
            E = new Spell.Skillshot(SpellSlot.E, 670, SkillShotType.Cone, (int) 0.25f, 2000, 12*2*(int) Math.PI/180);
            E.AllowedCollisionCount = int.MaxValue;
            R = new Spell.Targeted(SpellSlot.R, 1500);
            if (_Player.GetSpellSlotFromName("summonerdot") != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(_Player.GetSpellSlotFromName("summonerdot"), 550);
            }
        }

        #endregion

        #region Util Functions

        public static bool isChecked(Menu obj, string value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderValue(Menu obj, string value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }

        #endregion

        #region Drawings

        private static void OnDraw(EventArgs args)
        {
            if (_Player.IsDead) return;

            if (isChecked(MenuX.Drawing, "drawQ"))
                new Circle { Color = Color.White, Radius = Q.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (isChecked(MenuX.Drawing, "drawW"))
                new Circle { Color = Color.White, Radius = W.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (isChecked(MenuX.Drawing, "drawE"))
                new Circle { Color = Color.White, Radius = E.Range, BorderWidth = 2f }.Draw(_Player.Position);
            if (isChecked(MenuX.Drawing, "drawR"))
                new Circle { Color = Color.White, Radius = R.Range, BorderWidth = 2f }.Draw(_Player.Position);
        }

        #endregion
    }
}
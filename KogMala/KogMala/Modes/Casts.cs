using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using KogMala.AllahuAkbar;

namespace KogMala.Modes
{
    internal class Casts : Model
    {
        public static void SmartQ()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target.IsValidTarget() && target != null)
            {
                /* 
                Mana & Dmg Update
                */
                MananDamage.SetMana();
                MananDamage.SetDmg(target);

                if (target.IsValidTarget(Q.Range) && Qdmg + Edmg > target.Health)
                {
                    /*
                    get Spell Prediction
                    */
                    CastwithPred(Q, target);
                }
                /*
                Q with Mana Control
                */
                else if (_Player.Mana > Rmana + Qmana*2 + Emana)
                {
                    CastwithPred(Q, target);
                }
                else if (_Player.Mana > Rmana + Qmana + Emana)
                {
                    if (immobileTarget(target))
                    {
                        CastwithPred(Q, target);
                    }
                }
            }
        }

        public static void SmartW()
        {
            var WRange = 760 + 20*_Player.Spellbook.GetSpell(SpellSlot.W).Level;
            if (_Player.CountEnemiesInRange(WRange) > 0)
            {
                W.Cast();
            }
        }

        public static void SmartE()
        {
            var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
            if (target.IsValidTarget() && target != null)
            {
                /*
                Mana & Damage Manager
                */
                MananDamage.SetMana();
                MananDamage.SetDmg(target);

                if (Edmg > target.Health)
                    CastwithPred(E, target);
                else if (Edmg + Qdmg > target.Health && Q.IsReady())
                    CastwithPred(E, target);
                else if (_Player.Mana > Rmana + Wmana + Emana + Qmana + Emana)
                {
                    CastwithPred(E, target);
                }
                else if (_Player.Mana > Rmana + Qmana + Emana)
                {
                    if (immobileTarget(target))
                    {
                        E.Cast(target);
                    }
                }
            }
        }

        public static void SmartR()
        {
            var RRAnge = 800 + 300*_Player.Spellbook.GetSpell(SpellSlot.R).Level;
            var target = TargetSelector.GetTarget(RRAnge, DamageType.Magical);
            if (target == null || !target.IsValidTarget(RRAnge) || Orbwalker.IsAutoAttacking) return;

            /* 
                Mana & Dmg Update
                */
            MananDamage.SetMana();
            MananDamage.SetDmg(target);
            if (target.Health < Rdmg)
                CastwithPred(R, target);
            else if (Rdmg*2 > target.Health && _Player.Mana > Rmana*3)
                CastwithPred(R, target);
            else if (_Player.Mana > Rmana + Wmana + Emana + Qmana)
                CastwithPred(R, target);
            else if (_Player.Mana > Rmana*3)
            {
                if (immobileTarget(target))
                {
                    CastwithPred(R, target);
                }
            }
        }

        private static bool immobileTarget(Obj_AI_Base target)
        {
            if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                target.HasBuffOfType(BuffType.Knockup) ||
                target.HasBuffOfType(BuffType.Charm) || target.HasBuffOfType(BuffType.Fear) ||
                target.HasBuffOfType(BuffType.Knockback) ||
                target.HasBuffOfType(BuffType.Taunt) || target.HasBuffOfType(BuffType.Suppression))
            {
                return true;
            }
            return false;
        }

        private static void CastwithPred(Spell.Skillshot x, AIHeroClient t)
        {
            var pred = x.GetPrediction(t);
            if (pred.HitChance >= HitChance.High)
                x.Cast(pred.CastPosition);
        }
    }
}
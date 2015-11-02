using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace BRSelector.Model
{
    internal class Points
    {
        private static List<Targets.Heroes> _getHeroeses;

        static Points()
        {
            try
            {
                _getHeroeses = new List<Targets.Heroes>();
                Items = new HashSet<Heroes>
                {
                    new Heroes(
                        "Killable", "AA Killable", 20,
                        t => t.Health < ObjectManager.Player.GetAutoAttackDamage(t, true) ? 10 : 0),
                    new Heroes(
                        "AD", "Attack Damage", 15, delegate(AIHeroClient t)
                        {
                            var ad = t.FlatPhysicalDamageMod;
                            ad += ad/100*(t.Crit*100) * (t.InventoryItems.HasItem(ItemId.Infinity_Edge) ? 2.5f : 2f);
                            var armor = EntityManager.Heroes.Allies.Select(a => a.Armor).DefaultIfEmpty(0).Average()*
                                        t.PercentArmorPenetrationMod - t.FlatArmorPenetrationMod;
                            return (ad*(100/(100 + (armor > 0 ? armor : 0))))*t.AttackSpeedMod;
                        }),
                    new Heroes(
                        "AP", "Ability Power", 15, delegate(AIHeroClient t)
                        {
                            var mr = EntityManager.Heroes.Allies.Select(a => a.SpellBlock).DefaultIfEmpty(0).Average()*
                                     t.PercentMagicPenetrationMod - t.FlatMagicPenetrationMod;
                            return t.FlatMagicDamageMod * (100 / (100 + (mr > 0 ? mr : 0)));
                        }),
                    new Heroes("Low-resist",
                    "Resist", 3, t =>
                    ObjectManager.Player.FlatPhysicalDamageMod >= ObjectManager.Player.FlatMagicDamageMod ? t.Armor : t.SpellBlock),
                    new Heroes("low-healt", "Health", 20, t => t.Health),
                    new Heroes("CC", "Crowd Control", 3, delegate(AIHeroClient t)
                        {
                            var buffs =
                                t.Buffs.Where(
                                    x =>
                                        x.Type == BuffType.Charm || x.Type == BuffType.Knockback ||
                                        x.Type == BuffType.Suppression || x.Type == BuffType.Fear ||
                                        x.Type == BuffType.Taunt || x.Type == BuffType.Stun || x.Type == BuffType.Slow ||
                                        x.Type == BuffType.Silence || x.Type == BuffType.Snare ||
                                        x.Type == BuffType.Polymorph).ToList();
                return buffs.Any() ? buffs.Max(x => x.EndTime) + 1f : 0f;
            }),
                    new Heroes("gold", "Farming Machine", 3, t => (t.MinionsKilled + t.NeutralMinionsKilled * 22.35f + t.ChampionsKilled * 300f + t.Assists * 95f))
                };

            }
            catch (Exception ex) { Chat.Print(ex); }

        }

        public static float GetValue(Heroes item, Targets.Heroes target)
        {
            try
            {
                var value = item.GetValueFunc(target.Hero);
                return value > 1 ? value : 1;
            }
            catch (Exception ex)
            {
               Chat.Print(ex);
                return 0;
            }
        }

        public static float CalculatedWeight(Heroes item, Targets.Heroes target, bool simulation = false)
        {
            try
            {
                if (item.Value == 0)
                {
                    return 0;
                }
                var value = item.Value * GetValue(item, target) /  item.MaxValue;
                return float.IsNaN(value) || float.IsInfinity(value) ? 0 : value;
            }
            catch (Exception ex)
            {
                Chat.Print(ex);
            }
            return 0;
        }

        public static void UpdateMaxMinValue(Heroes item, IEnumerable<Targets.Heroes> targets, bool simulation = false)
        {
            try
            {
                var min = float.MaxValue;
                var max = float.MinValue;
                foreach (var target in targets)
                {
                    var value = GetValue(item, target);
                    if (value < min)
                    {
                        min = value;
                    }
                    if (value > max)
                    {
                        max = value;
                    }
                }
                if (!simulation)
                {
                    item.MinValue = min > 1 ? min : 1;
                    item.MaxValue = max > min ? max : min + 1;
                }
                else
                {
                    item.SimulationMinValue = min > 1 ? min : 1;
                    item.SimulationMaxValue = max > min ? max : min + 1;
                }
            }
            catch (Exception ex)
            {
                Chat.Print(ex);
            }
        }

        public static IEnumerable<Targets.Heroes> OrderChampions(List<Targets.Heroes> targets)
        {
            try
            {
                foreach (var item in Items.Where(w => w.Value >0))
                {
                   
                    UpdateMaxMinValue(item, targets);
                }

                foreach (var target in targets)
                {
                    var tmpvalue = Items.Where(w => w.Value > 0).Sum(w => CalculatedWeight(w, target));

                    target.Value = tmpvalue;
                }
                return targets.Count > 1
                    ? new List<Targets.Heroes>
                    {
                        targets.OrderByDescending(t => t.Value).First()
                    }
                    : targets.OrderByDescending(t => t.Value).ToList();
            }
            catch (Exception ex)
            {
                Chat.Print(ex);
            }
            return new List<Targets.Heroes>();
        }

        public static HashSet<Heroes> Items { get; private set; }
    }

    public class Heroes
    {
        public Heroes(string name, string displayname, int value, Func<AIHeroClient, float> getValue)
        {
            GetValueFunc = getValue;
            Name = name;
            DisplayName = displayname;
            Value = value;
        }
    
        public Func<AIHeroClient, float> GetValueFunc { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Value { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        public float SimulationMaxValue { get; set; }
        public float SimulationMinValue { get; set; }
    }
}

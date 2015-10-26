using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;

namespace BRSelector.Helpers
{
    public class Invulnerable
    {
        public static readonly HashSet<Item> Items = new HashSet<Item>
        {
            new Item(
                "Alistar", "FerociousHowl", null, false,
                (target, type) =>
                    ObjectManager.Player.CountEnemiesInRange(ObjectManager.Player.GetAutoAttackRange(target)) > 1),
            new Item(
                "MasterYi", "Meditate", null, false,
                (target, type) =>
                    ObjectManager.Player.CountEnemiesInRange(ObjectManager.Player.GetAutoAttackRange(target)) > 1),
            new Item("Tryndamere", "UndyingRage", null, false, (target, type) => target.HealthPercent < 5),
            new Item("Kayle", "JudicatorIntervention", null, false),
            new Item("Fizz", "fizztrickslamsounddummy", null, false),
            new Item("Vladimir", "VladimirSanguinePool", null, false),
            new Item(null, "BlackShield", DamageType.Magical, true),
            new Item(null, "BansheesVeil", DamageType.Magical, true),
            new Item("Sivir", "SivirE", null, true),
            new Item("Nocturne", "ShroudofDarkness", null, true)
        };

        public static bool Check(AIHeroClient target, DamageType damageType = DamageType.True, bool ignoreShields = true)
        {
            try
            {
                if (target.HasBuffOfType(BuffType.Invulnerability) || target.IsInvulnerable)
                {
                    return true;
                }
                foreach (var invulnerable in Items)
                {
                    if (invulnerable.Champion == null || invulnerable.Champion == target.ChampionName)
                    {
                        if (invulnerable.DamageType == null || invulnerable.DamageType == damageType)
                        {
                            if (!ignoreShields && invulnerable.IsShield && target.HasBuff(invulnerable.BuffName))
                            {
                                return true;
                            }
                            if (invulnerable.CustomCheck != null && CustomCheck(invulnerable, target, damageType))
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        private static bool CustomCheck(Item invulnerable, AIHeroClient target, DamageType damageType)
        {
            try
            {
                if (invulnerable != null)
                {
                    if (invulnerable.CustomCheck(target, damageType))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public class Item
        {
            public Item(string champion,
                string buffName,
                DamageType? damageType,
                bool isShield,
                Func<Obj_AI_Base, DamageType, bool> customCheck = null)
            {
                Champion = champion;
                BuffName = buffName;
                DamageType = damageType;
                IsShield = isShield;
                CustomCheck = customCheck;
            }

            public string Champion { get; set; }
            public string BuffName { get; private set; }
            public DamageType? DamageType { get; private set; }
            public bool IsShield { get; private set; }
            public Func<Obj_AI_Base, DamageType, bool> CustomCheck { get; private set; }
        }
    }
}
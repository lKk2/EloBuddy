using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;


namespace KayleBuddy
{
    class Brain
    {

        public static void Loading_OnLoadingComplete(EventArgs args)
        {
            if (Utils._Player.BaseSkinName.ToLower() != "kayle") return;
            Bootstrap.Init(null);
            Chat.Print("KayleBuddy LOADED", Color.DeepPink);
            MenuX.getMenu();
            Spells.GetSpells();
            Game.OnTick += Game_OnTick;
            Drawing.OnDraw += OnDraw;
            MenuX.skinSelect.OnValueChange += delegate(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs a)
            {
                Utils._Player.SetSkin(Utils._Player.ChampionName, a.NewValue);
            };
             Utils._Player.SetSkin(Utils._Player.ChampionName, 7);
        }

        private static void Game_OnTick(EventArgs args)
        {
            UltManager();
            HealingMachine();
            Potions();
            switch (Orbwalker.ActiveModesFlags)
            {
                case Orbwalker.ActiveModes.Combo:
                    Flags.Combo();
                    break;
                case Orbwalker.ActiveModes.Harass:
                    Flags.Harass();
                    break;
                case Orbwalker.ActiveModes.LaneClear:
                    Flags.WaveClear();
                    break;
                case Orbwalker.ActiveModes.JungleClear:
                    Flags.JungleClear();
                    break;
                case Orbwalker.ActiveModes.LastHit:
                    Flags.LastHit();
                    break;
            }
        }
        #region Pot
        private static void Potions()
        {
            if (Utils.isChecked(MenuX.MiscMenu, "usePot") && !Utils._Player.IsInShopRange() && !Utils._Player.HasBuff("recall"))
            {
                var hpPot = new Item(2003);
                var manaPot = new Item(2004);
                var biscuit = new Item(2010);
                if ((hpPot.IsReady() || biscuit.IsReady()) &&
                    (!Utils._Player.HasBuff("RegenerationPotion") || Utils._Player.HasBuff("ItemMiniRegenPotion")))
                {
                    if (Utils._Player.CountEnemiesInRange(700) > 0 && Utils._Player.Health + 200 < Utils._Player.MaxHealth)
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
                    else if (Utils._Player.Health < Utils._Player.MaxHealth * 0.6)
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
                if (manaPot.IsReady() && !Utils._Player.HasBuff("FlaskOfCrystalWater"))
                {
                    if (Utils._Player.Mana < Utils._Player.MaxMana * 0.6)
                    {
                        manaPot.Cast();
                    }
                }
            }
        }

        #endregion

        #region Ult/Heal Manager
        public static void UltManager()
        {
            if (Utils._Player.IsDead || !Spells.R.IsReady() || Utils._Player.IsRecalling) return;

            var getUlt = HeroManager.Allies.Where(
                h => !h.IsDead && !h.IsRecalling &&
                     h.Distance(Utils._Player) <= Spells.R.Range &&
                     !Utils._Player.IsRecalling &&
                     Utils.isChecked(MenuX.UltMenu, "UseR" + h.ChampionName) &&
                     h.HealthPercent <= Utils.getSliderValue(MenuX.UltMenu, "minHPR" + h.ChampionName) &&
                     Utils._Player.CountEnemiesInRange(1200) > 0).ToList();
            var allytoult = getUlt.OrderBy(x => x.Health).FirstOrDefault(x => !x.IsInShopRange());
            if (allytoult != null)
                Spells.R.Cast(allytoult);
        }

        private static void HealingMachine()
        {
            if (Utils._Player.IsDead || !Spells.W.IsReady() || Utils._Player.IsRecalling) return;

            var test = HeroManager.Allies.Where(
                hero => !hero.IsDead && !hero.IsInShopRange()
                        && !hero.IsZombie && !hero.IsRecalling && 
                        !Utils._Player.IsRecalling &&
                        hero.Distance(Utils._Player) <= Spells.W.Range &&
                        Utils.isChecked(MenuX.HealingMenu, "UseW" + hero.ChampionName) &&
                        hero.HealthPercent <= Utils.getSliderValue(MenuX.HealingMenu, "minHPW" + hero.ChampionName)
                ).ToList();
            var allytoheal = test.OrderBy(x => x.Health).FirstOrDefault(x => !x.IsInShopRange());
            if (allytoheal != null)
            {
                Spells.W.Cast(allytoheal);
            }
        }
        #endregion

        private static void OnDraw(EventArgs args)
        {
          if (Utils.isChecked(MenuX.MiscMenu, "drawQ"))
                new Circle { Color = Color.White, Radius = Spells.Q.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
            if (Utils.isChecked(MenuX.MiscMenu, "drawH"))
            {
                foreach (var h in HeroManager.Allies)
                {
                    var pos = h.HPBarPosition;
                    if (!h.IsDead && !h.IsMe &&
                        h.HealthPercent <= Utils.getSliderValue(MenuX.HealingMenu, "minHPW" + h.ChampionName))
                    {
                        Drawing.DrawText(pos.X + 110, pos.Y - 5, Color.Tomato, "H");
                    }
                }
            }
        }
    }
}

using System;
using BRSelector.Model;
using BRSelector.Model.Enum;
using BRSelector.Util;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace BRSelector
{
    public static class Selector
    {
        public static Menu menuTs,
            DrawMenu,
            SelectorMenu;

        public static External.Menu MenuExterno;

        internal static readonly string Version = "1.0.0";

        public static void Init()
        {
            MenuExterno = new External.Menu();

            menuTs = MainMenu.AddMenu("BR Selector", "BrSelector");

            menuTs.AddLabel("Version: " + Version);
            menuTs.AddSeparator();
            menuTs.AddLabel("By KK2 and Vector");

            /*
                Draw menu
            */

            DrawMenu = menuTs.AddSubMenu("Draws", "Draw");

            /*
                Draw Target Checkbox
            */

            DrawMenu.Add("showExternalMenu", new CheckBox("Show External Menu", true));
            var drawTarget = DrawMenu.Add("drawTarget", new CheckBox("Show target", true));
            MenuExterno.showTarget.Checked = drawTarget.CurrentValue;
            drawTarget.OnValueChange += delegate
            {
                MenuExterno.showTarget.Checked = Misc.IsChecked(DrawMenu, "drawTarget");
            };

            /*
                Draw Forced Target Checkbox
            */

            var drawForcedTarget = DrawMenu.Add("drawForcedTarget", new CheckBox("Mark forced target", true));
            MenuExterno.drawForcedTarget.Checked = drawForcedTarget.CurrentValue;
            drawForcedTarget.OnValueChange += delegate
            {
                MenuExterno.drawForcedTarget.Checked = Misc.IsChecked(DrawMenu, "drawForcedTarget");
            };


            /*
                Selector Menu
            */

            SelectorMenu = menuTs.AddSubMenu("Selector", "Selector");
            var forceTarget = SelectorMenu.Add("forceTarget", new CheckBox("Force Selected Target", true));
            MenuExterno.forceSelectedTarget.Checked = forceTarget.CurrentValue;
            forceTarget.OnValueChange += delegate
            {
                MenuExterno.forceSelectedTarget.Checked = Misc.IsChecked(SelectorMenu, "forceTarget");
            };

            /*
                Selector Type
            */

            var sliderValue = SelectorMenu.Add("selectorType", new Slider("Selector Type", 0, 0, 9));
            MenuExterno.selectorType.SelectedIndex = sliderValue.CurrentValue;
            sliderValue.OnValueChange += delegate
            {
                sliderValue.DisplayName = "Selector Type: " + Enum.GetName(typeof(EnumSelectorType), Misc.GetSliderValue(SelectorMenu, "selectorType"));
                MenuExterno.selectorType.SelectedIndex = Misc.GetSliderValue(SelectorMenu, "selectorType");
            };

            /*
                Priority slider by champion
            */

            var counter = 1;
            foreach (var aiHeroClient in EntityManager.Heroes.Enemies)
            {
                var aux = SelectorMenu.Add("ts" + aiHeroClient.ChampionName, new Slider(aiHeroClient.ChampionName, AutoPriority.GetPriority(aiHeroClient.ChampionName), 1, 4));

                switch (counter)
                {
                    case 1:
                        MenuExterno.trackBar1.Text = "ts" + aiHeroClient.ChampionName;
                        MenuExterno.champion1.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar1.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                        break;
                    case 2:
                        MenuExterno.trackBar2.Text = "ts" + aiHeroClient.ChampionName;
                        MenuExterno.champion2.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar2.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                        break;
                    case 3:
                        MenuExterno.trackBar3.Text = "ts" + aiHeroClient.ChampionName;
                        MenuExterno.champion3.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar3.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                        break;
                    case 4:
                        MenuExterno.trackBar4.Text = "ts" + aiHeroClient.ChampionName;
                        MenuExterno.champion4.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar4.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                        break;
                    case 5:
                        MenuExterno.trackBar5.Text = "ts" + aiHeroClient.ChampionName;
                        MenuExterno.champion5.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar5.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                        break;
                }

                aux.OnValueChange += delegate
                {
                    if (MenuExterno.trackBar1.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar1.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                    }
                    else if (MenuExterno.trackBar2.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar2.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                    }
                    else if (MenuExterno.trackBar3.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar3.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                    }
                    else if (MenuExterno.trackBar4.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar4.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                    }
                    else if (MenuExterno.trackBar5.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar5.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName) - 1;
                    }
                };
                counter++;
            }

            if(Misc.IsChecked(DrawMenu, "showExternalMenu"))
                MenuExterno.Show();
        }
    }
}

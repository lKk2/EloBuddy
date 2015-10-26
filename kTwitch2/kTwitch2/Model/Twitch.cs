using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BRSelector;
using EloBuddy;
using kTwitch2.Controller;
using kTwitch2.Helpers.DamageIndicator;
using SharpDX;
using SharpDX.Direct3D9;

namespace kTwitch2.Model
{

    internal class Twitch : Model
    {
        public static DamageIndicator Indicator;

        public void Init()
        {
            new Spells().Init();
            ModeManager.Initialize();
            ItemManager.Init();
            Selector.Init();
            MenuX.Init();
            new Drawings().Init();
            Obj_AI_Base.OnProcessSpellCast += AiHeroClientOnOnProcessSpellCast;
            GameObject.OnCreate += GameObjectOnOnCreate;
            GameObject.OnDelete += GameObjectOnOnDelete;
            Chat.Print("KK2 passou por aqui");
            Indicator = new DamageIndicator();
        }

        private void GameObjectOnOnDelete(GameObject sender, EventArgs args)
        {
            if ((sender.Name.ToLower().Contains("twitch_poison_counter_06.troy")))
            {
                CanCastE = false;
            }
        }

        private void GameObjectOnOnCreate(GameObject sender, EventArgs args)
        {
            if ((sender.Name.ToLower().Contains("twitch_poison_counter_06.troy")))
            {
                CanCastE = true;
            }
        }

        private static void AiHeroClientOnOnProcessSpellCast(Obj_AI_Base sender,
            GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
            if (args.Slot == SpellSlot.Q)
            {
                ItemManager.UseYomu();
            }
        }
    }
}

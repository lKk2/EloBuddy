using EloBuddy;
using SimpleSivir.Controller;
using SimpleSivir.Helpers;

namespace SimpleSivir.Model
{
    internal class Sivir
    {
        static Sivir()
        {
            if (Player.Instance.Hero != Champion.Sivir) return;
            Spells.Initiliaze();
            ModeManager.Initialize();
            ProcessSpells.Initialize();
            Config.Initialize();
            DamageIndicator.Initialize(Spells.GetTotalDmg);
        }

        public static void Initiliaze()
        {
        }
    }
}
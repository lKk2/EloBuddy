using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;

namespace Veigar
{
    class Drawings
    {
        public static void OnDraw(EventArgs args)
        {
            if (Utils.isChecked(MenuX.Drawing, "drawQ"))
                new Circle { Color = Color.White, Radius = Spells.Q.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
            if (Utils.isChecked(MenuX.Drawing, "drawW"))
                new Circle { Color = Color.White, Radius = Spells.W.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);
            if (Utils.isChecked(MenuX.Drawing, "drawE"))
                new Circle { Color = Color.White, Radius = Spells.E.Range, BorderWidth = 2f }.Draw(Utils._Player.Position);

            if (Utils.isChecked(MenuX.Drawing, "writeKillable"))
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
            if (MenuX.LastHit["farmQActive"].Cast<KeyBind>().CurrentValue)
            {
                Drawing.DrawText(Utils._Player.HPBarPosition.X, Utils._Player.HPBarPosition.Y + 50, Color.White, "FarmQ Active");
            }
        }


        private static float getDamage(Obj_AI_Base enemy)
        {
            float x = 0;
            if (Spells.Q.IsReady() && Spells.Q.IsLearned)
            {
                x = x + DamageLib.QDamage(enemy);
            }
            if (Spells.W.IsLearned && Spells.W.IsReady())
            {
                x = x + DamageLib.WDamage(enemy);
            }
            if (Spells.R.IsLearned && Spells.R.IsReady())
            {
                x = x + DamageLib.RDamage(enemy);
            }
            return x;
        }
    }
}

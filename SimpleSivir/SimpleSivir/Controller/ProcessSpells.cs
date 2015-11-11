using System;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using SimpleSivir.Helpers;

namespace SimpleSivir.Controller
{
    internal class ProcessSpells : Model.Model
    {
        static ProcessSpells()
        {
            if (!Config.Misc.UseE) return;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            getDB();
        }

        private static HashSet<string> DB { get; set; }

        public static void Initialize()
        {
        }

        private static void getDB()
        {
            DB = new HashSet<string>
            {
                "AhriSeduce"
                ,
                "InfernalGuardian"
                ,
                "EnchantedCrystalArrow"
                ,
                "InfernalGuardian"
                ,
                "EnchantedCrystalArrow"
                ,
                "RocketGrab"
                ,
                "BraumQ"
                ,
                "CassiopeiaPetrifyingGaze"
                ,
                "DariusAxeGrabCone"
                ,
                "DravenDoubleShot"
                ,
                "DravenRCast"
                ,
                "Dazzle"
                ,
                "EzrealTrueshotBarrage"
                ,
                "FizzMarinerDoom"
                ,
                "GnarBigW"
                ,
                "GnarR"
                ,
                "GragasR"
                ,
                "GravesChargeShot"
                ,
                "GravesClusterShot"
                ,
                "JarvanIVDemacianStandard"
                ,
                "JinxW"
                ,
                "JinxR"
                ,
                "KarmaQ"
                ,
                "KogMawLivingArtillery"
                ,
                "LeblancSlide"
                ,
                "LeblancSoulShackle"
                ,
                "LeonaSolarFlare"
                ,
                "LuxLightBinding"
                ,
                "LuxLightStrikeKugel"
                ,
                "LuxMaliceCannon"
                ,
                "UFSlash"
                ,
                "DarkBindingMissile"
                ,
                "NamiQ"
                ,
                "NamiR"
                ,
                "OrianaDetonateCommand"
                ,
                "RengarE"
                ,
                "rivenizunablade"
                ,
                "RumbleCarpetBombM"
                ,
                "SejuaniGlacialPrisonStart"
                ,
                "SionR"
                ,
                "ShenShadowDash"
                ,
                "SonaR"
                ,
                "StaticField"
                ,
                "ThreshQ"
                ,
                "ThreshEFlay"
                ,
                "VarusQMissilee"
                ,
                "VarusR"
                ,
                "VeigarBalefulStrike"
                ,
                "VelkozQ"
                ,
                "Vi-q"
                ,
                "Laser"
                ,
                "xeratharcanopulse2"
                ,
                "XerathArcaneBarrage2"
                ,
                "XerathMageSpear"
                ,
                "xerathrmissilewrapper"
                ,
                "yasuoq3w"
                ,
                "ZacQ"
                ,
                "ZedShuriken"
                ,
                "ZiggsQ"
                ,
                "ZiggsW"
                ,
                "ZiggsE"
                ,
                "ZiggsR"
                ,
                "ZileanQ"
                ,
                "ZyraQFissure"
                ,
                "ZyraGraspingRoots"
            };
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            try
            {
                if (sender.IsMe || sender.IsAlly || args.SData.IsAutoAttack()) return;
                var articunoPerfectCheck = _Player.Position.PointOnLineSegment(args.Start,
                    args.Start.Extend(args.End, args.SData.CastRangeDisplayOverride).To3D());
                if (DB.Contains(args.SData.Name) &&
                    E.IsReady() &&
                    (articunoPerfectCheck || (args.Target != null && args.Target.IsMe)))
                {
                    E.Cast();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Got Milk: " + ex);
            }
        }
    }
}
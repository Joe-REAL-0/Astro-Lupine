using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Combat;
using Godot;

namespace AstroLupine.Characters
{
    [HarmonyPatch(typeof(NCreature))]
    public static class AstroLupineAnimationPatch
    {
        [HarmonyPatch(nameof(NCreature._Ready))]
        [HarmonyPostfix]
        public static void ReadyPostfix(NCreature __instance)
        {
            if (__instance.Entity?.Player?.Character is AstroLupineCharacter)
            {
                var handler = new AstroLupineAnimHandler();
                handler.Name = "AstroLupineAnimHandler";
                handler.Visuals = __instance.Visuals;
                handler.VisualsRoot = __instance.Visuals.GetNode<Node2D>("%Visuals");
                handler.Sprite = __instance.Visuals.GetNode<Sprite2D>("%Visuals/Sprite2D");
                __instance.Visuals.AddChild(handler);
            }
        }

        [HarmonyPatch(nameof(NCreature.SetAnimationTrigger))]
        [HarmonyPostfix]
        public static void Postfix(NCreature __instance, string trigger)
        {
            if (__instance.Entity?.Player?.Character is AstroLupineCharacter)
            {
                var handler = __instance.Visuals.GetNodeOrNull<AstroLupineAnimHandler>("AstroLupineAnimHandler");
                if (handler != null)
                {
                    handler.PlayAnimation(trigger);
                }
            }
        }
    }
}

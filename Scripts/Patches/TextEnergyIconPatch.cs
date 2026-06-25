using HarmonyLib;
using MegaCrit.Sts2.addons.mega_text;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AstroLupine.Patches
{
    public static class TextEnergyIconHelper
    {
        public static string ModifyImagePath(string path, int fontSize)
        {
            if (path != null && path.Contains("text_energy_icon.png"))
            {
                if (fontSize <= 24) return path.Replace(".png", "_20.png");
                if (fontSize <= 32) return path.Replace(".png", "_30.png");
            }
            return path;
        }

        public static string ModifyBBCode(string bbcode, int fontSize)
        {
            if (bbcode != null && bbcode.Contains("text_energy_icon.png"))
            {
                if (fontSize <= 24) return bbcode.Replace("text_energy_icon.png", "text_energy_icon_20.png");
                if (fontSize <= 32) return bbcode.Replace("text_energy_icon.png", "text_energy_icon_30.png");
            }
            return bbcode;
        }
    }

    [HarmonyPatch(typeof(MegaRichTextLabel), "SetFontSize")]
    public static class MegaRichTextLabel_SetFontSize_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var parseBbcodeMethod = AccessTools.Method(typeof(Godot.RichTextLabel), "ParseBbcode");
            var modifyBbcodeMethod = AccessTools.Method(typeof(TextEnergyIconHelper), nameof(TextEnergyIconHelper.ModifyBBCode));

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Call && codes[i].operand == parseBbcodeMethod)
                {
                    // Stack before ParseBbcode: [this, string_text]
                    // We need to inject:
                    // ldarg.1 (the size argument)
                    // call ModifyBBCode
                    codes.Insert(i, new CodeInstruction(OpCodes.Ldarg_1));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, modifyBbcodeMethod));
                    break;
                }
            }
            return codes;
        }
    }

    [HarmonyPatch(typeof(MegaLabelHelper), "EstimateTextSize", new[] { typeof(Godot.TextParagraph), typeof(List<MegaCrit.Sts2.Core.Entities.Text.BbcodeObject>), typeof(Godot.Font), typeof(int), typeof(float), typeof(float) })]
    public static class MegaLabelHelper_EstimateTextSize_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            var getTextureMethod = AccessTools.Method(typeof(MegaCrit.Sts2.Core.Assets.AssetCache), "GetTexture2D");
            var modifyImagePathMethod = AccessTools.Method(typeof(TextEnergyIconHelper), nameof(TextEnergyIconHelper.ModifyImagePath));

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Callvirt && codes[i].operand == getTextureMethod)
                {
                    // Stack before GetTexture2D: [PreloadManager.Cache instance, string_path]
                    // We need to inject:
                    // ldarg.3 (fontSize is the 4th argument in EstimateTextSize: TextParagraph, List<BbcodeObject>, Font, int fontSize)
                    // call ModifyImagePath
                    codes.Insert(i, new CodeInstruction(OpCodes.Ldarg_3));
                    codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, modifyImagePathMethod));
                    break;
                }
            }
            return codes;
        }
    }
}

using HarmonyLib;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Entities.Ancients;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Characters;
using System.Reflection;

namespace AstroLupine.Patches
{
    [HarmonyPatch]
    public static class AncientsDialoguePatch
    {
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(TheArchitect), "DefineDialogues");
            yield return AccessTools.Method(typeof(Darv), "DefineDialogues");
            yield return AccessTools.Method(typeof(Neow), "DefineDialogues");
            yield return AccessTools.Method(typeof(Nonupeipe), "DefineDialogues");
            yield return AccessTools.Method(typeof(Orobas), "DefineDialogues");
            yield return AccessTools.Method(typeof(Pael), "DefineDialogues");
            yield return AccessTools.Method(typeof(Tanx), "DefineDialogues");
            yield return AccessTools.Method(typeof(Tezcatara), "DefineDialogues");
            yield return AccessTools.Method(typeof(Vakuu), "DefineDialogues");
        }

        public static void Postfix(ref AncientDialogueSet __result)
        {
            string charId = ModelDb.Character<AstroLupineCharacter>().Id.Entry;
            
            // 构造统一的占位对话，避免报错，并且提供给本地化文件映射键值
            var dialogues = new List<AncientDialogue>
            {
                new AncientDialogue("", "")
                {
                    VisitIndex = 0,
                    EndAttackers = ArchitectAttackers.Both,
                    IsRepeating = true
                }
            };
            
            if (!__result.CharacterDialogues.ContainsKey(charId))
            {
                __result.CharacterDialogues.Add(charId, dialogues);
            }
        }
    }
}

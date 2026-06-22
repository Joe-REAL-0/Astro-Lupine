using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace AstroLupine.Cards
{
    public static class AstroLupineKeywords
    {
        [CustomEnum("read")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Read;

        [CustomEnum("write")]
        [KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Write;

        [CustomEnum("attack_register")] public static CardKeyword AttackRegister;
        [CustomEnum("defense_register")] public static CardKeyword DefenseRegister;
        [CustomEnum("draw_register")] public static CardKeyword DrawRegister;
        [CustomEnum("trojan_horse_virus")] public static CardKeyword TrojanHorseVirus;
        [CustomEnum("zero_day_exploit")] public static CardKeyword ZeroDayExploit;
        [CustomEnum("auto_maintenance")] public static CardKeyword AutoMaintenance;
        [CustomEnum("attack_accumulator")] public static CardKeyword AttackAccumulator;
        [CustomEnum("defense_accumulator")] public static CardKeyword DefenseAccumulator;
        [CustomEnum("attack_overwrite")] public static CardKeyword AttackOverwrite;
        [CustomEnum("defense_overwrite")] public static CardKeyword DefenseOverwrite;
        [CustomEnum("draw_overwrite")] public static CardKeyword DrawOverwrite;
        [CustomEnum("overwrite")] public static CardKeyword Overwrite;
    }
}

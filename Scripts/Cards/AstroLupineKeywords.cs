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
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Starter
{
    public class DefendAstroLupine : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Defend";

        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadBlockVar(0m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public DefendAstroLupine()
            : base(1, CardType.Skill, CardRarity.Basic, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            // Call our custom hook from the base class to gain Read Block
            await GainReadBlock(cardPlay, this.DynamicVars.Block);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(3m);
        }
    }
}

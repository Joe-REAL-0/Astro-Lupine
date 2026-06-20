using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class ParallelProcessing : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-PARALLEL_PROCESSING";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new CardKeyword[0];

        public ParallelProcessing() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.None) {}

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<ParallelProcessingPower>(choiceContext, cardPlay.Card.Owner.Creature, 1, cardPlay.Card.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.UpgradeBy(-1);
        }
    }
}

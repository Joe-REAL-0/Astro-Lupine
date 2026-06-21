using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AstroLupine.Cards.Rare
{
    public class LowLevelRefactoring : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_LowLevelRefactoring";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, CardKeyword.Retain };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[0];

        public LowLevelRefactoring()
            : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                var hand = PileType.Hand.GetPile(Owner);
                if (hand != null)
                {
                    foreach (var card in hand.Cards)
                    {
                        if (card.Type == CardType.Attack)
                        {
                            CardCmd.ApplyKeyword(card, AstroLupineKeywords.Write);
                            if (card is BaseAstroLupineCard astroCard)
                            {
                                astroCard.HasWriteTag = true;
                            }
                        }
                    }
                }
            }
            await Task.CompletedTask;
        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.SetCustomBaseCost(1);
        }
    }
}

using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class TuringComplete : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_TuringComplete";

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new CardsVar(1)
        };

        public TuringComplete()
            : base(1, CardType.Power, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<TuringCompletePower>(choiceContext, Owner.Creature, this.DynamicVars["Cards"].IntValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["Cards"].UpgradeValueBy(1m);
        }
    }
}

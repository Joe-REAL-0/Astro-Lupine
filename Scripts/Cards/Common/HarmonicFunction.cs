using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class HarmonicFunction : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_HarmonicFunction";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, CardKeyword.Exhaust };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new EnergyVar(1),
            new AstroReadCardsVar(0)
        };



        public HarmonicFunction()
            : base(0, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                await PlayerCmd.GainEnergy(1, Owner);
                await DrawReadCards(choiceContext, this.DynamicVars["Cards"]);
            }
        }

        protected override void OnUpgrade()
        {
            this.RemoveKeyword(CardKeyword.Exhaust);
        }
    }
}

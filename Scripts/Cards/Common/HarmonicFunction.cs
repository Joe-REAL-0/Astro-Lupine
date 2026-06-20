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
            new AstroReadMagicVar(0m),
            new MagicVar(2m) // Energy gain
        };



        public HarmonicFunction()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                await PlayerCmd.GainEnergy(this.DynamicVars["Magic2"].PreviewValue, Owner);
                await DrawReadCards(choiceContext, this.DynamicVars["Magic"]);
            }
        }

        protected override void OnUpgrade()
        {
            this.RemoveKeyword(CardKeyword.Exhaust);
        }
    }
}

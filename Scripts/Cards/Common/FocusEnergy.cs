using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class FocusEnergy : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_FocusEnergy";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Ethereal };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(6m) 
        };

        public FocusEnergy()
            : base(3, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
                await PlayerCmd.GainEnergy(this.DynamicVars["Magic"].PreviewValue, Owner);
        }

        protected override void OnUpgrade()
        {
            this.RemoveKeyword(CardKeyword.Ethereal);
        }
    }
}

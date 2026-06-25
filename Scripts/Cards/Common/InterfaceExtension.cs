using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Localization;

namespace AstroLupine.Cards.Common
{
    public class InterfaceExtension : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_InterfaceExtension";

        public InterfaceExtension()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            var prefs = new CardSelectorPrefs(new LocString("gameplay_ui", "ASTROLUPINE-CHOOSE_CARD_TO_WRITE_AND_EXHAUST"), 1);
            var selectedCards = await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => c != this, this);
            
            var targetCard = selectedCards.FirstOrDefault();
            if (targetCard != null)
            {
                if (!targetCard.Keywords.Contains(AstroLupineKeywords.Write)) CardCmd.ApplyKeyword(targetCard, AstroLupineKeywords.Write);
                if (!targetCard.Keywords.Contains(CardKeyword.Exhaust)) CardCmd.ApplyKeyword(targetCard, CardKeyword.Exhaust);
            }
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.SetCustomBaseCost(0);
        }
    }
}

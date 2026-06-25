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
    public class MountHardDrive : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Overwrite";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

        public MountHardDrive()
            : base(0, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            var prefs = new CardSelectorPrefs(new LocString("gameplay_ui", "ASTROLUPINE-CHOOSE_CARD_TO_WRITE"), 1);
            var selectedCards = await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => c != this && !c.Keywords.Contains(AstroLupineKeywords.Write), this);
            
            var targetCard = selectedCards.FirstOrDefault();
            if (targetCard != null)
            {
                CardCmd.ApplyKeyword(targetCard, AstroLupineKeywords.Write);
            }
        }

        protected override void OnUpgrade()
        {
            this.AddKeyword(CardKeyword.Retain);
        }
    }
}

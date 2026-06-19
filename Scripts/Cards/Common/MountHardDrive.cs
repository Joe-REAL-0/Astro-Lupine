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
        public const string CardId = "AstroLupine_Card_MountHardDrive";

        public MountHardDrive()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            if (this.IsUpgraded)
            {
                // Upgrade: Apply Read to all cards in hand
                if (Owner.PlayerCombatState != null)
                {
                    foreach (CardModel card in Owner.PlayerCombatState.Hand.Cards)
                    {
                    if (card != this && !card.Keywords.Contains(AstroLupineKeywords.Read))
                    {
                        CardCmd.ApplyKeyword(card, AstroLupineKeywords.Read);
                    }
                }
                }
            }
            else
            {
                // Basic: Select 1 card in hand to apply Read
                var prefs = new CardSelectorPrefs(new LocString("gameplay_ui", "CHOOSE_CARD_UPGRADE_HEADER"), 1);
                var selectedCards = await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => c != this && !c.Keywords.Contains(AstroLupineKeywords.Read), this);
                
                var targetCard = selectedCards.FirstOrDefault();
                if (targetCard != null)
                {
                    CardCmd.ApplyKeyword(targetCard, AstroLupineKeywords.Read);
                }
            }
        }

        protected override void OnUpgrade()
        {
            // Upgraded version affects all cards in hand. No cost or number change needed here, just logic branch in OnPlay.
        }
    }
}

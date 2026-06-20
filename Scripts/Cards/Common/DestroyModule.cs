using System.Collections.Generic;
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
    public class DestroyModule : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_DestroyModule";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadBlockVar(5m),
            new MagicVar(1m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public DestroyModule()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;
            
            int exhaustAmount = (int)this.DynamicVars["Magic"].PreviewValue;
            var prefs = new CardSelectorPrefs(new LocString("gameplay_ui", "CHOOSE_CARD_TO_EXHAUST"), exhaustAmount);
            var selectedCards = await CardSelectCmd.FromHand(choiceContext, Owner, prefs, c => c != this, this);
            foreach (var card in selectedCards)
            {
                await CardCmd.Exhaust(choiceContext, card);
            }
            await GainReadBlock(cardPlay, this.DynamicVars.Block);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(2m);
        }
    }
}

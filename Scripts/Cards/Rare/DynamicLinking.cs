using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Rare
{
    public class DynamicLinking : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_DynamicLinking";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, CardKeyword.Ethereal, AstroLupineKeywords.Read };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new MagicVar(0m)
        };

        public DynamicLinking()
            : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null && Owner.PlayerCombatState != null)
            {
                int drawnAmount = await DrawReadCards(choiceContext, base.DynamicVars["Magic"]);
                
                int readCardsPlayed = MegaCrit.Sts2.Core.Combat.CombatManager.Instance.History.CardPlaysStarted.Count(e => e.HappenedThisTurn(this.CombatState) && e.CardPlay.Card.Owner == this.Owner && e.CardPlay.Card.Keywords.Contains(AstroLupineKeywords.Read));
                
                if (readCardsPlayed > 0)
                {
                    Owner.PlayerCombatState.GainEnergy(readCardsPlayed);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.RemoveKeyword(CardKeyword.Ethereal);
        }
    }
}

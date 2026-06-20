using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.CardSelection;

namespace AstroLupine.Cards.Rare
{
    public class ConsoleAgent : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_ConsoleAgent";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.Read };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DynamicVar("Magic", 0m)
        };

        protected override bool HasEnergyCostX => true;

        public ConsoleAgent()
            : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null && Owner.PlayerCombatState != null)
            {
                int xValue = ResolveEnergyXValue();
                int bonusDraw = this.IsUpgraded ? 1 : 0;
                
                var tempMagicVar = new MagicVar(xValue + bonusDraw);
                int drawnCards = await DrawReadCards(choiceContext, tempMagicVar);
                
                if (drawnCards > 0)
                {
                    Owner.PlayerCombatState.GainEnergy(drawnCards);
                }
            }
        }

        protected override void OnUpgrade()
        {
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class LupineIntuition : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-LUPINE_INTUITION";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new MagicVar(3) };

        public LupineIntuition() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.None) {}

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<LupineIntuitionPower>(choiceContext, cardPlay.Card.Owner.Creature, this.DynamicVars["Magic"].IntValue, cardPlay.Card.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1);
        }
    }
}

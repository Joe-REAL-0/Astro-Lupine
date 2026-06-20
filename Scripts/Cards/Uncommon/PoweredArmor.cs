using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class PoweredArmor : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-POWERED_ARMOR";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new MagicVar(4) };

        public PoweredArmor() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.None) {}

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AttackAccumulatorPower>(choiceContext, cardPlay.Card.Owner.Creature, this.DynamicVars["Magic"].IntValue, cardPlay.Card.Owner.Creature, this);
            await PowerCmd.Apply<DefenseAccumulatorPower>(choiceContext, cardPlay.Card.Owner.Creature, this.DynamicVars["Magic"].IntValue, cardPlay.Card.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(2);
        }
    }
}

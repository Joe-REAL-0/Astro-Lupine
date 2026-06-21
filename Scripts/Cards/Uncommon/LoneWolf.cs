using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class LoneWolf : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-LONE_WOLF";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new MagicVar(3) };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.DefenseAccumulator, AstroLupineKeywords.AttackAccumulator };

        public LoneWolf() : base(3, CardType.Power, CardRarity.Uncommon, TargetType.None) {}

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AttackAccumulatorPower>(choiceContext, cardPlay.Card.Owner.Creature, this.DynamicVars["Magic"].IntValue, cardPlay.Card.Owner.Creature, this);
            await PowerCmd.Apply<DefenseAccumulatorPower>(choiceContext, cardPlay.Card.Owner.Creature, this.DynamicVars["Magic"].IntValue, cardPlay.Card.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.UpgradeBy(-1);
        }
    }
}

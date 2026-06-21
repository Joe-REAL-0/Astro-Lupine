using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class Hack : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-HACK";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DynamicVar("Debuff", 2m),
            new BlockVar(8m, ValueProp.Move)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.ZeroDayExploit };

        public Hack()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target != null && Owner?.Creature != null)
            {
                await PowerCmd.Apply<ZeroDayExploitPower>(choiceContext, cardPlay.Target, this.DynamicVars["Debuff"].IntValue, Owner.Creature, this);
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(3m);
        }
    }
}

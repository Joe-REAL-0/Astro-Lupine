using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AstroLupine.Cards.Uncommon
{
    public class OperatorImpact : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-OPERATOR_IMPACT";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(2m),
            new BlockVar(0m, ValueProp.Move)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public OperatorImpact()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_slash");

            bool playedSkill = CombatManager.Instance.History.CardPlaysStarted.Any(e => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Owner == base.Owner && e.CardPlay.Card.Type == CardType.Skill);
            
            if (playedSkill)
            {
                await CreatureCmd.GainBlock(Owner!.Creature, this.DynamicVars.Block, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(2m);
            this.DynamicVars.Block.UpgradeValueBy(2m);
        }
    }
}



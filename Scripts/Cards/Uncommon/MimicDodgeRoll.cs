using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class MimicDodgeRoll : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_DODGE_ROLL";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new BlockVar(6m, ValueProp.Move)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        public MimicDodgeRoll()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block, cardPlay);
                await WriteDefenseRegister(this.DynamicVars.Block.IntValue);
                
                await PowerCmd.Apply<AutoMaintenancePower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(2m);
        }
    }
}

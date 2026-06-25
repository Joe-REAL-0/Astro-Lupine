using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Common
{
    public class KineticConversion : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_KineticConversion";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new BlockVar(14m, ValueProp.Move),
            new DynamicVar("Magic", 8m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.AttackOverwrite };

        public KineticConversion()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                // Gain 12 Block
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block.PreviewValue, ValueProp.Move, cardPlay);
                // Apply Attack Overwrite
                await PowerCmd.Apply<AttackOverwritePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(3m);
        }
    }
}

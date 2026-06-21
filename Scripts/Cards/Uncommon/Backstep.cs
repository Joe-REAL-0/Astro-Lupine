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
    public class Backstep : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-BACKSTEP";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new BlockVar(13m, ValueProp.Move),
            new MagicVar(5m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write, AstroLupineKeywords.DefenseOverwrite };

        public Backstep()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block, cardPlay);
                await WriteDefenseRegister(this.DynamicVars.Block.IntValue);
                await PowerCmd.Apply<DefenseOverwritePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(4m);
        }
    }
}

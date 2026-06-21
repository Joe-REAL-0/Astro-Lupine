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
    public class MimicAfterimage : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_AFTERIMAGE";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadBlockVar(2m, ValueProp.Move)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.DefenseRegister };

        public MimicAfterimage()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block, cardPlay);
                
                int defReg = Owner.Creature.GetPower<DefenseRegisterPower>()?.Read() ?? 0;
                if (defReg > 0)
                {
                    await PowerCmd.Apply<BlockNextTurnPower>(choiceContext, Owner.Creature, defReg, Owner.Creature, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(2m);
        }
    }
}

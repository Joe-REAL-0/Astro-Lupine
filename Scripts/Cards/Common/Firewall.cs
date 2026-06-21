using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Cards.Common
{
    public class Firewall : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Firewall";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new BlockVar(8m, ValueProp.Move) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        public Firewall()
            : base(2, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block.PreviewValue, ValueProp.Move, cardPlay);
                await WriteDefenseRegister((int)this.DynamicVars.Block.PreviewValue);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(4m);
        }
    }
}

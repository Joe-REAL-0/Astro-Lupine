using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AstroLupine.Cards.Rare
{
    public class Redirect : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Redirect";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.Read, AstroLupineKeywords.KernelHardening };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new BlockVar(17m, ValueProp.Move),
            new DynamicVar("Magic", 2m)
        };

        public Redirect()
            : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await GainReadBlock(cardPlay, base.DynamicVars.Block.BaseValue);
            
            if (Owner != null && Owner.Creature != null)
            {
                await PowerCmd.Apply<KernelHardeningPower>(choiceContext, Owner.Creature, (int)base.DynamicVars["Magic"].BaseValue, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Block.UpgradeValueBy(4m);
        }
    }
}

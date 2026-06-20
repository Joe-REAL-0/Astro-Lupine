using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class EnergyBarrier : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_EnergyBarrier";

        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadBlockVar(2m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public EnergyBarrier()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await GainReadBlock(cardPlay, this.DynamicVars.Block.BaseValue);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(3m);
        }
    }
}

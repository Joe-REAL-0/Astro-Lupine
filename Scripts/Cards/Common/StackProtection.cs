using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class StackProtection : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_StackProtection";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadBlockVar(1m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.Write };

        public StackProtection()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int blockGained = (int)this.DynamicVars.Block.PreviewValue;
            await GainReadBlock(cardPlay, this.DynamicVars.Block.BaseValue);
            await WriteDefenseRegister(blockGained);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(1m);
        }
    }
}

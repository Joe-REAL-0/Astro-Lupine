using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class SandboxMode : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_SandboxMode";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DynamicVar("Magic", 2m)
        };

        public SandboxMode()
            : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null && Owner.Creature != null)
            {
                await PowerCmd.Apply<SandboxModePower>(choiceContext, Owner.Creature, (int)base.DynamicVars["Magic"].BaseValue, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

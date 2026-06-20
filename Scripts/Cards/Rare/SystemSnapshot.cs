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
    public class SystemSnapshot : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_SystemSnapshot";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[0];

        public SystemSnapshot()
            : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null && Owner.Creature != null)
            {
                await PowerCmd.Apply<SystemSnapshotPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.AddKeyword(CardKeyword.Retain);
        }
    }
}

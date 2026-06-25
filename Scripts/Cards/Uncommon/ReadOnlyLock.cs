using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class ReadOnlyLock : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-READ_ONLY_LOCK";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Retain };

        public ReadOnlyLock()
            : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                await PowerCmd.Apply<ReadOnlyLockPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.AddKeyword(CardKeyword.Innate);
        }
    }
}

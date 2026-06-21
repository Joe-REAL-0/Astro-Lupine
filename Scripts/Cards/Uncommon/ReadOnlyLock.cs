using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class ReadOnlyLock : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-READ_ONLY_LOCK";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new MagicVar(6) };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.DefenseRegister, AstroLupineKeywords.AttackRegister };

        public ReadOnlyLock() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.None) {}

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var power = cardPlay.Card.Owner.Creature.GetPower<ReadOnlyLockPower>();
            if (power == null || power.Amount < this.DynamicVars["Magic"].IntValue)
            {
                await PowerCmd.Apply<ReadOnlyLockPower>(choiceContext, cardPlay.Card.Owner.Creature, this.DynamicVars["Magic"].IntValue, cardPlay.Card.Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(2);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class MimicOverclock : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_OVERCLOCK";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new CardsVar(3),
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

        public MimicOverclock()
            : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            await CardPileCmd.Draw(choiceContext, this.DynamicVars["Cards"].IntValue, Owner);

            if (Owner.Creature != null)
            {
                await PowerCmd.Apply<AttackOverwritePower>(choiceContext, Owner.Creature, 8, Owner.Creature, this);
                await PowerCmd.Apply<DefenseOverwritePower>(choiceContext, Owner.Creature, 6, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Cards"].UpgradeValueBy(1m);
        }
    }
}

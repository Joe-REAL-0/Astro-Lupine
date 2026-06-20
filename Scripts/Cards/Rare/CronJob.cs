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
    public class CronJob : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_CronJob";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DynamicVar("Magic", 6m),
            new DynamicVar("Magic2", 5m),
            new DynamicVar("Magic3", 2m)
        };

        public CronJob()
            : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null && Owner.Creature != null)
            {
                await PowerCmd.Apply<AttackOverwritePower>(choiceContext, Owner.Creature, (int)base.DynamicVars["Magic"].BaseValue, Owner.Creature, this);
                await PowerCmd.Apply<DefenseOverwritePower>(choiceContext, Owner.Creature, (int)base.DynamicVars["Magic2"].BaseValue, Owner.Creature, this);
                await PowerCmd.Apply<DrawOverwritePower>(choiceContext, Owner.Creature, (int)base.DynamicVars["Magic3"].BaseValue, Owner.Creature, this);
                
                await PowerCmd.Apply<CronJobExtraTurnPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.SetCustomBaseCost(2);
        }
    }
}

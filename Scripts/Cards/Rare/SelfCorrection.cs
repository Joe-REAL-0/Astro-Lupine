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
    public class SelfCorrection : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_SelfCorrection";

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DynamicVar("Magic", 1m)
        };

        public SelfCorrection()
            : base(2, CardType.Power, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AutoMaintenancePower>(choiceContext, Owner.Creature, (int)base.DynamicVars["Magic"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

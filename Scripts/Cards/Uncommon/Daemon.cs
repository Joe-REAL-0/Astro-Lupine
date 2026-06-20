using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class Daemon : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-DAEMON";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(2m)
        };

        public Daemon()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            await PowerCmd.Apply<AutoMaintenancePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

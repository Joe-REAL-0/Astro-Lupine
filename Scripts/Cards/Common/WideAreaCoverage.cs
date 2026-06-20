using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class WideAreaCoverage : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_WideAreaCoverage";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(7m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        public WideAreaCoverage()
            : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_slash_fast");

            // Write to Attack Register
            await WriteAttackRegister((int)this.DynamicVars.Damage.PreviewValue);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(2m);
        }
    }
}

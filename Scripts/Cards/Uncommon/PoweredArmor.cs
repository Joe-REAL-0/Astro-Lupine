using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Uncommon
{
    public class PoweredArmor : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-POWERED_ARMOR";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] { new MagicVar(3) };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.AttackRegister, AstroLupineKeywords.DefenseRegister };

        public PoweredArmor() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.None) {}

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int amount = (int)this.DynamicVars["Magic"].PreviewValue;
            await IncAttackRegister(amount);
            await IncDefenseRegister(amount);
            
            await Task.CompletedTask;
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(2);
        }
    }
}

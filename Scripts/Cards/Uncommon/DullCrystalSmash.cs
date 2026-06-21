using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class DullCrystalSmash : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-DULL_CRYSTAL_SMASH";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(2m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.AttackRegister };

        public DullCrystalSmash()
            : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int regAmount = Owner?.Creature?.GetPower<AttackRegisterPower>()?.Read() ?? 0;
            int times = 1 + (regAmount / 8);

            for (int i = 0; i < times; i++)
            {
                await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_blunt");
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(2m);
        }
    }
}

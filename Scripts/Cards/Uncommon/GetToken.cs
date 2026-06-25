using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Linq;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class GetToken : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-GET_TOKEN";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DamageVar(12m, ValueProp.Move) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write,AstroLupineKeywords.TrojanHorseVirus };

        public GetToken()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override bool ShouldGlowGoldInternal => base.CombatState != null && base.CombatState.HittableEnemies.Any(e => e.GetPower<TrojanHorseVirusPower>() != null);

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            if (cardPlay.Target.GetPower<TrojanHorseVirusPower>() != null)
            {
                await PlayerCmd.GainEnergy(1, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(3m);
        }
    }
}



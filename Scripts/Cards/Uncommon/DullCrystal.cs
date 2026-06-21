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
    public class DullCrystal : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-DULL_CRYSTAL";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DamageVar(14m, ValueProp.Move) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write, AstroLupineKeywords.TrojanHorseVirus };

        public DullCrystal()
            : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);

            await WriteAttackRegister(this.DynamicVars.Damage.IntValue);

            await PowerCmd.Apply<TrojanHorseVirusPower>(choiceContext, cardPlay.Target, 2, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(4m);
        }
    }
}



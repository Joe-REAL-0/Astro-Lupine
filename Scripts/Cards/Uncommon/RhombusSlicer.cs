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
    public class RhombusSlicer : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-RHOMBUS_SLICER";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DamageVar(10m, ValueProp.Move),
            new MagicVar(2m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write, AstroLupineKeywords.ZeroDayExploit };

        public RhombusSlicer()
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

            await PowerCmd.Apply<ZeroDayExploitPower>(choiceContext, cardPlay.Target, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(4m);
        }
    }
}



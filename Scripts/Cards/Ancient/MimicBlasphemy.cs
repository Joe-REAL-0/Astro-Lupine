using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Cards.Ancient
{
    public class MimicBlasphemy : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_BLASPHEMY";

        public override System.Collections.Generic.IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        public MimicBlasphemy()
            : base(3, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy)
        {
        }

        protected override DynamicVar[] CanonicalVars => new DynamicVar[]
        {
            new DamageVar(48m, ValueProp.Move)
        };

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            int damageAmount = (int)this.DynamicVars.Damage.BaseValue;

            if (cardPlay.Target != null)
            {
                await DamageCmd.Attack(damageAmount)
                    .FromCard(this)
                    .Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_heavy")
                    .Execute(choiceContext);
            }

            await WriteAttackRegister(damageAmount, choiceContext);

            await PowerCmd.Apply<MimicryBlasphemyDeathPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(12m);
        }
    }
}

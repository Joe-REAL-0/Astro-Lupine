using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Common
{
    public class WideAreaCoverage : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_WideAreaCoverage";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(0m, ValueProp.Move),
            new MagicVar(1m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] {AstroLupineKeywords.Read, AstroLupineKeywords.TrojanHorseVirus };

        public WideAreaCoverage()
            : base(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Attack", Owner.Character.AttackAnimDelay);
            
            await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                .FromCard(this)
                .TargetingAllOpponents(CombatState)
                .WithAttackerAnim("Cast", 0.5f)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            if (Owner?.Creature?.CombatState != null)
            {
                foreach (Creature enemy in Owner.Creature.CombatState.HittableEnemies)
                {
                    if (enemy.IsAlive)
                    {
                        await PowerCmd.Apply<TrojanHorseVirusPower>(choiceContext, enemy, (int)this.DynamicVars["Magic"].BaseValue, Owner.Creature, this);
                    }
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

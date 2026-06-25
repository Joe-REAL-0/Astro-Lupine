using System.Collections.Generic;
using System.Threading.Tasks;
using AstroLupine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Cards.Common
{
    public class ComputeOverload : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_ComputeOverload";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DamageVar(10m, ValueProp.Move),
            new CardsVar(1)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write, AstroLupineKeywords.DrawOverwrite};

        public ComputeOverload()
            : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                // Deal damage
                if (cardPlay.Target != null)
                {
                    await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                        .FromCard(this)
                        .Targeting(cardPlay.Target)
                        .WithHitFx("vfx/vfx_attack_blunt")
                        .Execute(choiceContext);
                }

                await PowerCmd.Apply<DrawOverwritePower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(3m);
        }
    }
}

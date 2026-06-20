using System.Collections.Generic;
using System.Threading.Tasks;
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
            new DamageVar(12m, ValueProp.Move),
            new MagicVar(1m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

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
                        .WithHitFx("vfx/vfx_attack_heavy")
                        .Execute(choiceContext);
                }

                // Write Attack Register
                WriteAttackRegister((int)this.DynamicVars.Damage.BaseValue);

                // Draw 1 card
                int drawAmount = (int)this.DynamicVars["Magic"].PreviewValue;
                await CardPileCmd.Draw(choiceContext, drawAmount, Owner);

                // Write Draw Register to 1
                WriteDrawRegister(1);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(2m);
        }
    }
}

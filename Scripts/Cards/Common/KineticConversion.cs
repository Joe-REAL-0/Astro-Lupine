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
    public class KineticConversion : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_KineticConversion";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new BlockVar(12m, ValueProp.Move),
            new DamageVar(5m, ValueProp.Move)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        public KineticConversion()
            : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                // Gain 12 Block
                await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block.PreviewValue, ValueProp.Move, cardPlay);
                
                // Write Defense Register to block amount
                await WriteDefenseRegister((int)this.DynamicVars.Block.PreviewValue);

                // Deal 5 damage
                if (cardPlay.Target != null)
                {
                    await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                        .FromCard(this)
                        .Targeting(cardPlay.Target)
                        .WithHitFx("vfx/vfx_attack_blunt")
                        .Execute(choiceContext);
                }

                // Write Attack Register to 3
                await WriteAttackRegister(5);
            }
        }

        protected override void OnUpgrade()
        {
            this.AddKeyword(CardKeyword.Retain);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Creatures;
using AstroLupine.Cards.Ancient;
using BaseLib.Abstracts;

namespace AstroLupine.Cards.Starter
{
    public class AttackIncAstroLupine : BaseAstroLupineCard, ITranscendenceCard
    {
        public const string CardId = "AstroLupine_Card_AttackInc";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DamageVar(8m, ValueProp.Move),
            new MagicVar(2m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.AttackRegister };

        public AttackIncAstroLupine()
            : base(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target != null)
            {
                await DamageCmd.Attack(this.DynamicVars.Damage.BaseValue)
                    .FromCard(this)
                    .Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
            }

            // Increment the attack register by Magic amount
            int amount = (int)this.DynamicVars["Magic"].BaseValue;
            await IncAttackRegister(amount);
            
            if (base.Owner?.Creature != null)
            {
                MegaCrit.Sts2.Core.Nodes.Vfx.NPowerUpVfx.CreateNormal(base.Owner.Creature);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }

        public CardModel GetTranscendenceTransformedCard()
        {
            return ModelDb.Card<MimicBlasphemy>();
        }
    }
}

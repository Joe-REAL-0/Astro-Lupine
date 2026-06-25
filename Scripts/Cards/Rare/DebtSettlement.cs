using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Rare
{
    public class DebtSettlement : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_DebtSettlement";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Overwrite };

        public class DebtSettlementDamageVar : DamageVar
        {
            public DebtSettlementDamageVar(decimal damage) : base(damage, ValueProp.Move) { }

            public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
            {
                decimal originalBase = this.BaseValue;
                if (card.Owner?.Creature != null)
                {
                    int total = 0;
                    var atk = card.Owner.Creature.GetPower<AttackOverwritePower>();
                    if (atk != null) total += atk.Amount;
                    var def = card.Owner.Creature.GetPower<DefenseOverwritePower>();
                    if (def != null) total += def.Amount;
                    var draw = card.Owner.Creature.GetPower<DrawOverwritePower>();
                    if (draw != null) total += draw.Amount;
                    
                    int multiplier = card.IsUpgraded ? 2 : 1;
                    this.BaseValue += total * multiplier;
                }
                
                base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);
                
                this.BaseValue = originalBase;
            }
        }

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new DebtSettlementDamageVar(10m),
            new MagicVar(1m)
        };

        public DebtSettlement()
            : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            int total = 0;
            var atk = Owner.Creature.GetPower<AttackOverwritePower>();
            if (atk != null) total += atk.Amount;
            var def = Owner.Creature.GetPower<DefenseOverwritePower>();
            if (def != null) total += def.Amount;
            var draw = Owner.Creature.GetPower<DrawOverwritePower>();
            if (draw != null) total += draw.Amount;

            int multiplier = IsUpgraded ? 2 : 1;
            decimal bonusDamage = total * multiplier;
            
            decimal baseDamage = 10m + bonusDamage;

            await DamageCmd.Attack(baseDamage)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].BaseValue = 2m;
        }
    }
}

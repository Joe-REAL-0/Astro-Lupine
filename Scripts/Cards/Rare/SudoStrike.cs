using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class SudoStrike : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_SudoStrike";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write, AstroLupineKeywords.AttackOverwrite };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new DamageVar(32m, ValueProp.Move),
            new DynamicVar("Magic", 1m)
        };

        public SudoStrike()
            : base(9, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
            this.HasWriteTag = true;
            
        }

        public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
        {
            if (card == this && Owner != null && Owner.Creature != null)
            {
                var atkOverwrite = Owner.Creature.GetPower<AttackOverwritePower>();
                if (atkOverwrite != null && atkOverwrite.Amount > 0)
                {
                    decimal reduction = atkOverwrite.Amount * base.DynamicVars["Magic"].BaseValue;
                    modifiedCost = Math.Max(0, originalCost - reduction);
                    return true;
                }
            }
            return base.TryModifyEnergyCostInCombat(card, originalCost, out modifiedCost);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null) return;

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);

            int writeValue = (int)base.DynamicVars.Damage.BaseValue;
            await WriteAttackRegister(writeValue, choiceContext);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

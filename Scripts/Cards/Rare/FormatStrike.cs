using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class FormatStrike : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_FormatStrike";

        private decimal _extraDamage;

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write, AstroLupineKeywords.TrojanHorseVirus };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DamageVar(8m, ValueProp.Move),
            new DynamicVar("Increase", 2m)
        };

        public FormatStrike()
            : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
            this.HasWriteTag = true;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null) return;

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
            await WriteAttackRegister((int)base.DynamicVars.Damage.BaseValue, choiceContext);

            var trojan = cardPlay.Target.GetPower<TrojanHorseVirusPower>();
            int layersRemoved = 0;
            if (trojan != null)
            {
                layersRemoved = trojan.Amount;
                await PowerCmd.Remove(trojan);
            }

            if (layersRemoved > 0 && Owner != null)
            {
                decimal increasePerLayer = base.DynamicVars["Increase"].BaseValue;
                decimal totalIncrease = layersRemoved * increasePerLayer;
                
                IEnumerable<FormatStrike> allFormatStrikes = Owner.PlayerCombatState.AllCards.OfType<FormatStrike>();
                foreach (FormatStrike strike in allFormatStrikes)
                {
                    strike.DynamicVars.Damage.BaseValue += totalIncrease;
                    strike._extraDamage += totalIncrease;
                }
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["Increase"].UpgradeValueBy(1m);
        }

        protected override void AfterDowngraded()
        {
            base.AfterDowngraded();
            base.DynamicVars.Damage.BaseValue += _extraDamage;
        }
    }
}

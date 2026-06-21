using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class RecklessDamageVar : DamageVar
    {
        private decimal _multiplier;
        public RecklessDamageVar(decimal multiplier) : base(0m, ValueProp.Move)
        {
            _multiplier = multiplier;
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);
            if (card.Owner?.Creature != null)
            {
                int defReg = card.Owner.Creature.GetPower<DefenseRegisterPower>()?.Read() ?? 0;
                this.PreviewValue = defReg * _multiplier;
            }
        }
        
        public void UpgradeMultiplierBy(decimal addend)
        {
            _multiplier += addend;
            this.WasJustUpgraded = true;
        }
        
        public decimal GetMultiplier() => _multiplier;
    }

    public class MimicRecklessImpact : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_RECKLESS_IMPACT";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new RecklessDamageVar(2m),
            new MagicVar(5m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.DefenseRegister, AstroLupineKeywords.DefenseOverwrite };

        public MimicRecklessImpact()
            : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            int defReg = Owner.Creature.GetPower<DefenseRegisterPower>()?.Read() ?? 0;
            decimal multiplier = ((RecklessDamageVar)this.DynamicVars.Damage).GetMultiplier();
            decimal baseDmg = defReg * multiplier;

            await DamageCmd.Attack(baseDmg)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);

            await PowerCmd.Apply<DefenseOverwritePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            ((RecklessDamageVar)this.DynamicVars.Damage).UpgradeMultiplierBy(1m);
            this.DynamicVars["Magic"].UpgradeValueBy(3m);
        }
    }
}



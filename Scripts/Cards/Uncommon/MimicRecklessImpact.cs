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
    public class MimicRecklessImpact : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_RECKLESS_IMPACT";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MimicRecklessImpactDamageVar(),
            new MagicVar(5m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.DefenseOverwrite };

        public MimicRecklessImpact()
            : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            decimal baseDmg = 0m;
            var register = Owner.Creature.GetPower<DefenseRegisterPower>();
            if (register != null)
            {
                baseDmg = register.Read();
            }

            await DamageCmd.Attack(baseDmg)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);

            await PowerCmd.Apply<DefenseOverwritePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(3m);
        }
    }

    public class MimicRecklessImpactDamageVar : DamageVar
    {
        public MimicRecklessImpactDamageVar() : base(0m, 0)
        {
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            decimal num = 0m;
            if (card.Owner?.Creature != null)
            {
                var register = card.Owner.Creature.GetPower<DefenseRegisterPower>();
                if (register != null)
                {
                    num = register.Read();
                }
            }

            var enchantment = card.Enchantment;
            if (enchantment != null)
            {
                num += enchantment.EnchantDamageAdditive(num, this.Props);
                num *= enchantment.EnchantDamageMultiplicative(num, this.Props);
                if (!card.IsEnchantmentPreview)
                {
                    this.EnchantedValue = num;
                }
            }

            if (runGlobalHooks && card.Owner != null)
            {
                num = MegaCrit.Sts2.Core.Hooks.Hook.ModifyDamage(
                    card.Owner.RunState, 
                    card.CombatState ?? card.Owner.Creature.CombatState, 
                    target, 
                    card.Owner.Creature, 
                    num, 
                    this.Props, 
                    card, 
                    MegaCrit.Sts2.Core.Hooks.ModifyDamageHookType.All, 
                    previewMode, 
                    out _);
            }
            
            this.PreviewValue = System.Math.Max(num, 0m);
        }
    }
}

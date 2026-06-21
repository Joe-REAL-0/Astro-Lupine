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
            : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            await DamageCmd.Attack(this.DynamicVars.Damage.IntValue)
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
            base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);
            if (card.Owner?.Creature != null)
            {
                var register = card.Owner.Creature.GetPower<DefenseRegisterPower>();
                if (register != null)
                {
                    this.PreviewValue += register.Read();
                }
            }
        }
    }
}

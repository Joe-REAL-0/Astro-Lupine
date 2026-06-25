using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class Rollback : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-ROLLBACK";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new RollbackDamageVar()
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Overwrite };

        public Rollback()
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

            if (Owner?.Creature != null)
            {
                var atkOw = Owner.Creature.GetPower<AttackOverwritePower>();
                if (atkOw != null) await PowerCmd.Remove(atkOw);

                var defOw = Owner.Creature.GetPower<DefenseOverwritePower>();
                if (defOw != null) await PowerCmd.Remove(defOw);

                var drawOw = Owner.Creature.GetPower<DrawOverwritePower>();
                if (drawOw != null) await PowerCmd.Remove(drawOw);
            }
        }

        protected override void OnUpgrade()
        {
            this.AddKeyword(CardKeyword.Retain);
        }
    }

    public class RollbackDamageVar : DamageVar
    {
        public RollbackDamageVar() : base(0m, 0)
        {
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);
            if (card.Owner?.Creature != null)
            {
                int stacks = 0;
                var atkOw = card.Owner.Creature.GetPower<AttackOverwritePower>();
                if (atkOw != null) stacks += atkOw.Amount;

                var defOw = card.Owner.Creature.GetPower<DefenseOverwritePower>();
                if (defOw != null) stacks += defOw.Amount;

                var drawOw = card.Owner.Creature.GetPower<DrawOverwritePower>();
                if (drawOw != null) stacks += drawOw.Amount;

                this.PreviewValue += stacks;
            }
        }
    }
}

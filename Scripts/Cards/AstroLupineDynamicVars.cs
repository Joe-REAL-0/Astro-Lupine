using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards
{
    public class AstroReadDamageVar : DamageVar
    {
        public AstroReadDamageVar(decimal damage, ValueProp props = ValueProp.Move) : base(damage, props)
        {
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            // Calculate base damage including normal modifiers
            base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);
            
            // Apply register buff
            if (card.Owner?.Creature != null)
            {
                AttackRegisterPower? register = card.Owner.Creature.GetPower<AttackRegisterPower>();
                if (register != null)
                {
                    this.PreviewValue += register.Read();
                }
            }
        }
    }

    public class AstroReadBlockVar : BlockVar
    {
        public AstroReadBlockVar(decimal block, ValueProp props = ValueProp.Move) : base(block, props)
        {
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            // Calculate base block including normal modifiers
            base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);

            // Apply register buff
            if (card.Owner?.Creature != null)
            {
                DefenseRegisterPower? register = card.Owner.Creature.GetPower<DefenseRegisterPower>();
                if (register != null)
                {
                    this.PreviewValue += register.Read();
                }
            }
        }
    }

    public class MagicVar : IntVar
    {
        public MagicVar(decimal magic) : base("Magic", magic)
        {
        }
    }

    public class AstroReadMagicVar : MagicVar
    {
        public AstroReadMagicVar(decimal magic, ValueProp props = ValueProp.Move) : base(magic)
        {
        }

        public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks)
        {
            // Calculate base magic including normal modifiers
            base.UpdateCardPreview(card, previewMode, target, runGlobalHooks);

            // Apply register buff
            if (card.Owner?.Creature != null)
            {
                DrawRegisterPower? register = card.Owner.Creature.GetPower<DrawRegisterPower>();
                if (register != null)
                {
                    this.PreviewValue += register.Read();
                }
            }
        }
    }
}

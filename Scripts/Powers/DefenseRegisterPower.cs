using BaseLib.Abstracts;

using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Combat;
using AstroLupine.Cards;
using System.Linq;

namespace AstroLupine.Powers
{
    public class DefenseRegisterPower : BaseRegisterPower
    {
        public const string PowerId = "AstroLupine_DefenseRegister";

        // 临时占位：使用敏捷(Dexterity)图标
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/defense_register.png";

        public DefenseRegisterPower() : base(5)
        {
        }

        public override decimal ModifyBlockAdditive(Creature target, decimal block, ValueProp props, CardModel? cardSource, CardPlay? cardPlay)
        {
            if (cardSource != null && cardSource.Keywords.Contains(AstroLupineKeywords.Read))
            {
                decimal multiplier = 1m;
                if (Owner?.CombatState != null)
                {
                    foreach (AbstractModel item in Owner.CombatState.IterateHookListeners())
                    {
                        multiplier *= item.ModifyBlockMultiplicative(target, 1m, props, cardSource, cardPlay);
                    }
                }

                if (multiplier > 0)
                {
                    return Read() / multiplier;
                }
            }
            return 0m;
        }
    }
}

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
    public class AttackRegisterPower : BaseRegisterPower
    {
        public const string PowerId = "AstroLupine_AttackRegister";

        // 临时占位：使用力量(Strength)图标
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/attack_register.png";

        public AttackRegisterPower() : base(6)
        {
        }

        public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (cardSource != null && cardSource.Keywords.Contains(AstroLupineKeywords.Read))
            {
                decimal multiplier = 1m;
                var runState = Owner?.Player?.RunState ?? Owner?.CombatState?.RunState;
                if (runState != null)
                {
                    foreach (AbstractModel item in runState.IterateHookListeners(Owner?.CombatState))
                    {
                        multiplier *= item.ModifyDamageMultiplicative(target, 1m, props, dealer, cardSource);
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

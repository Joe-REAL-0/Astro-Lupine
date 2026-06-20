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
        public override string? CustomPackedIconPath => "res://assets/texture/power/attack_register.png";

        public AttackRegisterPower() : base(6)
        {
        }

        public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (cardSource != null && cardSource.Keywords.Contains(AstroLupineKeywords.Read))
            {
                // Native cards handle their own read in DealReadDamage. We only hook for dynamically added Read.
                bool isNativeRead = (cardSource is BaseAstroLupineCard astroCard) && astroCard.CanonicalKeywords.Contains(AstroLupineKeywords.Read);
                if (!isNativeRead)
                {
                    decimal multiplier = 1m;
                    var runState = Owner?.Player?.RunState ?? Owner?.CombatState?.RunState;
                    if (runState != null)
                    {
                        multiplier = Hook.ModifyDamage(
                            runState, Owner?.CombatState, target, dealer, 1m, props, cardSource,
                            ModifyDamageHookType.Multiplicative, CardPreviewMode.None, out _
                        );
                    }

                    if (multiplier > 0)
                    {
                        return Read() / multiplier;
                    }
                }
            }
            return 0m;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Powers
{
    public class AttackAccumulatorPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_AttackAccumulator";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/attack_accumulator.png";

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(base.Owner))
            {
                Flash();
                var attackRegister = base.Owner.GetPower<AttackRegisterPower>();
                if (attackRegister != null)
                {
                    // Convert to correct runtime type to access Increment
                    var mutablePower = attackRegister as AttackRegisterPower;
                    if (mutablePower != null) await mutablePower.Increment(base.Amount);
                }
            }
        }
    }
}

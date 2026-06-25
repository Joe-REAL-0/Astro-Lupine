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
    public class DefenseAccumulatorPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_DefenseAccumulator";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/defense_accumulator.png";
        public override string? CustomBigIconPath => CustomPackedIconPath;

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(base.Owner))
            {
                Flash();
                var defenseRegister = base.Owner.GetPower<DefenseRegisterPower>();
                if (defenseRegister != null)
                {
                    var mutablePower = defenseRegister as DefenseRegisterPower;
                    if (mutablePower != null) await mutablePower.Increment(base.Amount);
                }
            }
        }
    }
}

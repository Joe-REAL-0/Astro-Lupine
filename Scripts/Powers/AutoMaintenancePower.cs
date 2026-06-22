using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Powers
{
    public class AutoMaintenancePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_AutoMaintenance";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter; // or none
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/auto_maintenance.png";

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(base.Owner))
            {
                int blockAmount = base.Owner.GetPowerAmount<DefenseRegisterPower>();
                if (blockAmount > 0)
                {
                    Flash();
                    await CreatureCmd.GainBlock(base.Owner, blockAmount, ValueProp.Unpowered, null);
                }
                
                await PowerCmd.TickDownDuration(this);
            }
        }
    }
}

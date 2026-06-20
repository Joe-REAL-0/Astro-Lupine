using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Powers
{
    public class DefenseOverwritePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_DefenseOverwrite";
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/defense_overwrite.png";

        public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            if (this.Owner != null)
            {
                var openSource = this.Owner.GetPower<OpenSourceProtocolPower>();
                if (openSource != null)
                {
                    await openSource.TriggerProtocol();
                }
            }
        }

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(base.Owner))
            {
                Flash();
                var defenseRegister = base.Owner.GetPower<DefenseRegisterPower>();
                if (defenseRegister != null)
                {
                    var mutablePower = defenseRegister as DefenseRegisterPower;
                    mutablePower?.Write(base.Amount);
                }
                
                await PowerCmd.Remove(this);
            }
        }
    }
}

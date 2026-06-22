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
    public class AttackOverwritePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_AttackOverwrite";
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/attack_overwrite.png";

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
                var attackRegister = base.Owner.GetPower<AttackRegisterPower>();
                if (attackRegister != null)
                {
                    var mutablePower = attackRegister as AttackRegisterPower;
                    mutablePower?.Write(base.Amount);
                }
                
                await PowerCmd.Remove(this);
            }
        }
    }
}

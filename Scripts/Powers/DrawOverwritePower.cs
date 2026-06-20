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
    public class DrawOverwritePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_DrawOverwrite";
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/draw_overwrite.png";

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
                var drawRegister = base.Owner.GetPower<DrawRegisterPower>();
                if (drawRegister != null)
                {
                    var mutablePower = drawRegister as DrawRegisterPower;
                    mutablePower?.Write(base.Amount);
                }
                
                await PowerCmd.Remove(this);
            }
        }
    }
}

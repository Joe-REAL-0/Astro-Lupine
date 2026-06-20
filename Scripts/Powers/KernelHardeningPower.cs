using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace AstroLupine.Powers
{
    public class KernelHardeningPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_KernelHardening";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/kernel_hardening.png";

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Player && participants.Contains(base.Owner))
            {
                await PowerCmd.Decrement(this);
            }
        }
    }
}

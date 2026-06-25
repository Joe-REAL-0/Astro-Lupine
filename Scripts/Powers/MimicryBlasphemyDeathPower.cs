using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace AstroLupine.Powers
{
    public class MimicryBlasphemyDeathPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_MimicryBlasphemyDeath";
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.Single;
        
        // Since we don't have a specific icon, we can use a generic one or none.
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/mimicry_blasphemy.png";
        public override string? CustomBigIconPath => CustomPackedIconPath;

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (side == CombatSide.Player && participants.Contains(this.Owner))
            {
                Flash();
                await CreatureCmd.Kill(this.Owner);
            }
        }
    }
}

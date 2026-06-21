using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class CoolingArchitecturePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_CoolingArchitecture";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/cooling_architecture.png";

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(this.Owner))
            {
                var atkPower = this.Owner.GetPower<AttackRegisterPower>();
                if (atkPower != null && atkPower.Amount >= this.Amount)
                {
                    Flash();
                    await CardPileCmd.Draw(new MegaCrit.Sts2.Core.GameActions.Multiplayer.ThrowingPlayerChoiceContext(), 2, this.Owner.Player);
                }
            }
        }
    }
}

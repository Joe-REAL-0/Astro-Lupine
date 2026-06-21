using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class ExceptionHandlingPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_ExceptionHandling";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/exception_handling.png";

        public async Task OnRegisterChanged(PlayerChoiceContext? choiceContext)
        {
            Flash();
            var combatState = this.Owner?.CombatState;
            if (combatState != null && this.Owner != null)
            {
                var context = choiceContext ?? new ThrowingPlayerChoiceContext();
                await CreatureCmd.Damage(
                    context,
                    combatState.HittableEnemies,
                    this.Amount,
                    ValueProp.Unpowered,
                    this.Owner,
                    null);
            }
        }
    }
}

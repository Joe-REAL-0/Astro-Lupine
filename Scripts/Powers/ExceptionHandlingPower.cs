using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using BaseLib.Abstracts;

using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Powers
{
    public class ExceptionHandlingPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_ExceptionHandling";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;

        public async Task OnRegisterChanged(PlayerChoiceContext? choiceContext)
        {
            Flash();
            var combatState = this.Owner?.CombatState;
            if (combatState != null && this.Owner != null)
            {
                await CreatureCmd.Damage(
                    choiceContext ?? new MegaCrit.Sts2.Core.GameActions.Multiplayer.ThrowingPlayerChoiceContext(),
                    combatState.HittableEnemies,
                    this.Amount,
                    ValueProp.Unpowered,
                    this.Owner,
                    null);
            }
        }
    }
}

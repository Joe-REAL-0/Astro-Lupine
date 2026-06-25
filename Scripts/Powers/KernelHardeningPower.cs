using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class KernelHardeningPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_KernelHardening";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/kernel_hardening.png";
        public override string? CustomBigIconPath => CustomPackedIconPath;

        public override async Task AfterPowerAmountChanged(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (power == this && this.Owner != null)
            {
                var atkPower = this.Owner.GetPower<AttackRegisterPower>();
                if (atkPower != null && atkPower.Amount < this.Amount)
                {
                    await atkPower.Write(this.Amount);
                }

                var defPower = this.Owner.GetPower<DefenseRegisterPower>();
                if (defPower != null && defPower.Amount < this.Amount)
                {
                    await defPower.Write(this.Amount);
                }
            }
        }
    }
}

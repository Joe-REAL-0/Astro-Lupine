using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class ReadOnlyLockPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_ReadOnlyLock";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/read_only_lock.png";

        public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            if (this.Owner != null)
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

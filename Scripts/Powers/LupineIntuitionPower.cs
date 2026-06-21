using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class LupineIntuitionPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_LupineIntuition";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/lupine_intuition.png";

        public async Task TriggerIntuition()
        {
            Flash();
            await CreatureCmd.GainBlock(this.Owner, this.Amount, MegaCrit.Sts2.Core.ValueProps.ValueProp.Unpowered, null, false);
        }
    }
}

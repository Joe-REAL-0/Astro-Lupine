using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace AstroLupine.Powers
{
    public class RootPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_Root";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
    }
}
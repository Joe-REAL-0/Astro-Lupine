using System.Collections.Generic;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class ProliferateAlgorithmPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_ProliferateAlgorithm";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
    }
}

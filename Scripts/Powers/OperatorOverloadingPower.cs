using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using BaseLib.Abstracts;
using AstroLupine.Cards;

namespace AstroLupine.Powers
{
    public class OperatorOverloadingPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_OperatorOverloading";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/operator_overloading.png";
        public override string? CustomBigIconPath => CustomPackedIconPath;

        public async Task TriggerProtocol(PlayerChoiceContext? choiceContext = null)
        {
            this.Flash();
            await PowerCmd.Apply<MegaCrit.Sts2.Core.Models.Powers.VigorPower>(choiceContext, this.Owner, this.Amount, this.Owner, null);
        }
    }
}

using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace AstroLupine.Powers
{
    public class HyperThreadingFormPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_HyperThreadingForm";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/hayper_threading_form.png";

        public bool HasTriggeredThisTurn = false;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player.Creature == this.Owner)
            {
                HasTriggeredThisTurn = false;
            }
        }
    }
}
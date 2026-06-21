using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace AstroLupine.Powers
{
    public class PrivilegeEscalationPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_PrivilegeEscalation";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/privilege_escalatio.png";

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Type == CardType.Attack && cardPlay.Card.Owner == base.Owner.Player)
            {
                Flash();
                await PowerCmd.Decrement(this);
            }
        }
    }
}


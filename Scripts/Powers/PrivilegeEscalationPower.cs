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

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Type == CardType.Attack && cardPlay.Card.Owner == base.Owner.Player)
            {
                Flash();
                int damage = cardPlay.Card.DynamicVars.Damage.IntValue;
                base.Owner.GetPower<AttackRegisterPower>()?.Write(damage);
                
                base.Amount--;
                if (base.Amount <= 0)
                {
                    await PowerCmd.Remove(this);
                }
            }
        }
        
        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Player && participants.Contains(base.Owner))
            {
                await PowerCmd.Remove(this);
            }
        }
    }
}


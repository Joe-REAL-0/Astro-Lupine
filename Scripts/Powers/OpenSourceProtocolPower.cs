using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Powers
{
    public class OpenSourceProtocolPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_OpenSourceProtocol";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public async Task TriggerProtocol(PlayerChoiceContext? choiceContext = null)
        {
            this.Flash();
            var atk = this.Owner?.GetPower<AttackRegisterPower>();
            var def = this.Owner?.GetPower<DefenseRegisterPower>();
            
            if (atk != null) await atk.Increment(this.Amount, choiceContext);
            if (def != null) await def.Increment(this.Amount, choiceContext);
        }
    }
}

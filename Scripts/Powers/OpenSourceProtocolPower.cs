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
        public override PowerStackType StackType => PowerStackType.Single;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/open_source_protocol.png";
        public override string? CustomBigIconPath => CustomPackedIconPath;
        
        public async Task TriggerProtocol(string registerType, int overwriteAmount, PlayerChoiceContext? choiceContext = null)
        {
            this.Flash();
            if (registerType == "Attack")
            {
                var atk = this.Owner?.GetPower<AttackRegisterPower>();
                if (atk != null) await atk.Increment(overwriteAmount, choiceContext);
            }
            else if (registerType == "Defense")
            {
                var def = this.Owner?.GetPower<DefenseRegisterPower>();
                if (def != null) await def.Increment(overwriteAmount, choiceContext);
            }
            else if (registerType == "Draw")
            {
                var draw = this.Owner?.GetPower<DrawRegisterPower>();
                if (draw != null) await draw.Increment(overwriteAmount, choiceContext);
            }
        }
    }
}

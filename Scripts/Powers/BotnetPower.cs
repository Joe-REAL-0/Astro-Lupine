using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using BaseLib.Abstracts;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Combat;

namespace AstroLupine.Powers
{
    public class BotnetPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_Botnet";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/botnet.png";

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(this.Owner))
            {
                int totalVirus = 0;
                foreach (var enemy in combatState.HittableEnemies)
                {
                    var virus = enemy.GetPower<TrojanHorseVirusPower>();
                    if (virus != null)
                    {
                        totalVirus += virus.Amount;
                    }
                }

                if (totalVirus > 0)
                {
                    Flash();
                    int blockAmount = totalVirus * this.Amount;
                    await CreatureCmd.GainBlock(base.Owner, blockAmount, MegaCrit.Sts2.Core.ValueProps.ValueProp.Unpowered, null, false);
                }
            }
        }
    }
}

using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class CronJobExtraTurnPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_Power_CronJobExtraTurn";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/cron_job.png";

        public override bool ShouldTakeExtraTurn(Player player)
        {
            if (player.Creature == Owner)
            {
                return true;
            }
            return base.ShouldTakeExtraTurn(player);
        }

        public override async Task AfterTakingExtraTurn(Player player)
        {
            if (player.Creature == Owner)
            {
                await PowerCmd.Remove(this);
            }
        }
    }
}

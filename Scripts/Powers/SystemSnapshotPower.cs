using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace AstroLupine.Powers
{
    public class SystemSnapshotPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_SystemSnapshot";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public int SnapshotAtk { get; set; }
        public int SnapshotDef { get; set; }
        public int SnapshotDrw { get; set; }

        public override Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (target != null)
            {
                SnapshotAtk = target.GetPower<AttackRegisterPower>()?.Amount ?? 1;
                SnapshotDef = target.GetPower<DefenseRegisterPower>()?.Amount ?? 1;
                SnapshotDrw = target.GetPower<DrawRegisterPower>()?.Amount ?? 1;
            }
            return Task.CompletedTask;
        }

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player.Creature == this.Owner)
            {
                var atk = player.Creature.GetPower<AttackRegisterPower>() as AttackRegisterPower;
                if (atk != null) await atk.Write(SnapshotAtk);

                var def = player.Creature.GetPower<DefenseRegisterPower>() as DefenseRegisterPower;
                if (def != null) await def.Write(SnapshotDef);

                var drw = player.Creature.GetPower<DrawRegisterPower>() as DrawRegisterPower;
                if (drw != null) await drw.Write(SnapshotDrw);

                await PowerCmd.Remove(this);
            }
        }
    }
}
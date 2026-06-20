import os

powers_dir = r"c:\Users\Joe\Documents\Astro Lupine\Scripts\Powers"

root_power = """using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Powers
{
    public class RootPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_Root";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
    }
}"""
with open(os.path.join(powers_dir, "RootPower.cs"), 'w', encoding='utf-8') as f:
    f.write(root_power)

hyper_power = """using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Models;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Combat;

namespace AstroLupine.Powers
{
    public class HyperThreadingFormPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_HyperThreadingForm";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;

        public bool HasTriggeredThisTurn = false;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player.Creature == this.Owner)
            {
                HasTriggeredThisTurn = false;
            }
        }
    }
}"""
with open(os.path.join(powers_dir, "HyperThreadingFormPower.cs"), 'w', encoding='utf-8') as f:
    f.write(hyper_power)

sys_snapshot_power = """using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Combat;

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
}"""
with open(os.path.join(powers_dir, "SystemSnapshotPower.cs"), 'w', encoding='utf-8') as f:
    f.write(sys_snapshot_power)

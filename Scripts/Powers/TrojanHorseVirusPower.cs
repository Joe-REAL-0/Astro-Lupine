using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Powers
{
    public class TrojanHorseVirusPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_TrojanHorseVirus";
        public override PowerType Type => PowerType.Debuff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/torjan_horse_virus.png";

        public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
        {
            var intuition = applier?.GetPower<LupineIntuitionPower>();
            if (intuition == null && this.Owner?.CombatState?.PlayerCreatures != null && this.Owner.CombatState.PlayerCreatures.Count > 0)
            {
                intuition = this.Owner.CombatState.PlayerCreatures[0].GetPower<LupineIntuitionPower>();
            }
            
            if (intuition != null)
            {
                await intuition.TriggerIntuition();
            }
        }
        
        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (side == CombatSide.Player)
            {
                var player = combatState.Players.FirstOrDefault();
                if (player != null && player.Creature != null)
                {
                    int dmg = player.Creature.GetPowerAmount<AttackRegisterPower>();
                    if (dmg > 0)
                    {
                        Flash();
                        await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, dmg, ValueProp.Unpowered, null, null);
                    }
                }
            }
        }

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Enemy && participants.Contains(base.Owner))
            {
                await PowerCmd.Decrement(this);
            }
        }
    }
}

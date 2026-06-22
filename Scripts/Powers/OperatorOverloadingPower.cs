using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using BaseLib.Abstracts;
using AstroLupine.Cards;

namespace AstroLupine.Powers
{
    public class OperatorOverloadingPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_OperatorOverloading";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/operator_overloading.png";

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner != null && cardPlay.Card.Owner.Creature == this.Owner)
            {
                bool hasWrite = false;
                if (cardPlay.Card is BaseAstroLupineCard astroCard && astroCard.HasWriteTag) hasWrite = true;
                if (cardPlay.Card.Keywords.Contains(AstroLupineKeywords.Write)) hasWrite = true;

                if (hasWrite)
                {
                    Flash();
                    var combatState = this.Owner?.CombatState;
                    if (combatState != null && this.Owner != null)
                    {
                        await CreatureCmd.Damage(
                            choiceContext,
                            combatState.HittableEnemies,
                            this.Amount,
                            ValueProp.Unpowered,
                            this.Owner,
                            null);
                    }
                }
            }
        }
    }
}

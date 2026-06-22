using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class CoolingArchitecturePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_CoolingArchitecture";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/cooling_architecture.png";

        protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => new MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar[] { new AstroLupine.Cards.MagicVar(12) };

        public override Task AfterApplied(Creature? applier, MegaCrit.Sts2.Core.Models.CardModel? cardSource)
        {
            if (cardSource != null && cardSource.DynamicVars.TryGetValue("Magic", out var magicVar))
            {
                int newThreshold = magicVar.IntValue;
                if (this.DynamicVars["Magic"].BaseValue == 12m || newThreshold < this.DynamicVars["Magic"].BaseValue)
                {
                    this.DynamicVars["Magic"].BaseValue = newThreshold;
                }
            }
            return Task.CompletedTask;
        }

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(this.Owner))
            {
                var atkPower = this.Owner.GetPower<AttackRegisterPower>();
                if (atkPower != null && atkPower.Amount >= this.DynamicVars["Magic"].IntValue)
                {
                    Flash();
                    await CardPileCmd.Draw(new MegaCrit.Sts2.Core.GameActions.Multiplayer.ThrowingPlayerChoiceContext(), this.Amount, this.Owner.Player);
                }
            }
        }
    }
}

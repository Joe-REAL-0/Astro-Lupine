using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Hooks;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class FeedbackSystem : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-FEEDBACK_SYSTEM";

        public FeedbackSystem()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner == null) return;

            var defenseOverwrite = Owner.Creature.GetPower<DefenseOverwritePower>();
            int overwriteAmount = defenseOverwrite?.Amount ?? 0;
            
            if (overwriteAmount > 0)
            {
                int blockAmount = overwriteAmount;
                if (this.IsUpgraded)
                {
                    blockAmount *= 2;
                }
                
                await CreatureCmd.GainBlock(Owner.Creature, blockAmount, ValueProp.Move, cardPlay);
            }
        }
    }
}

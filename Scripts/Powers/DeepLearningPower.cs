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
using AstroLupine.Cards;

namespace AstroLupine.Powers
{
    public class DeepLearningPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_DeepLearning";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://AstroLupine/assets/texture/power/deep_learning.png";
        public override string? CustomBigIconPath => CustomPackedIconPath;
        
        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner?.Creature == this.Owner && (cardPlay.Card.Keywords.Contains(AstroLupineKeywords.Write) || cardPlay.Card.Keywords.Contains(AstroLupineKeywords.Read)))
            {
                var enemies = this.CombatState.GetCreaturesOnSide(MegaCrit.Sts2.Core.Combat.CombatSide.Enemy).Where(c => !c.IsDead).ToList();
                if (enemies.Count > 0)
                {
                    // For randomness we use System.Random if RunRng isn't easily accessible, but normally we use RunRng.
                    // For now, let's just use System.Random for safety or take the first.
                    var rand = new System.Random();
                    var target = enemies[rand.Next(enemies.Count)];
                    
                    Flash();
                    await CreatureCmd.Damage(choiceContext, target, this.Amount, MegaCrit.Sts2.Core.ValueProps.ValueProp.Unpowered, this.Owner, null);
                }
            }
        }
    }
}

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
    public class TuringCompletePower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_TuringComplete";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        
        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner?.Creature == this.Owner)
            {
                bool hasRead = cardPlay.Card.Keywords.Contains(AstroLupineKeywords.Read);
                bool hasWrite = cardPlay.Card.Keywords.Contains(AstroLupineKeywords.Write);
                
                if (hasRead && hasWrite)
                {
                    this.Flash();
                    var player = this.CombatState.Players.FirstOrDefault(p => p.Creature == this.Owner);
                    if (player != null) await CardPileCmd.Draw(choiceContext, this.Amount, player);
                }
            }
        }

    }
}

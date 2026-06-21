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
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.CardSelection;
using AstroLupine.Cards;

namespace AstroLupine.Powers
{
    public class KnowledgeGraphPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_KnowledgeGraph";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;
        
        public override string? CustomPackedIconPath => "res://assets/texture/power/knowledge_graph.png";
        
        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player.Creature == base.Owner)
            {
                for (int i = 0; i < this.Amount; i++)
                    {
                        var pile = PileType.Draw.GetPile(player);
                        if (pile.Cards.Count > 0)
                        {
                            var cards = await CardSelectCmd.FromCombatPile(choiceContext, pile, player, new CardSelectorPrefs(new LocString("gameplay_ui", "ASTROLUPINE-CHOOSE_CARD_TO_HAND_AND_WRITE"), 1, 1));
                            
                            foreach (var card in cards)
                            {
                                await CardPileCmd.Add(card, PileType.Hand);
                                CardCmd.ApplyKeyword(card, AstroLupineKeywords.Write);
                            }
                        }
                    }
            }
        }
    }
}

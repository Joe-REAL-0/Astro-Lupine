using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class MimicRenewSpirit : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_RENEW_SPIRIT";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new BlockVar(5m, ValueProp.Move)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        public MimicRenewSpirit()
            : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                int count = 0;
                CardPile pile = PileType.Hand.GetPile(Owner);
                List<CardModel> handCards = pile.Cards.ToList();
                foreach (CardModel card in handCards)
                {
                    if (card != this)
                    {
                        await CardCmd.Exhaust(choiceContext, card);
                        await CreatureCmd.GainBlock(Owner.Creature, this.DynamicVars.Block, cardPlay);
                        count++;
                    }
                }
                if (count > 0)
                {
                    await WriteDefenseRegister(this.DynamicVars.Block.IntValue);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Block.UpgradeValueBy(2m);
        }
    }
}

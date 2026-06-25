using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Rare
{
    public class GarbageCollection : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_GarbageCollection";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new AstroReadDamageVar(2m, ValueProp.Move)
        };

        public GarbageCollection()
            : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            var discardPile = PileType.Discard.GetPile(Owner);
            int exhaustedCount = discardPile.Cards.Count;
            
            if (exhaustedCount > 0)
            {
                var cardsToExhaust = discardPile.Cards.ToList();
                foreach (var card in cardsToExhaust)
                {
                    await CardCmd.Exhaust(choiceContext, card);
                }

                for (int i = 0; i < exhaustedCount; i++)
                {
                    await DealReadDamage(choiceContext, cardPlay, base.DynamicVars.Damage);
                }
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(1m);
        }
    }
}

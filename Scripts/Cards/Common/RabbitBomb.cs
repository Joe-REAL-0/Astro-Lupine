using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class RabbitBomb : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_RabbitBomb";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(0m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public RabbitBomb()
            : base(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_slash");

            // Add a copy to the discard pile
            if (Owner != null)
            {
                CardModel copy = this.CreateClone();
                await CardPileCmd.AddGeneratedCardToCombat(copy, PileType.Discard, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(2m);
        }
    }
}

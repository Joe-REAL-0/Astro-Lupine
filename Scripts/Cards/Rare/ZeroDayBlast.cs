using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class ZeroDayBlast : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_ZeroDayBlast";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.ZeroDayExploit };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DamageVar(8m, ValueProp.Move),
            new DynamicVar("Magic", 3m)
        };

        public ZeroDayBlast()
            : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null) return;

            if (cardPlay.Target.Block > 0)
            {
                await CreatureCmd.LoseBlock(cardPlay.Target, cardPlay.Target.Block);
            }

            await DealReadDamage(choiceContext, cardPlay, base.DynamicVars.Damage);

            var zeroDay = new ZeroDayExploitPower();
            await PowerCmd.Apply<ZeroDayExploitPower>(choiceContext, cardPlay.Target, (int)base.DynamicVars["Magic"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(4m);
            base.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

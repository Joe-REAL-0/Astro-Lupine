using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class Deadlock : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-DEADLOCK";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new DamageVar(0m, ValueProp.Move),
            new MagicVar(2m)
        };

        public Deadlock()
            : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            int times = (int)this.DynamicVars["Magic"].BaseValue;
            for (int i = 0; i < times; i++)
            {
                int zeroDayStacks = cardPlay.Target.GetPower<ZeroDayExploitPower>()?.Amount ?? 0;
                decimal bonus = zeroDayStacks * 2;

                var tempDamageVar = new DamageVar(this.DynamicVars.Damage.BaseValue + bonus, ValueProp.Move);
                await DealReadDamage(choiceContext, cardPlay, tempDamageVar, "vfx/vfx_attack_blunt");
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

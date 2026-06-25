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

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.TrojanHorseVirus };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new DamageVar(6m, ValueProp.Move),
            new MagicVar(1m)
        };

        public Deadlock()
            : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            int times = 2;
            for (int i = 0; i < times; i++)
            {
                int trojanStacks = cardPlay.Target.GetPower<TrojanHorseVirusPower>()?.Amount ?? 0;
                decimal bonus = trojanStacks * this.DynamicVars["Magic"].IntValue;

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

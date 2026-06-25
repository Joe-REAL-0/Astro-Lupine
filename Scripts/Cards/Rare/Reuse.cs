using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class Reuse : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-REUSE";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.ZeroDayExploit, AstroLupineKeywords.TrojanHorseVirus };

        public Reuse()
            : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target != null && Owner?.Creature != null)
            {
                var sysPower = Owner.Creature.GetPower<AstroLupineSystemPower>();
                if (sysPower != null && sysPower.TotalZeroDayAppliedThisCombat > 0)
                {
                    await PowerCmd.Apply<TrojanHorseVirusPower>(choiceContext, cardPlay.Target, sysPower.TotalZeroDayAppliedThisCombat, Owner.Creature, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.UpgradeBy(-1);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class LogicBackdoor : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-LOGIC_BACKDOOR";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(2m),
            new AstroReadCardsVar(0)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.ZeroDayExploit };

        public LogicBackdoor()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target != null && Owner?.Creature != null)
            {
                await PowerCmd.Apply<ZeroDayExploitPower>(choiceContext, cardPlay.Target, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
                await DrawReadCards(choiceContext, this.DynamicVars["Cards"]);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

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
            new DynamicVar("Debuff", 2m),
            new AstroReadMagicVar(0m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public LogicBackdoor()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target != null && Owner?.Creature != null)
            {
                await PowerCmd.Apply<ZeroDayExploitPower>(choiceContext, cardPlay.Target, this.DynamicVars["Debuff"].IntValue, Owner.Creature, this);
                await CardPileCmd.Draw(choiceContext, this.DynamicVars["Magic"].IntValue, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Debuff"].UpgradeValueBy(1m);
        }
    }
}

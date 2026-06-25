using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Common
{
    public class Preload : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-PRELOAD";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.AttackOverwrite };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new CardsVar(2),
            new MagicVar(6m) 
        };

        public Preload()
            : base(0, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                await CardPileCmd.Draw(choiceContext, this.DynamicVars["Cards"].IntValue, Owner);
                if (Owner.Creature != null)
                {
                    await PowerCmd.Apply<AttackOverwritePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(3m);
        }
    }
}

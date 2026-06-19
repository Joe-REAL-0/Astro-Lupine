using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class Preload : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Preload";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.Read };

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(2m) 
        };

        public Preload()
            : base(0, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                await CardPileCmd.Draw(choiceContext, (int)this.DynamicVars["Magic"].PreviewValue, Owner);
                WriteDrawRegister((int)this.DynamicVars["Magic"].PreviewValue);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

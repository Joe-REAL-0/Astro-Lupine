using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
    public class IterativeRetrieval : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_IterativeRetrieval";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadMagicVar(1m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.Write };

        public IterativeRetrieval()
            : base(2, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                int drawAmount = (int)this.DynamicVars["Magic"].PreviewValue;
                await CardPileCmd.Draw(choiceContext, drawAmount, Owner);
                WriteDrawRegister(drawAmount);

                // Reduce cost for the rest of combat
                this.EnergyCost.AddThisCombat(-1);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

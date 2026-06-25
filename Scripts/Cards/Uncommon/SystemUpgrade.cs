using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Uncommon
{
    public class SystemUpgrade : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_SystemUpgrade";


        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(2m) 
        };

        public SystemUpgrade()
            : base(1, CardType.Power, CardRarity.Uncommon, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int amount = (int)this.DynamicVars["Magic"].PreviewValue;
            await IncAttackRegister(amount);
            await IncDefenseRegister(amount);
            await IncDrawRegister(amount);
            
            await Task.CompletedTask;
        }

        protected override void OnUpgrade()
        {
            this.AddKeyword(CardKeyword.Retain);
        }
    }
}

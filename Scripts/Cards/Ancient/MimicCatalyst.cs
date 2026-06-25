using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Ancient
{
    public class MimicCatalyst : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MIMIC_CATALYST";

        public override System.Collections.Generic.IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.TrojanHorseVirus };

        protected override DynamicVar[] CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(2m) 
        };

        public MimicCatalyst()
            : base(1, CardType.Skill, CardRarity.Ancient, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            var virus = cardPlay.Target.GetPower<TrojanHorseVirusPower>();
            if (virus != null && virus.Amount > 0)
            {
                int multiplier = this.IsUpgraded ? 2 : 1;
                int amountToAdd = virus.Amount * multiplier;
                
                await PowerCmd.Apply<TrojanHorseVirusPower>(choiceContext, cardPlay.Target, amountToAdd, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

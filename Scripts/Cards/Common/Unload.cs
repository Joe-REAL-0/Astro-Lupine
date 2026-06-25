using System.Collections.Generic;
using System.Threading.Tasks;
using AstroLupine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AstroLupine.Cards.Common
{
    public class Unload : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Unload";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new EnergyVar(2),
            new MagicVar("Magic1", 5m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.DefenseOverwrite };

        public Unload()
            : base(0, CardType.Skill, CardRarity.Common, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner != null)
            {
                await PlayerCmd.GainEnergy(this.DynamicVars["Energy"].PreviewValue, Owner);
                await PowerCmd.Apply<DefenseOverwritePower>(choiceContext, Owner.Creature, this.DynamicVars["Magic1"].IntValue, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic1"].UpgradeValueBy(3m);
        }
    }
}

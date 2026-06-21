using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class Rollback : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-ROLLBACK";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.AttackOverwrite, AstroLupineKeywords.DefenseOverwrite, AstroLupineKeywords.DrawOverwrite };

        public Rollback()
            : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                var atkOw = Owner.Creature.GetPower<AttackOverwritePower>();
                if (atkOw != null) await PowerCmd.Remove(atkOw);

                var defOw = Owner.Creature.GetPower<DefenseOverwritePower>();
                if (defOw != null) await PowerCmd.Remove(defOw);

                var drawOw = Owner.Creature.GetPower<DrawOverwritePower>();
                if (drawOw != null) await PowerCmd.Remove(drawOw);
            }
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.UpgradeBy(-1);
        }
    }
}

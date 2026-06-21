using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class ExchangeToken : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-EXCHANGE_TOKEN";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.DefenseRegister, AstroLupineKeywords.AttackRegister };

        public ExchangeToken()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.None)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                AttackRegisterPower? atkPower = Owner.Creature.GetPower<AttackRegisterPower>();
                DefenseRegisterPower? defPower = Owner.Creature.GetPower<DefenseRegisterPower>();

                if (atkPower != null && defPower != null)
                {
                    int atk = atkPower.Read();
                    int def = defPower.Read();
                    await atkPower.Write(def);
                    await defPower.Write(atk);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.UpgradeBy(-1);
        }
    }
}

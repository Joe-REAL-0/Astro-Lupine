using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class OpenSourceProtocol : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_OpenSourceProtocol";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Ethereal, AstroLupineKeywords.AttackRegister, AstroLupineKeywords.DefenseRegister, AstroLupineKeywords.DrawRegister, AstroLupineKeywords.Overwrite };

        public OpenSourceProtocol()
            : base(2, CardType.Power, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<OpenSourceProtocolPower>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Ethereal);
        }
    }
}

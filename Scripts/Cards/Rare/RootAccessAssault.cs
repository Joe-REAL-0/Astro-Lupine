using AstroLupine.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;

namespace AstroLupine.Cards.Rare
{
    public class RootAccessAssault : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_RootAccessAssault";

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Write };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DamageVar(15m, ValueProp.Move)
        };

        public RootAccessAssault()
            : base(2, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
        {
            this.HasWriteTag = true;
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null) return;

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);

            int writeValue = (int)base.DynamicVars.Damage.BaseValue;
            await WriteAttackRegister(writeValue, choiceContext);

            bool intentIsAttack = false;
            if (cardPlay.Target.IsMonster && cardPlay.Target.Monster != null)
            {
                var nextMove = cardPlay.Target.Monster.NextMove;
                if (nextMove != null && nextMove.Intents != null)
                {
                    intentIsAttack = nextMove.Intents.Any(i => i is AttackIntent || i is MultiAttackIntent || i is SingleAttackIntent);
                }
            }

            if (intentIsAttack)
            {
                await WriteDefenseRegister(writeValue, choiceContext);
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(3m);
        }
    }
}

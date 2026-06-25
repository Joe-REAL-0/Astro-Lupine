using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class InfectionProtocol : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-INFECTION_PROTOCOL";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new MagicVar(4m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.ZeroDayExploit, AstroLupineKeywords.TrojanHorseVirus };

        public InfectionProtocol()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature == null) return;
            foreach (Creature enemy in Owner.Creature.CombatState.HittableEnemies)
            {
                if (enemy.IsAlive)
                {
                    await PowerCmd.Apply<ZeroDayExploitPower>(choiceContext, enemy, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
                    await PowerCmd.Apply<TrojanHorseVirusPower>(choiceContext, enemy, this.DynamicVars["Magic"].IntValue, Owner.Creature, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            this.RemoveKeyword(CardKeyword.Exhaust);
        }
    }
}

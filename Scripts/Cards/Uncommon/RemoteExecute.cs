using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class RemoteExecute : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-REMOTE_EXECUTE";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
        {
            new BlockVar(0m, ValueProp.Move),
            new MagicVar(4m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.ZeroDayExploit };

        public RemoteExecute()
            : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature == null) return;
            
            int totalRemoved = 0;
            
            foreach (Creature enemy in Owner.Creature.CombatState.HittableEnemies)
            {
                if (enemy.IsAlive)
                {
                    var zeroDay = enemy.GetPower<ZeroDayExploitPower>();
                    if (zeroDay != null)
                    {
                        totalRemoved += zeroDay.Amount;
                        await PowerCmd.Remove(zeroDay);
                    }
                }
            }
            
            if (totalRemoved > 0)
            {
                decimal blockAmount = totalRemoved * this.DynamicVars["Magic"].IntValue;
                await CreatureCmd.GainBlock(Owner.Creature, blockAmount, ValueProp.Move, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(2m);
        }
    }
}
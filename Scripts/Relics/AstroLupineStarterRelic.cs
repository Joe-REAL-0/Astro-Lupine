using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace AstroLupine.Relics
{
    public class AstroLupineStarterRelic : CustomRelicModel
    {
        public const string RelicId = "AstroLupine_Relic_Starter";

        public AstroLupineStarterRelic() : base(false)
        {
        }

        public override RelicRarity Rarity => RelicRarity.Starter;

        public override string PackedIconPath => "res://assets/texture/relic/relic.png";
        protected override string PackedIconOutlinePath => "res://assets/texture/relic/relic.png";
        protected override string BigIconPath => "res://assets/texture/relic/relic.png";

        public override async System.Threading.Tasks.Task BeforeCombatStart()
        {
            if (this.Owner != null)
            {
                var choiceCtx = new MegaCrit.Sts2.Core.GameActions.Multiplayer.ThrowingPlayerChoiceContext();
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.AttackRegisterPower>(choiceCtx, this.Owner.Creature, 6, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.DefenseRegisterPower>(choiceCtx, this.Owner.Creature, 5, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.DrawRegisterPower>(choiceCtx, this.Owner.Creature, 2, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.AstroLupineSystemPower>(choiceCtx, this.Owner.Creature, 1, this.Owner.Creature, null);
            }
        }
    }
}

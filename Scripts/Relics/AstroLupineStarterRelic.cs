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

        public override string PackedIconPath => "res://AstroLupine/assets/texture/relic/old_terminal.png";
        protected override string PackedIconOutlinePath => "res://AstroLupine/assets/texture/relic/old_terminal.png";
        protected override string BigIconPath => "res://AstroLupine/assets/texture/relic/old_terminal.png";

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

        public override MegaCrit.Sts2.Core.Models.RelicModel? GetUpgradeReplacement()
        {
            return MegaCrit.Sts2.Core.Models.ModelDb.Relic<BrandNewTerminalRelic>();
        }
    }
}

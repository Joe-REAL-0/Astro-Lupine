using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace AstroLupine.Relics
{
    [BaseLib.Utils.Pool(typeof(Characters.AstroLupineRelicPool))]
    public class BrandNewTerminalRelic : CustomRelicModel
    {
        public const string RelicId = "AstroLupine_Relic_BrandNewTerminal";

        public BrandNewTerminalRelic() : base(true)
        {
        }

        public override RelicRarity Rarity => RelicRarity.Ancient;

        public override string PackedIconPath => "res://AstroLupine/assets/texture/relic/new_terminal.png";
        protected override string PackedIconOutlinePath => "res://AstroLupine/assets/texture/relic/new_terminal.png";
        protected override string BigIconPath => "res://AstroLupine/assets/texture/relic/new_terminal.png";

        public override async System.Threading.Tasks.Task BeforeCombatStart()
        {
            if (this.Owner != null)
            {
                var choiceCtx = new MegaCrit.Sts2.Core.GameActions.Multiplayer.ThrowingPlayerChoiceContext();
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.AttackRegisterPower>(choiceCtx, this.Owner.Creature, 9, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.DefenseRegisterPower>(choiceCtx, this.Owner.Creature, 8, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.DrawRegisterPower>(choiceCtx, this.Owner.Creature, 2, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.AstroLupineSystemPower>(choiceCtx, this.Owner.Creature, 1, this.Owner.Creature, null);
            }
        }
    }
}

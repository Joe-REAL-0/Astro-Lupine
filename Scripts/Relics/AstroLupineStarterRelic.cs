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

        public override System.Threading.Tasks.Task AfterObtained()
        {
            MegaCrit.Sts2.Core.Combat.CombatManager.Instance.CombatSetUp += OnCombatSetUp;
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public override System.Threading.Tasks.Task AfterRemoved()
        {
            MegaCrit.Sts2.Core.Combat.CombatManager.Instance.CombatSetUp -= OnCombatSetUp;
            return System.Threading.Tasks.Task.CompletedTask;
        }

        private void OnCombatSetUp(MegaCrit.Sts2.Core.Combat.CombatState state) 
        {
            MegaCrit.Sts2.Core.Helpers.TaskHelper.RunSafely(ApplyPowers());
        }

        private async System.Threading.Tasks.Task ApplyPowers()
        {
            if (this.Owner != null)
            {
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.AttackRegisterPower>(this.Owner.Creature, 6, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.DefenseRegisterPower>(this.Owner.Creature, 5, this.Owner.Creature, null);
                await MegaCrit.Sts2.Core.Commands.PowerCmd.Apply<Powers.DrawRegisterPower>(this.Owner.Creature, 3, this.Owner.Creature, null);
            }
        }
    }
}

using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using System.Collections.Generic;

namespace AstroLupine.Characters
{
    public class AstroLupineRelicPool : RelicPoolModel
    {
        // 临时借用铁甲战士的能量颜色名
        // TODO: Phase 4 替换为自定义能量颜色
        public override string EnergyColorName => "ironclad";

        protected override IEnumerable<RelicModel> GenerateAllRelics()
        {
            return new List<RelicModel>
            {
                // 自定义初始遗物
                ModelDb.Relic<Relics.AstroLupineStarterRelic>(),
                // 临时借用铁甲战士遗物池，确保奥涅祝福有足够遗物可选
                // TODO: Phase 3 替换为角色专属遗物
                ModelDb.Relic<Brimstone>(),
                ModelDb.Relic<BurningBlood>(),
                ModelDb.Relic<CharonsAshes>(),
                ModelDb.Relic<DemonTongue>(),
                ModelDb.Relic<PaperPhrog>(),
                ModelDb.Relic<RedSkull>(),
                ModelDb.Relic<RuinedHelmet>(),
                ModelDb.Relic<SelfFormingClay>()
            };
        }
    }
}

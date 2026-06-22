using Godot;
using MegaCrit.Sts2.Core.Models;
using BaseLib.Abstracts;

namespace AstroLupine.Characters
{
    public class AstroLupineCardPool : CustomCardPoolModel
    {
        public override string Title => "AstroLupine";
        
        public override string? BigEnergyIconPath => "res://AstroLupine/assets/texture/character/energy_icon.png";
        public override string? TextEnergyIconPath => "res://AstroLupine/assets/texture/character/text_energy_icon.png";
        
        // 使用 BaseLib 提供的方法，自动根据给定的颜色生成着色器材质，无需手动创建文件
        public override Color ShaderColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override float V => 1.0f; // 强制调高亮度，防止卡牌边框因为暗色变成纯黑
        
        public override Color DeckEntryCardColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override Color EnergyOutlineColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override bool IsColorless => false;

        protected override CardModel[] GenerateAllCards()
        {
            return new CardModel[]
            {
                ModelDb.Card<Cards.Starter.StrikeAstroLupine>(),
                ModelDb.Card<Cards.Starter.DefendAstroLupine>(),
                ModelDb.Card<Cards.Starter.AttackIncAstroLupine>(),
                ModelDb.Card<Cards.Starter.DefenseIncAstroLupine>(),
                
                // Common Attacks
                ModelDb.Card<Cards.Common.RabbitBomb>(),
                ModelDb.Card<Cards.Common.MimicIronWave>(),
                ModelDb.Card<Cards.Common.RewriteSmash>(),
                ModelDb.Card<Cards.Common.KineticIteration>(),
                ModelDb.Card<Cards.Common.DualThread>(),
                ModelDb.Card<Cards.Common.WideAreaCoverage>(),
                
                // Common Defends
                ModelDb.Card<Cards.Common.EnergyBarrier>(),
                ModelDb.Card<Cards.Common.Firewall>(),
                ModelDb.Card<Cards.Common.StackProtection>(),
                ModelDb.Card<Cards.Common.Unload>(),
                ModelDb.Card<Cards.Common.DestroyModule>(),
                
                // Common Skills (Draw)
                ModelDb.Card<Cards.Common.CacheExtraction>(),
                ModelDb.Card<Cards.Common.Preload>(),
                ModelDb.Card<Cards.Common.IterativeRetrieval>(),
                
                // Common Special Skills
                ModelDb.Card<Cards.Common.MountHardDrive>(),
                ModelDb.Card<Cards.Common.Overwrite>(),
                ModelDb.Card<Cards.Common.InterfaceExtension>(),
                ModelDb.Card<Cards.Common.FocusEnergy>(),
                ModelDb.Card<Cards.Common.HarmonicFunction>(),
                
                // Common Equilibrium
                ModelDb.Card<Cards.Common.KineticConversion>(),
                ModelDb.Card<Cards.Common.ComputeOverload>(),

                // Uncommon Cards
                ModelDb.Card<Cards.Uncommon.AllOut>(),
                ModelDb.Card<Cards.Uncommon.Backstep>(),
                ModelDb.Card<Cards.Uncommon.Botnet>(),
                ModelDb.Card<Cards.Uncommon.BufferOverflow>(),
                ModelDb.Card<Cards.Uncommon.ComputeOverdraft>(),
                ModelDb.Card<Cards.Uncommon.CoolingArchitecture>(),
                ModelDb.Card<Cards.Uncommon.Daemon>(),
                ModelDb.Card<Cards.Uncommon.DataCleanse>(),
                ModelDb.Card<Cards.Uncommon.DullCrystal>(),
                ModelDb.Card<Cards.Uncommon.DullCrystalSmash>(),
                ModelDb.Card<Cards.Uncommon.ExceptionHandling>(),
                ModelDb.Card<Cards.Uncommon.ExploitAnomaly>(),
                ModelDb.Card<Cards.Uncommon.GetToken>(),
                ModelDb.Card<Cards.Uncommon.Hack>(),
                ModelDb.Card<Cards.Uncommon.InfectionProtocol>(),
                ModelDb.Card<Cards.Uncommon.KernelHardening>(),
                ModelDb.Card<Cards.Uncommon.LogicBackdoor>(),
                ModelDb.Card<Cards.Uncommon.LoneWolf>(),
                ModelDb.Card<Cards.Uncommon.LupineIntuition>(),
                ModelDb.Card<Cards.Uncommon.MemoryLeak>(),
                ModelDb.Card<Cards.Uncommon.MimicAfterimage>(),
                ModelDb.Card<Cards.Uncommon.MimicDodgeRoll>(),
                ModelDb.Card<Cards.Uncommon.MimicOverclock>(),
                ModelDb.Card<Cards.Uncommon.MimicRecklessImpact>(),
                ModelDb.Card<Cards.Uncommon.MimicRenewSpirit>(),
                ModelDb.Card<Cards.Uncommon.OperatorImpact>(),
                ModelDb.Card<Cards.Uncommon.OperatorOverloading>(),
                ModelDb.Card<Cards.Uncommon.ParallelProcessing>(),
                ModelDb.Card<Cards.Uncommon.PrivilegeEscalation>(),
                ModelDb.Card<Cards.Uncommon.ReadOnlyLock>(),
                ModelDb.Card<Cards.Uncommon.RhombusSlicer>(),
                ModelDb.Card<Cards.Uncommon.Rollback>(),
                ModelDb.Card<Cards.Uncommon.SystemUpgrade>(),
                ModelDb.Card<Cards.Uncommon.ExchangeToken>(),
                ModelDb.Card<Cards.Uncommon.VestAssault>(),

                // Rare Cards
                ModelDb.Card<Cards.Rare.FormatStrike>(),
                ModelDb.Card<Cards.Rare.RootAccessAssault>(),
                ModelDb.Card<Cards.Rare.GarbageCollection>(),
                ModelDb.Card<Cards.Rare.ZeroDayBlast>(),
                ModelDb.Card<Cards.Rare.Deadlock>(),
                ModelDb.Card<Cards.Rare.SudoStrike>(),
                
                ModelDb.Card<Cards.Rare.Redirect>(),
                ModelDb.Card<Cards.Rare.CronJob>(),
                ModelDb.Card<Cards.Rare.LowLevelRefactoring>(),
                ModelDb.Card<Cards.Rare.SystemSnapshot>(),
                ModelDb.Card<Cards.Rare.ConsoleAgent>(),
                ModelDb.Card<Cards.Rare.SandboxMode>(),
                ModelDb.Card<Cards.Rare.DynamicLinking>(),
                
                ModelDb.Card<Cards.Rare.KnowledgeGraph>(),
                ModelDb.Card<Cards.Rare.DAConversion>(),
                ModelDb.Card<Cards.Rare.DeepLearning>(),
                ModelDb.Card<Cards.Rare.Root>(),
                ModelDb.Card<Cards.Rare.HyperThreadingForm>(),
                ModelDb.Card<Cards.Rare.OpenSourceProtocol>(),
                ModelDb.Card<Cards.Rare.TuringComplete>()
            };
        }
    }
}

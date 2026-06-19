using Godot;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Characters
{
    public class AstroLupineCardPool : CardPoolModel
    {
        public override string Title => "AstroLupine";
        public override string EnergyColorName => "AstroLupine_Energy";
        public override string CardFrameMaterialPath => "card_frame_astrolupine"; 
        public override Color DeckEntryCardColor => new Color("B5B9C8FF");
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
                ModelDb.Card<Cards.Common.SynapseStrike>(),
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
                ModelDb.Card<Cards.Common.HarmonicFunction>()
            };
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace AstroLupine.Cards.Starter
{
    public class DefenseIncAstroLupine : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_DefenseInc";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new DynamicVar("Magic", 1m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.DefenseRegister };

        public DefenseIncAstroLupine()
            : base(0, CardType.Skill, CardRarity.Basic, TargetType.None)
        {
        }

        public override async Task OnEnqueuePlayVfx(Creature? target)
        {
            if (base.Owner?.Creature != null)
            {
                await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            // Increment the defense register by Magic amount
            int amount = (int)this.DynamicVars["Magic"].BaseValue;
            await IncDefenseRegister(amount);
            
            if (base.Owner?.Creature != null)
            {
                MegaCrit.Sts2.Core.Nodes.Vfx.NPowerUpVfx.CreateNormal(base.Owner.Creature);
            }
            
            await Task.CompletedTask;
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(1m);
        }
    }
}

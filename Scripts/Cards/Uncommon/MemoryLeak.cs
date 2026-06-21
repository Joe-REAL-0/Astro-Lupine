using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Uncommon
{
    public class MemoryLeak : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-MEMORY_LEAK";

        private int _currentDamage = 4;
        private int _increasedDamage;

        [SavedProperty]
        public int CurrentDamage
        {
            get => _currentDamage;
            set
            {
                AssertMutable();
                _currentDamage = value;
                this.DynamicVars.Damage.BaseValue = _currentDamage;
            }
        }

        [SavedProperty]
        public int IncreasedDamage
        {
            get => _increasedDamage;
            set
            {
                AssertMutable();
                _increasedDamage = value;
            }
        }

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(CurrentDamage),
            new MagicVar(4m)
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Fatal) };

        public MemoryLeak()
            : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null) return;
            
            bool shouldTriggerFatal = cardPlay.Target.Powers.All(p => p.ShouldOwnerDeathTriggerFatal());
            AttackCommand attackCommand = await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_slash");

            if (attackCommand != null && shouldTriggerFatal && attackCommand.Results.SelectMany(r => r).Any(r => r.WasTargetKilled))
            {
                int intValue = this.DynamicVars["Magic"].IntValue;
                BuffFromPlay(intValue);
                (this.DeckVersion as MemoryLeak)?.BuffFromPlay(intValue);
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars["Magic"].UpgradeValueBy(2m);
        }

        private void BuffFromPlay(int extraDamage)
        {
            IncreasedDamage += extraDamage;
            UpdateDamage();
        }

        private void UpdateDamage()
        {
            CurrentDamage = 4 + IncreasedDamage;
        }
    }
}


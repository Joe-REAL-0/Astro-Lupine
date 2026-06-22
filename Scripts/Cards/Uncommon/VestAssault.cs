using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Hooks;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Uncommon
{
    public class VestAssault : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-VEST_ASSAULT";

        protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
        { 
            new AstroReadDamageVar(0m) 
        };

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read, AstroLupineKeywords.Write };

        private bool IsFirstCard => CombatManager.Instance.History.CardPlaysStarted.Count(e => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Owner == base.Owner) <= 1;

        public VestAssault()
            : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Target == null || Owner == null) return;

            decimal modifiedBase = Hook.ModifyDamage(RunState!, CombatState, cardPlay.Target, Owner.Creature, this.DynamicVars.Damage.BaseValue, ValueProp.Move, this, ModifyDamageHookType.All, CardPreviewMode.Normal, out IEnumerable<AbstractModel> _);
            
            decimal finalDamage = modifiedBase;

            if (IsFirstCard)
            {
                finalDamage *= 2;
            }

            await DamageCmd.Attack(finalDamage)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .Unpowered()
                .WithHitFx("vfx/vfx_attack_blunt")
                .Execute(choiceContext);

            await WriteAttackRegister((int)finalDamage);
        }

        protected override void OnUpgrade()
        {
            this.EnergyCost.UpgradeBy((int)(-1m));
        }
    }
}


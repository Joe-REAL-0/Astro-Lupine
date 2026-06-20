using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using AstroLupine.Powers;

using BaseLib.Utils;
using AstroLupine.Characters;

namespace AstroLupine.Cards
{
    [Pool(typeof(AstroLupineCardPool))]
    public abstract class BaseAstroLupineCard : CustomCardModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<AstroLupineCardPool>();
        public override CardPoolModel VisualCardPool => ModelDb.CardPool<AstroLupineCardPool>();

        protected BaseAstroLupineCard(int canonicalEnergyCost, CardType type, CardRarity rarity, TargetType targetType, bool shouldShowInCardLibrary = true) 
            : base(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary)
        {
        }

        /// <summary>
        /// Reads the Attack Register and deals damage.
        /// This ensures the register bonus is added AFTER all normal debuffs (like Weak).
        /// </summary>
        protected async Task DealReadDamage(PlayerChoiceContext choiceContext, CardPlay cardPlay, DynamicVar damageVar, string hitVfx = "vfx/vfx_attack_slash")
        {
            if (cardPlay.Target == null || Owner == null)
            {
                return;
            }

            // Calculate the base damage normally, applying Strength, Weak, etc.
            decimal modifiedBase = Hook.ModifyDamage(
                RunState!, 
                CombatState, 
                cardPlay.Target, 
                Owner.Creature, 
                damageVar.BaseValue, 
                ValueProp.Move, 
                this, 
                ModifyDamageHookType.All, 
                CardPreviewMode.Normal, 
                out IEnumerable<AbstractModel> _
            );

            // Add the attack register value
            int regAmount = Owner.Creature.GetPower<AttackRegisterPower>()?.Read() ?? 0;
            decimal finalDamage = modifiedBase + regAmount;

            // Deal final damage using Unpowered to prevent hooks from applying again
            await DamageCmd.Attack(finalDamage)
                .FromCard(this)
                .Targeting(cardPlay.Target)
                .Unpowered()
                .WithHitFx(hitVfx)
                .Execute(choiceContext);
        }

        /// <summary>
        /// Reads the Defense Register and gains block.
        /// This ensures the register bonus is added AFTER all normal debuffs (like Frail).
        /// </summary>
        protected async Task GainReadBlock(CardPlay? cardPlay, DynamicVar blockVar)
        {
            if (Owner == null)
            {
                return;
            }

            // Calculate base block normally, applying Dexterity, Frail, etc.
            decimal modifiedBase = Hook.ModifyBlock(
                CombatState!, 
                Owner.Creature, 
                blockVar.BaseValue, 
                ValueProp.Move, 
                this, 
                cardPlay, 
                out IEnumerable<AbstractModel> _
            );

            // Add the defense register value
            int regAmount = Owner.Creature.GetPower<DefenseRegisterPower>()?.Read() ?? 0;
            decimal finalBlock = modifiedBase + regAmount;

            // Gain block using Unpowered to prevent hooks from applying again
            await CreatureCmd.GainBlock(Owner.Creature, finalBlock, ValueProp.Unpowered | ValueProp.Move, cardPlay);
        }

        /// <summary>
        /// Reads the Draw Register and draws cards.
        /// </summary>
        protected async Task<int> DrawReadCards(PlayerChoiceContext choiceContext, DynamicVar magicVar)
        {
            if (Owner == null)
            {
                return 0;
            }

            int baseAmount = (int)magicVar.BaseValue;
            int regAmount = Owner.Creature.GetPower<DrawRegisterPower>()?.Read() ?? 0;
            int finalDraw = baseAmount + regAmount;

            await CardPileCmd.Draw(choiceContext, finalDraw, Owner);
            return finalDraw;
        }

        /// <summary>
        /// Writes the specified value to the Attack Register.
        /// </summary>
        protected void WriteAttackRegister(int value)
        {
            Owner?.Creature?.GetPower<AttackRegisterPower>()?.Write(value);
        }

        /// <summary>
        /// Increments the Attack Register by the specified amount.
        /// </summary>
        protected void IncAttackRegister(int amount)
        {
            var power = Owner?.Creature?.GetPower<AttackRegisterPower>();
            if (power != null)
            {
                power.Increment(amount);
            }
        }

        /// <summary>
        /// Writes the specified value to the Defense Register.
        /// </summary>
        protected void WriteDefenseRegister(int value)
        {
            Owner?.Creature?.GetPower<DefenseRegisterPower>()?.Write(value);
        }

        /// <summary>
        /// Increments the Defense Register by the specified amount.
        /// </summary>
        protected void IncDefenseRegister(int amount)
        {
            var power = Owner?.Creature?.GetPower<DefenseRegisterPower>();
            if (power != null)
            {
                power.Increment(amount);
            }
        }

        /// <summary>
        /// Writes the specified value to the Draw Register.
        /// </summary>
        protected void WriteDrawRegister(int value)
        {
            Owner?.Creature?.GetPower<DrawRegisterPower>()?.Write(value);
        }

        /// <summary>
        /// Increments the Draw Register by the specified amount.
        /// </summary>
        protected void IncDrawRegister(int amount)
        {
            var power = Owner?.Creature?.GetPower<DrawRegisterPower>();
            if (power != null)
            {
                power.Increment(amount);
            }
        }
    }
}

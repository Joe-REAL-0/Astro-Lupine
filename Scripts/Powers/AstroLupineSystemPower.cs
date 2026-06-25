using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroLupine.Cards;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AstroLupine.Powers
{
    public class AstroLupineSystemPower : CustomPowerModel
    {
        public const string PowerId = "AstroLupine_SystemPower";
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.None;

        protected override bool IsVisibleInternal => false;

        public Dictionary<CardModel, int> CachedBlockValues = new Dictionary<CardModel, int>();
        public Dictionary<CardModel, int> CachedDamageValues = new Dictionary<CardModel, int>();
        public Dictionary<CardModel, int> CachedDrawValues = new Dictionary<CardModel, int>();

        protected override void DeepCloneFields()
        {
            base.DeepCloneFields();
            CachedBlockValues = new Dictionary<CardModel, int>();
            CachedDamageValues = new Dictionary<CardModel, int>();
            CachedDrawValues = new Dictionary<CardModel, int>();
        }

        public override async Task BeforeCardPlayed(CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner?.Creature != this.Owner) return;

            var card = cardPlay.Card;
            bool hasWrite = false;

            if (card is BaseAstroLupineCard astroCard && astroCard.HasWriteTag) hasWrite = true;
            if (card.Keywords.Contains(AstroLupineKeywords.Write)) hasWrite = true;
            if (this.Owner.GetPower<PrivilegeEscalationPower>() != null && card.Type == CardType.Attack) hasWrite = true;

            if (!hasWrite) return;

            if (card.DynamicVars.ContainsKey("Block"))
            {
                ValueProp blockProps = 0;
                if (card.DynamicVars["Block"] is BlockVar bv) blockProps = bv.Props;

                decimal modifiedBase = MegaCrit.Sts2.Core.Hooks.Hook.ModifyBlock(
                    card.CombatState, 
                    card.Owner.Creature, 
                    card.DynamicVars["Block"].BaseValue, 
                    blockProps, 
                    card, 
                    cardPlay, 
                    out _
                );
                int finalValue = (int)modifiedBase;
                CachedBlockValues[card] = finalValue;
                Godot.GD.Print($"[AstroLupineSystemPower] Cached Block value: {finalValue}");
            }
            if (card.DynamicVars.ContainsKey("Damage"))
            {
                ValueProp damageProps = 0;
                if (card.DynamicVars["Damage"] is DamageVar dv) damageProps = dv.Props;

                decimal modifiedBase = MegaCrit.Sts2.Core.Hooks.Hook.ModifyDamage(
                    card.RunState, 
                    card.CombatState, 
                    cardPlay.Target, 
                    card.Owner.Creature, 
                    card.DynamicVars["Damage"].BaseValue, 
                    damageProps, 
                    card, 
                    MegaCrit.Sts2.Core.Hooks.ModifyDamageHookType.All, 
                    CardPreviewMode.Normal, 
                    out _
                );
                int finalValue = (int)modifiedBase;
                CachedDamageValues[card] = finalValue;
                Godot.GD.Print($"[AstroLupineSystemPower] Cached Damage value: {finalValue}");
            }
            if (card.DynamicVars.ContainsKey("Cards"))
            {
                int finalValue = (int)card.DynamicVars["Cards"].BaseValue;
                if (card.DynamicVars["Cards"] is AstroReadCardsVar)
                {
                    finalValue += card.Owner.Creature.GetPower<DrawRegisterPower>()?.Read() ?? 0;
                }
                CachedDrawValues[card] = finalValue;
            }

            await Task.CompletedTask;
        }

        public override async Task AfterCardPlayed(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner?.Creature != this.Owner)
            {
                return;
            }

            var card = cardPlay.Card;
            bool hasWrite = false;

            if (card is BaseAstroLupineCard astroCard && astroCard.HasWriteTag)
            {
                hasWrite = true;
            }
            if (card.Keywords.Contains(AstroLupineKeywords.Write))
            {
                hasWrite = true;
            }
            if (this.Owner.GetPower<PrivilegeEscalationPower>() != null && card.Type == CardType.Attack)
            {
                hasWrite = true;
            }

            Godot.GD.Print($"[AstroLupineSystemPower] AfterCardPlayed for {card.Id}. hasWrite = {hasWrite}");

            if (!hasWrite)
            {
                return;
            }

            var hyper = this.Owner.GetPower<HyperThreadingFormPower>();
            bool isHyperTriggered = false;
            int hyperMultiplier = 1;
            if (hyper != null && !hyper.HasTriggeredThisTurn)
            {
                isHyperTriggered = true;
                hyperMultiplier = (int)System.Math.Pow(2, hyper.Amount);
                hyper.HasTriggeredThisTurn = true;
                hyper.FlashMe();
            }

            // Attack Register
            if (card.Keywords.Contains(AstroLupineKeywords.AttackOverwrite) || card.Type == CardType.Attack)
            {
                Godot.GD.Print($"[AstroLupineSystemPower] Checking Attack Overwrite for {card.Id}");
                if (CachedDamageValues.TryGetValue(card, out int dmgValue))
                {
                    var attackRegister = this.Owner.GetPower<AttackRegisterPower>();
                    if (attackRegister != null)
                    {
                        if (isHyperTriggered) dmgValue *= hyperMultiplier;
                        Godot.GD.Print($"[AstroLupineSystemPower] Executing attackRegister.Write({dmgValue})");
                        await attackRegister.Write(dmgValue, choiceContext);
                    }
                    CachedDamageValues.Remove(card);
                }
                else
                {
                    Godot.GD.Print($"[AstroLupineSystemPower] Value not found in CachedDamageValues for {card.Id}");
                }
            }

            // Defense Register
            if (card.Keywords.Contains(AstroLupineKeywords.DefenseOverwrite) || card.Type == CardType.Skill)
            {
                Godot.GD.Print($"[AstroLupineSystemPower] Checking Defense Overwrite for {card.Id}");
                if (CachedBlockValues.TryGetValue(card, out int blkValue))
                {
                    var defenseRegister = this.Owner.GetPower<DefenseRegisterPower>();
                    if (defenseRegister != null)
                    {
                        if (isHyperTriggered) blkValue *= hyperMultiplier;
                        Godot.GD.Print($"[AstroLupineSystemPower] Executing defenseRegister.Write({blkValue})");
                        await defenseRegister.Write(blkValue, choiceContext);
                    }
                    else 
                    {
                        Godot.GD.Print($"[AstroLupineSystemPower] ERROR: defenseRegister is null");
                    }
                    CachedBlockValues.Remove(card);
                }
                else
                {
                    Godot.GD.Print($"[AstroLupineSystemPower] Value not found in CachedBlockValues for {card.Id}");
                }
            }

            // Draw Register
            if (card.Keywords.Contains(AstroLupineKeywords.DrawOverwrite) || card.Type == CardType.Skill)
            {
                if (CachedDrawValues.TryGetValue(card, out int drawValue))
                {
                    var drawRegister = this.Owner.GetPower<DrawRegisterPower>();
                    if (drawRegister != null)
                    {
                        if (isHyperTriggered) drawValue *= hyperMultiplier;
                        await drawRegister.Write(drawValue, choiceContext);
                    }
                    CachedDrawValues.Remove(card);
                }
            }
        }

        public int TotalZeroDayAppliedThisCombat { get; set; } = 0;

        public override async Task AfterPowerAmountChanged(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, PowerModel power, decimal amount, MegaCrit.Sts2.Core.Entities.Creatures.Creature? applier, CardModel? cardSource)
        {
            if (power is ZeroDayExploitPower && amount > 0)
            {
                TotalZeroDayAppliedThisCombat += (int)amount;
                Godot.GD.Print($"[AstroLupineSystemPower] ZeroDayExploit applied! Amount: {amount}. Total this combat: {TotalZeroDayAppliedThisCombat}");
            }
            await base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);
        }
    }
}

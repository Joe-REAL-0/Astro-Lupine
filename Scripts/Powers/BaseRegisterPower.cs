using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using BaseLib.Abstracts;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace AstroLupine.Powers
{
    public abstract class BaseRegisterPower : CustomPowerModel
    {
        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;

        public BaseRegisterPower(int initialAmount) : base()
        {
            // The initial amount will be applied by PowerCmd or restored from save
            // when ApplyInternal / SetAmount is called by the game.
            // We shouldn't track our own amount field.
        }

        // 写入：覆盖赋值
        public virtual async Task Write(int value, PlayerChoiceContext? choiceContext = null)
        {
            if (this.Owner != null && this.Owner.GetPower<KernelHardeningPower>() != null)
            {
                // 如果拥有内核加固Buff，则所有寄存器被锁定，无法写入
                return;
            }

            if (this.Owner != null)
            {
                var sandbox = this.Owner.GetPower<SandboxModePower>();
                if (sandbox != null)
                {
                    await CreatureCmd.GainBlock(this.Owner, value * sandbox.Amount, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move, null);
                    return;
                }

                var hyper = this.Owner.GetPower<HyperThreadingFormPower>();
                if (hyper != null && !hyper.HasTriggeredThisTurn)
                {
                    value *= 2;
                    hyper.HasTriggeredThisTurn = true;
                }
            }

            int minVal = GetMinThreshold();
            if (value < minVal) value = minVal;

            int oldAmount = this.Amount;
            this.SetAmount(value);

            if (oldAmount != this.Amount)
            {
                OnAmountChanged();
                await TriggerExceptionHandling(choiceContext);
            }
        }

        // 读取：获取当前值
        public virtual int Read()
        {
            int baseAmount = this.Amount;
            if (this.Owner != null && this.Owner.GetPower<RootPower>() != null)
            {
                int atk = this.Owner.GetPower<AttackRegisterPower>()?.Amount ?? 0;
                int def = this.Owner.GetPower<DefenseRegisterPower>()?.Amount ?? 0;
                int drw = this.Owner.GetPower<DrawRegisterPower>()?.Amount ?? 0;
                baseAmount = System.Math.Max(atk, System.Math.Max(def, drw));
            }

            if (this.Owner != null && this.Owner.GetPower<ParallelProcessingPower>() != null)
            {
                baseAmount *= 2;
            }
            return baseAmount;
        }

        // 自增/自减：基于当前值的增量运算（支持负数）
        public virtual async Task Increment(int delta, PlayerChoiceContext? choiceContext = null)
        {
            if (this.Owner != null && this.Owner.GetPower<KernelHardeningPower>() != null)
            {
                // 如果拥有内核加固Buff，则所有寄存器被锁定，无法自增或自减
                return;
            }

            int minVal = GetMinThreshold();
            int newVal = this.Amount + delta;
            if (newVal < minVal) newVal = minVal;

            int oldAmount = this.Amount;
            this.SetAmount(newVal);

            if (oldAmount != this.Amount)
            {
                OnAmountChanged();
                await TriggerExceptionHandling(choiceContext);
            }
        }

        // 触发 UI 和 VFX 刷新（子类或具体实现中抛出事件）
        protected virtual void OnAmountChanged()
        {
            // 此处后续可补充发出 Godot Signal 或刷新特效的逻辑
        }

        protected virtual int GetMinThreshold()
        {
            if (this.Owner != null)
            {
                var lockPower = this.Owner.GetPower<ReadOnlyLockPower>();
                if (lockPower != null)
                {
                    return lockPower.Amount;
                }
            }
            return 1;
        }

        protected virtual async Task TriggerExceptionHandling(PlayerChoiceContext? choiceContext)
        {
            if (this.Owner != null)
            {
                var exPower = this.Owner.GetPower<ExceptionHandlingPower>();
                if (exPower != null)
                {
                    await exPower.OnRegisterChanged(choiceContext);
                }
            }
        }
    }
}

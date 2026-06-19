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
        public virtual void Write(int value)
        {
            this.SetAmount(value);
            OnAmountChanged();
        }

        // 读取：获取当前值
        public virtual int Read()
        {
            return this.Amount;
        }

        // 自增/自减：基于当前值的增量运算（支持负数）
        public virtual void Increment(int delta)
        {
            this.SetAmount(this.Amount + delta);
            OnAmountChanged();
        }

        // 触发 UI 和 VFX 刷新（子类或具体实现中抛出事件）
        protected virtual void OnAmountChanged()
        {
            // 此处后续可补充发出 Godot Signal 或刷新特效的逻辑
        }
    }
}

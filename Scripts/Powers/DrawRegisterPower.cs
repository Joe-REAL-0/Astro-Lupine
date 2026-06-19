using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class DrawRegisterPower : BaseRegisterPower
    {
        public const string PowerId = "AstroLupine_DrawRegister";

        // 临时占位：使用金属化(Metallicize)图标
        public override string? CustomPackedIconPath => "res://images/powers/focus.png";

        public DrawRegisterPower() : base(3)
        {
        }
    }
}

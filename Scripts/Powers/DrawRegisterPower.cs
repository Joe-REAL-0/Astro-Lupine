using BaseLib.Abstracts;

namespace AstroLupine.Powers
{
    public class DrawRegisterPower : BaseRegisterPower
    {
        public const string PowerId = "AstroLupine_DrawRegister";

        public override string? CustomPackedIconPath => "res://assets/texture/power/draw_register.png";

        public DrawRegisterPower() : base(2)
        {
        }

        protected override int GetMinThreshold()
        {
            return 1;
        }
    }
}

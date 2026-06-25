using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Rare
{
    public class DAConversion : BaseAstroLupineCard
    {
        public const string CardId = "ASTROLUPINE-DA_CONVERSION";

    public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { CardKeyword.Exhaust, AstroLupineKeywords.DefenseRegister, AstroLupineKeywords.AttackRegister };

        protected override IEnumerable<DynamicVar> CanonicalVars => new[]
        {
            new DynamicVar("Magic", 4m),
            new DynamicVar("Magic2", 3m)
        };

        public DAConversion()
            : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Owner?.Creature != null)
            {
                AttackRegisterPower? atkPower = Owner.Creature.GetPower<AttackRegisterPower>();
                DefenseRegisterPower? defPower = Owner.Creature.GetPower<DefenseRegisterPower>();

                int strGain = 0;
                if (atkPower != null)
                {
                    strGain = atkPower.Read() / (int)base.DynamicVars["Magic"].BaseValue;
                }

                int dexGain = 0;
                if (defPower != null)
                {
                    dexGain = defPower.Read() / (int)base.DynamicVars["Magic2"].BaseValue;
                }

                if (strGain > 0)
                {
                    await PowerCmd.Apply<StrengthPower>(choiceContext, Owner.Creature, strGain, Owner.Creature, this);
                }

                if (dexGain > 0)
                {
                    await PowerCmd.Apply<DexterityPower>(choiceContext, Owner.Creature, dexGain, Owner.Creature, this);
                }

                DrawRegisterPower? drawPower = Owner.Creature.GetPower<DrawRegisterPower>();
                if (atkPower != null) await atkPower.Write(2, choiceContext);
                if (defPower != null) await defPower.Write(2, choiceContext);
                if (drawPower != null) await drawPower.Write(2, choiceContext);
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["Magic"].UpgradeValueBy(-1m);
            base.DynamicVars["Magic2"].UpgradeValueBy(-1m);
        }
    }
}

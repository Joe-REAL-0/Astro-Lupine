using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Entities.Creatures;
using AstroLupine.Powers;

namespace AstroLupine.Cards.Common
{
    public class Lesion : BaseAstroLupineCard
    {
        public const string CardId = "AstroLupine_Card_Lesion";

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                var vars = new List<DynamicVar> { new AstroReadDamageVar(0m) };
                vars.AddRange(MakeCalculatedVar("Times", 0, (card, target) =>
                {
                    if (target != null)
                    {
                        var virus = target.GetPower<TrojanHorseVirusPower>();
                        if (virus != null) return virus.Amount;
                    }
                    return 0m;
                }));
                return vars;
            }
        }

        public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

        public Lesion()
            : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
        {
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int times = 0;
            if (cardPlay.Target != null)
            {
                var virus = cardPlay.Target.GetPower<TrojanHorseVirusPower>();
                if (virus != null)
                {
                    times += virus.Amount;
                }
            }

            for (int i = 0; i < times; i++)
            {
                await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_slash");
            }
        }

        protected override void OnUpgrade()
        {
            this.DynamicVars.Damage.UpgradeValueBy(1m);
        }
    }
}

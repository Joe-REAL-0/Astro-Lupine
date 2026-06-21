using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AstroLupine.Cards.Common
{
	public class MimicIronWave : BaseAstroLupineCard
	{
		public const string CardId = "AstroLupine_Card_MimicIronWave";

		protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[] 
		{ 
			new AstroReadDamageVar(0m),
			new AstroReadBlockVar(0m)
		};

		public override IEnumerable<CardKeyword> CanonicalKeywords => new[] { AstroLupineKeywords.Read };

		public MimicIronWave()
			: base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
		{
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await DealReadDamage(choiceContext, cardPlay, this.DynamicVars.Damage, "vfx/vfx_attack_blunt");
			await GainReadBlock(cardPlay, this.DynamicVars.Block.BaseValue);
		}

		protected override void OnUpgrade()
		{
			this.DynamicVars.Damage.UpgradeValueBy(2m);
			this.DynamicVars.Block.UpgradeValueBy(2m);
		}
	}
}

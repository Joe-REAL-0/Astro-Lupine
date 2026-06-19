using BaseLib.Abstracts;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Entities.Characters;
using AstroLupine.Cards.Starter;
using HarmonyLib;

namespace AstroLupine.Characters
{
    [HarmonyPatch(typeof(CharacterModel), nameof(CharacterModel.CharacterSelectTransitionPath), MethodType.Getter)]
    public static class CharacterTransitionPathPatch
    {
        public static void Postfix(CharacterModel __instance, ref string __result)
        {
            if (__instance is AstroLupineCharacter)
            {
                __result = "res://materials/transitions/ironclad_transition_mat.tres";
            }
        }
    }

    public class AstroLupineCharacter : CustomCharacterModel
    {
        public const string CharacterId = "AstroLupine_Character";

        public override int StartingHp => 60;
        public override int StartingGold => 99;
        public override Godot.Color NameColor => new Godot.Color("B5B9C8FF");
        
        public override CharacterGender Gender => default;
        
        public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
        {
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<DefendAstroLupine>(),
            ModelDb.Card<DefendAstroLupine>(),
            ModelDb.Card<DefendAstroLupine>(),
            ModelDb.Card<DefendAstroLupine>(),
            ModelDb.Card<DefendAstroLupine>(),
            ModelDb.Card<AttackIncAstroLupine>(),
            ModelDb.Card<DefenseIncAstroLupine>()
        };

        public override IReadOnlyList<RelicModel> StartingRelics => new List<RelicModel> { ModelDb.Relic<Relics.AstroLupineStarterRelic>() };

        public override CardPoolModel CardPool => ModelDb.CardPool<AstroLupineCardPool>();
        public override RelicPoolModel RelicPool => ModelDb.RelicPool<AstroLupineRelicPool>();
        public override PotionPoolModel PotionPool => ModelDb.PotionPool<MegaCrit.Sts2.Core.Models.PotionPools.IroncladPotionPool>();

        public override List<string> GetArchitectAttackVfx()
        {
            return new List<string> { "vfx/vfx_attack_slash" };
        }

        // =============================================
        // 临时占位：复用铁甲战士 (Ironclad) 的美术资源
        // TODO: Phase 4 替换为星际"狼"客专属素材
        // =============================================

        // 角色选择界面 - 背景场景
        public override string? CustomCharacterSelectBg => "res://scenes/screens/char_select/char_select_bg_ironclad.tscn";
        // 角色选择界面 - 头像图标
        public override string? CustomCharacterSelectIconPath => "res://images/packed/character_select/char_select_ironclad.png";
        // 战斗中角色立绘/动画
        public override string? CustomVisualPath => "res://scenes/creature_visuals/ironclad.tscn";
        // 卡牌拖尾特效
        public override string? CustomTrailPath => "res://scenes/vfx/card_trail_ironclad.tscn";
        // 左上角运行图标场景
        public override string? CustomIconPath => "res://scenes/ui/character_icons/ironclad_icon.tscn";
        // 左上角运行图标纹理
        public override string? CustomIconTexturePath => "res://images/ui/top_panel/character_icon_ironclad.png";
        // 多人游戏地图上的图标轮廓
        public override string? CustomIconOutlineTexturePath => "res://images/ui/top_panel/character_icon_ironclad_outline.png";
        // 未解锁时的选人头像
        public override string? CustomCharacterSelectLockedIconPath => "res://images/packed/character_select/char_select_ironclad_locked.png";
        // 地图标记图标
        protected override string MapMarkerPath => "res://images/packed/map/icons/map_marker_ironclad.png";

        // 能量计数器
        public override string? CustomEnergyCounterPath => "res://scenes/combat/energy_counters/ironclad_energy_counter.tscn";
        // 篝火休息动画
        public override string? CustomRestSiteAnimPath => "res://scenes/rest_site/characters/ironclad_rest_site.tscn";
        // 商店动画
        public override string? CustomMerchantAnimPath => "res://scenes/merchant/characters/ironclad_merchant.tscn";

        public override float AttackAnimDelay => 0.15f;
        public override float CastAnimDelay => 0.25f;
        public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

        // 色彩设置（淡灰色主题）
        public override Godot.Color EnergyLabelOutlineColor => new Godot.Color("B5B9C8FF");
        public override Godot.Color DialogueColor => new Godot.Color("666666FF");
        public override MegaCrit.Sts2.Core.Nodes.Vfx.VfxColor SpeechBubbleColor => MegaCrit.Sts2.Core.Nodes.Vfx.VfxColor.DarkGray;
        public override Godot.Color MapDrawingColor => new Godot.Color("B3B3B3FF");
        public override Godot.Color RemoteTargetingLineColor => new Godot.Color("CCCCCCFF");
        public override Godot.Color RemoteTargetingLineOutline => new Godot.Color("4D4D4DFF");

        // UI音效
        public override string CharacterSelectSfx => "event:/sfx/characters/ironclad/ironclad_select";

        // 肢体素材 (事件等)
        public override string? CustomArmPointingTexturePath => "res://images/creatures/ironclad/arm_pointing.png";
        public override string? CustomArmRockTexturePath => "res://images/creatures/ironclad/arm_rock.png";
        public override string? CustomArmPaperTexturePath => "res://images/creatures/ironclad/arm_paper.png";
        public override string? CustomArmScissorsTexturePath => "res://images/creatures/ironclad/arm_scissors.png";
    }
}

using BaseLib.Abstracts;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Entities.Characters;
using AstroLupine.Cards.Starter;
using HarmonyLib;

namespace AstroLupine.Characters
{


    public class AstroLupineCharacter : CustomCharacterModel
    {
        public const string CharacterId = "AstroLupine_Character";

        public override int StartingHp => 60;
        public override int StartingGold => 99;
        public override Godot.Color NameColor => Godot.Color.FromHtml("#00FFCCFF");
        
        public override CharacterGender Gender => default;
        
        public override IEnumerable<CardModel> StartingDeck => new List<CardModel>
        {
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
            ModelDb.Card<StrikeAstroLupine>(),
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
        public override string? CustomCharacterSelectBg => "res://scenes/screens/char_select/char_select_bg_astrolupine.tscn";
        // 角色选择界面 - 角色切换转场特效
        public override string? CustomCharacterSelectTransitionPath => "res://materials/transitions/ironclad_transition_mat.tres";
        // 角色选择界面 - 头像图标
        public override string? CustomCharacterSelectIconPath => "res://AstroLupine/assets/texture/character/choose_character_head.png";
        // 战斗中角色立绘/动画
        public override string? CustomVisualPath => "res://scenes/creature_visuals/astrolupine_visuals.tscn";
        // 卡牌拖尾特效
        public override string? CustomTrailPath => "res://scenes/vfx/card_trail_ironclad.tscn";
        // 左上角运行图标场景
        public override string? CustomIconPath => "res://scenes/ui/character_icons/ironclad_icon.tscn";
        // 左上角运行图标纹理
        public override string? CustomIconTexturePath => "res://AstroLupine/assets/texture/character/head_icon.png";

        public override Godot.Control? CustomIcon
        {
            get
            {
                var iconControl = MegaCrit.Sts2.Core.Assets.PreloadManager.Cache.GetScene("res://scenes/ui/character_icons/ironclad_icon.tscn").Instantiate<Godot.Control>();
                var texture = Godot.ResourceLoader.Load<Godot.Texture2D>(CustomIconTexturePath);

                ReplaceTextureRecursive(iconControl, texture);
                return iconControl;
            }
        }

        private void ReplaceTextureRecursive(Godot.Node node, Godot.Texture2D texture)
        {
            if (node is Godot.TextureRect tr)
            {
                tr.Texture = texture;
            }
            foreach (var child in node.GetChildren())
            {
                ReplaceTextureRecursive(child, texture);
            }
        }
        // 多人游戏地图上的图标轮廓
        public override string? CustomIconOutlineTexturePath => "res://images/ui/top_panel/character_icon_ironclad_outline.png";
        // 未解锁时的选人头像
        public override string? CustomCharacterSelectLockedIconPath => "res://AstroLupine/assets/texture/character/choose_character_head.png"; // 暂时复用已解锁时的头像
        // 地图标记图标
        protected override string MapMarkerPath => "res://images/packed/map/icons/map_marker_ironclad.png";

        // 能量计数器
        public override string? CustomEnergyCounterPath => null;
        public override CustomEnergyCounter? CustomEnergyCounter => new CustomEnergyCounter(
            layer => "res://AstroLupine/assets/texture/character/energy_icon.png", 
            Godot.Color.FromHtml("#2D3B4FFF"), 
            Godot.Color.FromHtml("#00FFCCFF")
        );
        // 篝火休息动画
        public override string? CustomRestSiteAnimPath => "res://scenes/rest_site/characters/ironclad_rest_site.tscn";
        // 商店动画
        public override string? CustomMerchantAnimPath => "res://scenes/merchant/characters/ironclad_merchant.tscn";

        public override float AttackAnimDelay => 0.15f;
        public override float CastAnimDelay => 0.25f;
        public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

        // 色彩设置（深蓝灰色主题）
        public override Godot.Color EnergyLabelOutlineColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override Godot.Color DialogueColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override MegaCrit.Sts2.Core.Nodes.Vfx.VfxColor SpeechBubbleColor => MegaCrit.Sts2.Core.Nodes.Vfx.VfxColor.DarkGray;
        public override Godot.Color MapDrawingColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override Godot.Color RemoteTargetingLineColor => Godot.Color.FromHtml("#2D3B4FFF");
        public override Godot.Color RemoteTargetingLineOutline => Godot.Color.FromHtml("#1A2230FF"); // 使用更深的同色系作为描边

        // UI音效
        public override string CharacterSelectSfx => "event:/sfx/characters/ironclad/ironclad_select";

        // 肢体素材 (事件等)
        public override string? CustomArmPointingTexturePath => "res://images/creatures/ironclad/arm_pointing.png";
        public override string? CustomArmRockTexturePath => "res://images/creatures/ironclad/arm_rock.png";
        public override string? CustomArmPaperTexturePath => "res://images/creatures/ironclad/arm_paper.png";
        public override string? CustomArmScissorsTexturePath => "res://images/creatures/ironclad/arm_scissors.png";
    }
}

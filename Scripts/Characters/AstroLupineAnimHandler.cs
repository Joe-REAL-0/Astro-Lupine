using Godot;
using MegaCrit.Sts2.Core.Nodes.Combat;
using System.Collections.Generic;

namespace AstroLupine.Characters
{
    public partial class AstroLupineAnimHandler : Node
    {
        public NCreatureVisuals Visuals;
        [Export]
        public Sprite2D Sprite;
        [Export]
        public Node2D VisualsRoot;
        
        private Dictionary<string, Texture2D> _textures = new();
        private Tween _jumpTween;

        public override void _Ready()
        {
            base._Ready();

            // Auto-fetch nodes if not assigned in Editor
            if (VisualsRoot == null)
            {
                VisualsRoot = GetNodeOrNull<Node2D>("%Visuals");
            }
            if (Sprite == null)
            {
                Sprite = GetNodeOrNull<Sprite2D>("%Visuals/Sprite2D");
            }
            
            // Load textures manually because Godot Modding prevents attaching C# scripts to .tscn files
            _textures["idle"] = GD.Load<Texture2D>("res://assets/texture/character/anim/standing.png");
            _textures["attack"] = GD.Load<Texture2D>("res://assets/texture/character/anim/attack.png");
            _textures["cast"] = GD.Load<Texture2D>("res://assets/texture/character/anim/cast.png");
            _textures["powerup"] = GD.Load<Texture2D>("res://assets/texture/character/anim/power_up.png");

            if (Sprite != null)
            {
                Sprite.Texture = _textures["idle"];
                // 缩放比例去除了硬编码，你可以直接在Godot引擎的 Sprite2D 节点中调整 Scale
            }
        }

        public void PlayAnimation(string trigger)
        {
            if (Sprite == null) return;

            string key = trigger.ToLowerInvariant();
            if (key == "powerup" || key == "power_up") key = "powerup";
            else if (!_textures.ContainsKey(key)) key = "idle";

            if (_textures.TryGetValue(key, out Texture2D tex))
            {
                Sprite.Texture = tex;
            }

            if (_jumpTween != null && _jumpTween.IsValid())
            {
                _jumpTween.Kill();
            }

            _jumpTween = CreateTween();
            
            float startY = 0f;
            float jumpY = -30.0f;

            _jumpTween.TweenProperty(VisualsRoot, "position:y", jumpY, 0.15f).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Quad);
            _jumpTween.TweenProperty(VisualsRoot, "position:y", startY, 0.15f).SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Quad);

            if (key != "idle")
            {
                _jumpTween.TweenInterval(0.3f);
                _jumpTween.TweenCallback(Callable.From(() => 
                {
                    if (_textures.ContainsKey("idle") && Sprite.Texture == tex)
                    {
                        Sprite.Texture = _textures["idle"];
                    }
                }));
            }
        }

        public override void _Input(InputEvent @event)
        {
            // 当你单独运行这个场景时，按下键盘按键即可预览动画
            if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
            {
                if (keyEvent.Keycode == Key.A) PlayAnimation("attack");
                else if (keyEvent.Keycode == Key.S) PlayAnimation("cast");
                else if (keyEvent.Keycode == Key.D) PlayAnimation("powerup");
            }
        }
    }
}

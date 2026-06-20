using Godot;
using MegaCrit.Sts2.Core.Nodes.Combat;
using System.Collections.Generic;

namespace AstroLupine.Characters
{
    public partial class AstroLupineAnimHandler : Node
    {
        public NCreatureVisuals Visuals;
        public Sprite2D Sprite;
        public Node2D VisualsRoot;
        
        private Dictionary<string, Texture2D> _textures = new();
        private Tween _jumpTween;

        public override void _Ready()
        {
            base._Ready();
            
            // Load textures
            _textures["idle"] = GD.Load<Texture2D>("res://assets/texture/character/anim/standing.png");
            _textures["attack"] = GD.Load<Texture2D>("res://assets/texture/character/anim/attack.png");
            _textures["cast"] = GD.Load<Texture2D>("res://assets/texture/character/anim/cast.png");
            _textures["powerup"] = GD.Load<Texture2D>("res://assets/texture/character/anim/power_up.png");

            if (Sprite != null)
            {
                Sprite.Texture = _textures["idle"];
                // 限制贴图尺寸，0.35 是一个缩小比例，你可以按需调整大小
                Sprite.Scale = new Vector2(0.15f, 0.15f);
            }
        }

        public void PlayAnimation(string trigger)
        {
            if (Sprite == null) return;

            string key = trigger.ToLowerInvariant();
            if (key == "powerup" || key == "power_up") key = "powerup";
            else if (!_textures.ContainsKey(key)) key = "idle";

            Sprite.Texture = _textures[key];

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
                    if (Sprite.Texture == _textures[key])
                    {
                        Sprite.Texture = _textures["idle"];
                    }
                }));
            }
        }
    }
}

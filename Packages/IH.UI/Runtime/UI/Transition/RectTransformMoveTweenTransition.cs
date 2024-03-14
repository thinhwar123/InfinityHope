using UnityEngine;
using DG.Tweening;

namespace HI.UI.Transitions
{
    public class RectTransformMoveTweenTransition : TweenTransition
    {
        [field: SerializeField] public RectTransform Owner { get; private set; }
        [field: SerializeField] public Vector2 MoveOffset { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
        private Vector2 StartPosition { get; set; }

        public override void Init()
        {
            base.Init();
            StartPosition = Owner.anchoredPosition;
        }

        public override Tween Play()
        {
            MainTween = Owner.DOAnchorPos(StartPosition + MoveOffset, Duration).SetEase(Ease).SetDelay(Delay);
            return MainTween;
        }

        public override Tween Kill()
        {
            MainTween?.Kill();
            return MainTween;
        }

        public override float GetDuration()
        {
            return Duration;
        }

        public override float GetDelay()
        {
            return Delay;
        }
    }

}
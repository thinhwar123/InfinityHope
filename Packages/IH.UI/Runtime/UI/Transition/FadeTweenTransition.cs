using DG.Tweening;
using UnityEngine;

namespace HI.UI.Transitions
{
    public class FadeTweenTransition : TweenTransition
    {
        [field: SerializeField] public CanvasGroup Owner { get; set; }
        [field: SerializeField] public float AlphaValue { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Delay { get; set; }
        [field: SerializeField] public Ease Ease { get; set; }

        private float StartValue { get; set; }
        private float EndValue { get; set; }

        public override void Init()
        {
            base.Init();
            switch (TargetValueType)
            {
                case TargetValueType.Direct:
                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            StartValue = AlphaValue;
                            EndValue = Owner.alpha;
                            break;
                        case TransitionType.To:
                            StartValue = Owner.alpha;
                            EndValue = AlphaValue;
                            break;
                    }

                    break;
                case TargetValueType.OffsetFromStart:
                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            EndValue = Owner.alpha;
                            StartValue = EndValue + AlphaValue;
                            break;
                        case TransitionType.To:
                            StartValue = Owner.alpha;
                            EndValue = StartValue + AlphaValue;
                            break;
                    }

                    break;

            }
        }

        public override void SetupStart()
        {
            base.SetupStart();
            Owner.alpha = StartValue;
        }

        public override Tween Play()
        {
            switch (TransitionConfig)
            {
                case TransitionConfig.NeedSetupStart:
                    SetupStart();
                    break;
                case TransitionConfig.Continue:
                    break;
            }

            MainTween = Owner.DOFade(EndValue, Duration).SetEase(Ease).SetDelay(Delay);
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

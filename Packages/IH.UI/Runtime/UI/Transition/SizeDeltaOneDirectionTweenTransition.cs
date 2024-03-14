using DG.Tweening;
using UnityEngine;

namespace HI.UI.Transitions
{
    public class SizeDeltaOneDirectionTweenTransition : TweenTransition
    {
        public enum TweenConfig
        {
            X,
            Y,
        }

        [field: SerializeField] public RectTransform Owner { get; private set; }
        [field: SerializeField] public TweenConfig Config { get; private set; }
        [field: SerializeField] public float FloatValue { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }

        private Vector2 StartSizeDelta { get; set; }
        private Vector2 EndSizeDelta { get; set; }

        public override void Init()
        {
            base.Init();
            Vector2 tempValue = Owner.sizeDelta;
            switch (TargetValueType)
            {
                case TargetValueType.Direct:
                    switch (Config)
                    {
                        case TweenConfig.X:
                            tempValue = new Vector2(Owner.sizeDelta.x, FloatValue);
                            break;
                        case TweenConfig.Y:
                            tempValue = new Vector2(FloatValue, Owner.sizeDelta.y);
                            break;
                    }

                    switch (TransitionType)
                    {

                        case TransitionType.From:

                            StartSizeDelta = tempValue;
                            EndSizeDelta = Owner.sizeDelta;
                            break;
                        case TransitionType.To:
                            StartSizeDelta = Owner.sizeDelta;
                            EndSizeDelta = tempValue;
                            break;
                    }

                    break;
                case TargetValueType.OffsetFromStart:
                    switch (Config)
                    {
                        case TweenConfig.X:
                            tempValue = new Vector2(Owner.sizeDelta.x, Owner.sizeDelta.y + FloatValue);
                            break;
                        case TweenConfig.Y:
                            tempValue = new Vector2(Owner.sizeDelta.x + FloatValue, Owner.sizeDelta.y);
                            break;
                    }

                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            StartSizeDelta = tempValue;
                            EndSizeDelta = Owner.sizeDelta;
                            break;
                        case TransitionType.To:
                            StartSizeDelta = Owner.sizeDelta;
                            EndSizeDelta = tempValue;
                            break;
                    }

                    break;
            }
        }

        public override void SetupStart()
        {
            base.SetupStart();
            Owner.sizeDelta = StartSizeDelta;
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

            MainTween = Owner.DOSizeDelta(EndSizeDelta, Duration).SetEase(Ease).SetDelay(Delay);
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
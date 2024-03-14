using DG.Tweening;
using UnityEngine;

namespace HI.UI.Transitions
{
    public class LocalMoveTweenTransition : TweenTransition
    {
        [field: SerializeField] public Transform Owner { get; private set; }
        [field: SerializeField] public Vector3 MoveValue { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }

        private Vector3 StartPosition { get; set; }
        private Vector3 EndPosition { get; set; }

        public override void Init()
        {
            base.Init();
            switch (TargetValueType)
            {
                case TargetValueType.Direct:
                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            StartPosition = MoveValue;
                            EndPosition = Owner.localPosition;
                            break;
                        case TransitionType.To:
                            StartPosition = Owner.localPosition;
                            EndPosition = MoveValue;
                            break;
                    }

                    break;
                case TargetValueType.OffsetFromStart:
                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            EndPosition = Owner.localPosition;
                            StartPosition = EndPosition + MoveValue;
                            break;
                        case TransitionType.To:
                            StartPosition = Owner.localPosition;
                            EndPosition = StartPosition + MoveValue;
                            break;
                    }

                    break;
            }
        }

        public override void SetupStart()
        {
            base.SetupStart();
            Owner.localPosition = StartPosition;
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

            MainTween = Owner.DOLocalMove(EndPosition, Duration).SetEase(Ease).SetDelay(Delay);
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
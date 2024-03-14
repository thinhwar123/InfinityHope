using DG.Tweening;
using UnityEngine;

namespace HI.UI.Transitions
{
    public class LocalMoveOneDirectionTweenTransition : TweenTransition
    {
        public enum TweenConfig
        {
            X,
            Y,
            Z,
        }

        [field: SerializeField] public Transform Owner { get; set; }
        [field: SerializeField] public TweenConfig Config { get; set; }
        [field: SerializeField] public float MoveValue { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Delay { get; set; }
        [field: SerializeField] public Ease Ease { get; set; }

        private Vector3 StartPosition { get; set; }
        private Vector3 EndPosition { get; set; }

        public override void Init()
        {
            base.Init();
            Vector3 TempPosition = Owner.localPosition;
            switch (TargetValueType)
            {
                case TargetValueType.Direct:
                    switch (Config)
                    {
                        case TweenConfig.X:
                            TempPosition = new Vector3(MoveValue, Owner.localPosition.y, Owner.localPosition.z);
                            break;
                        case TweenConfig.Y:
                            TempPosition = new Vector3(Owner.localPosition.x, MoveValue, Owner.localPosition.z);
                            break;
                        case TweenConfig.Z:
                            TempPosition = new Vector3(Owner.localPosition.x, Owner.localPosition.y, MoveValue);
                            break;
                    }

                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            StartPosition = TempPosition;
                            EndPosition = Owner.localPosition;
                            break;
                        case TransitionType.To:
                            StartPosition = Owner.localPosition;
                            EndPosition = TempPosition;
                            break;
                    }

                    break;
                case TargetValueType.OffsetFromStart:
                    switch (Config)
                    {
                        case TweenConfig.X:
                            TempPosition = new Vector3(MoveValue, 0, 0);
                            break;
                        case TweenConfig.Y:
                            TempPosition = new Vector3(0, MoveValue, 0);
                            break;
                        case TweenConfig.Z:
                            TempPosition = new Vector3(0, 0, MoveValue);
                            break;
                    }

                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            EndPosition = Owner.localPosition;
                            StartPosition = EndPosition + TempPosition;
                            break;
                        case TransitionType.To:
                            StartPosition = Owner.localPosition;
                            EndPosition = StartPosition + TempPosition;
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
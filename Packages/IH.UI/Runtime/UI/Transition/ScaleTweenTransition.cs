using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HI.UI.Transitions
{
    public class ScaleTweenTransition : TweenTransition
    {
        [field: SerializeField] public Transform Owner { get; set; }
        [field: SerializeField] public Vector3 ScaleValue { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Delay { get; set; }
        [field: SerializeField] public Ease Ease { get; set; }

        private Vector3 StartScale { get; set; }
        private Vector3 EndScale { get; set; }
        private List<ScrollRect> ScrollRects { get; set; } = new();
        private List<Vector3> StartPosition { get; set; } = new();
        private TweenCallback OnTweenStartCallback { get; set; }
        private TweenCallback OnTweenCompleteCallback { get; set; }

        public override void Init()
        {
            base.Init();
            ScrollRects = Owner.GetComponentsInChildren<ScrollRect>().ToList();
            StartPosition = ScrollRects.Select(s => s.content.localPosition).ToList();
            OnTweenStartCallback = () =>
            {
                for (int i = 0; i < ScrollRects.Count; i++)
                {
                    ScrollRects[i].enabled = false;
                    ScrollRects[i].content.localPosition = StartPosition[i];
                }
            };
            OnTweenCompleteCallback = () => { ScrollRects.ForEach(s => s.enabled = true); };
            switch (TargetValueType)
            {
                case TargetValueType.Direct:
                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            StartScale = ScaleValue;
                            EndScale = Owner.localScale;
                            break;
                        case TransitionType.To:
                            StartScale = Owner.localScale;
                            EndScale = ScaleValue;
                            break;
                    }

                    break;
                case TargetValueType.OffsetFromStart:
                    switch (TransitionType)
                    {
                        case TransitionType.From:
                            EndScale = Owner.localScale;
                            StartScale = EndScale + ScaleValue;
                            break;
                        case TransitionType.To:
                            StartScale = Owner.localScale;
                            EndScale = StartScale + ScaleValue;
                            break;
                    }

                    break;

            }
        }

        public override void SetupStart()
        {
            base.SetupStart();
            Owner.localScale = StartScale;
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

            MainTween = Owner.DOScale(EndScale, Duration).SetEase(Ease).SetDelay(Delay)
                .OnStart(OnTweenStartCallback)
                .OnComplete(OnTweenCompleteCallback);
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

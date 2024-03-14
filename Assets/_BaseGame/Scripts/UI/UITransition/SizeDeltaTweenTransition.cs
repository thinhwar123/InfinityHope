using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static SizeDeltaOneDirectionTweenTransition;

public class SizeDeltaTweenTransition : TweenTransition
{
    [field: SerializeField] public RectTransform Owner { get; private set; }
    [field: SerializeField] public Vector2 Vector2Value { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float Delay { get; private set; }
    [field: SerializeField] public Ease Ease { get; private set; }
    private Vector2 StartSizeDelta { get; set; }
    private Vector2 EndSizeDelta { get; set; }
    public override void Init()
    {
        base.Init();
        switch (TargetValueType)
        {
            case TargetValueType.Direct:
                switch (TransitionType)
                {
                    case TransitionType.From:

                        StartSizeDelta = Vector2Value;
                        EndSizeDelta = Owner.sizeDelta;
                        break;
                    case TransitionType.To:
                        StartSizeDelta = Owner.sizeDelta;
                        EndSizeDelta = Vector2Value;
                        break;
                }
                break;
            case TargetValueType.OffsetFromStart:
                switch (TransitionType)
                {
                    case TransitionType.From:
                        StartSizeDelta = Owner.sizeDelta + Vector2Value;
                        EndSizeDelta = Owner.sizeDelta;
                        break;
                    case TransitionType.To:
                        StartSizeDelta = Owner.sizeDelta;
                        EndSizeDelta = Owner.sizeDelta + Vector2Value;
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

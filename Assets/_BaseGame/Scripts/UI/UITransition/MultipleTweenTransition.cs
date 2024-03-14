using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MultipleTweenTransition : TweenTransition
{
    // [field: SerializeField] public List<TweenTransition> TweenTransitions {get; private set;}
    //
    // public override void Init()
    // {
    //     base.Init();
    //     TweenTransitions.ForEach(t => t.Init());
    // }
    //
    // public override void SetupStart()
    // {
    //     base.SetupStart();
    //     TweenTransitions.ForEach(t => t.SetupStart());
    // }
    // public override Tween Play()
    // {
    //     MainTween = Owner.DOLocalMove(StartPosition, Duration).SetEase(Ease).SetDelay(Delay);
    //     return MainTween;
    // }
    // public override Tween Kill()
    // {
    //
    //     return MainTween;
    // }
}

using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HI.UI.Transitions
{
    [System.Serializable]
    public class TweenTransition : MonoBehaviour
    {
        [field: SerializeField, HorizontalGroup("Attribute"), HideLabel]
        public TargetValueType TargetValueType { get; private set; }

        [field: SerializeField, HorizontalGroup("Attribute"), HideLabel]
        public TransitionType TransitionType { get; private set; }

        [field: SerializeField, HorizontalGroup("Attribute"), HideLabel]
        public TransitionConfig TransitionConfig { get; private set; }

        public Tween MainTween { get; set; }

        public virtual void Init()
        {
        }

        public virtual void SetupStart()
        {
        }

        public virtual Tween Play()
        {
            return MainTween;
        }

        public virtual Tween Kill()
        {
            return MainTween;
        }

        public virtual float GetDuration()
        {
            return 0;
        }

        public virtual float GetDelay()
        {
            return 0;
        }
    }

    public enum TransitionType
    {
        From,
        To,
    }

    public enum TargetValueType
    {
        Direct,
        OffsetFromStart,
    }

    public enum TransitionConfig
    {
        NeedSetupStart,
        Continue,
    }
}
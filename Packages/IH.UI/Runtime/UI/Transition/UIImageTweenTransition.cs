using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.UI;

namespace HI.UI.Transitions
{
    public class UIImageTweenTransition : TweenTransition
    {
        [field: SerializeField] public Image MainImage { get; set; }
        [field: SerializeField] public List<Sprite> SpriteList { get; set; }
        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public float Delay { get; set; }
        private int CurrentIndex { get; set; }
        private float Progress { get; set; }
        private DOGetter<float> DoGetterProgress { get; set; }
        private DOSetter<float> DoSetterProgress { get; set; }
        private TweenCallback ChangeSpriteCallback { get; set; }

        public override void Init()
        {
            base.Init();
            DoGetterProgress = GetProgress;
            DoSetterProgress = SetProgress;
            ChangeSpriteCallback = ChangeSprite;
        }

        public override void SetupStart()
        {
            base.SetupStart();
            CurrentIndex = 0;
            MainImage.sprite = SpriteList[0];
        }

        public override Tween Play()
        {
            Progress = 0;
            MainTween = DOTween.To(DoGetterProgress, DoSetterProgress, 1, Duration)
                .SetDelay(Delay)
                .SetEase(Ease.Linear)
                .OnUpdate(ChangeSpriteCallback);

            return base.Play();
        }

        private float GetProgress()
        {
            return Progress;
        }

        private void SetProgress(float progress)
        {
            Progress = progress;
        }

        private void ChangeSprite()
        {
            CurrentIndex = Mathf.Clamp(Mathf.FloorToInt(Progress * (SpriteList.Count - 1)), 0, SpriteList.Count - 1);
            MainImage.sprite = SpriteList[CurrentIndex];
        }

        public override Tween Kill()
        {
            MainTween?.Kill();
            return base.Kill();
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

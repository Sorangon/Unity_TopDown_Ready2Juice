namespace TopDownShooter.Effects {
    using TopDownShooter.Utility;
    using UnityEngine;
    using DG.Tweening;

    [CreateAssetMenu(fileName = "SC_", menuName = "Top Down Shooter/Effects/Tweening/Screen Shake")]
    public class CameraShake : ScriptableObject {
        #region Settings
        [SerializeField, Min(0f)] private float duration = 0.5f;
        [SerializeField, Min(0f)] private float strength = 1f;
        [SerializeField, Min(0f)] private int vibrato = 10;
        [SerializeField, Min(0f)] private float randomness = 90;
        [SerializeField] private bool snapping = false;
        [SerializeField] private bool fadeOut = true;
        #endregion

        #region Currents
        private Tween currentTween = null;
        #endregion

        #region Tween
        public void Play() {
            if (currentTween != null) {
                currentTween.Complete();
            } else {
                currentTween = MainCameraBuffer.Get().transform.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut).OnComplete(OnComplete);
            }

        }

        private void OnComplete() {
            currentTween = null;
        }
        #endregion
    }
}
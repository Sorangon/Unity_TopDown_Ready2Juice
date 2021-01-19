namespace TopDownShooter.UI {
    using TopDownShooter.Gameplay;
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    public class HealthBarUI : MonoBehaviour {
        #region Settings
        [SerializeField, Range(0f, 1f)] private float maxFillValue = 0.7f;
        [SerializeField] private Gradient colorOverHealth = new Gradient();
        [SerializeField] private Health targetHealth = null;
        [SerializeField] private Image fillBar = null;
        [SerializeField] private Image backgroundBar = null;
        #endregion

        #region Callbacks
        private void OnEnable() {
            OnUpdate();

            backgroundBar.fillAmount = maxFillValue;
            backgroundBar.transform.localRotation = GetRotationOffset(maxFillValue);
        }
        #endregion

        #region Behaviour
        public void OnUpdate() {
            float heatlhPercentage = targetHealth.CurrentHealthRatio;
            float fillValue = Mathf.Lerp(0f, maxFillValue, heatlhPercentage);

            fillBar.fillAmount = fillValue;
            fillBar.color = colorOverHealth.Evaluate(heatlhPercentage);
            fillBar.transform.localRotation = GetRotationOffset(fillValue);
        }

        private Quaternion GetRotationOffset(float fillValue) {
            return Quaternion.AngleAxis(fillValue * -180f - 90f, Vector3.forward);
        }
        #endregion
    }
}
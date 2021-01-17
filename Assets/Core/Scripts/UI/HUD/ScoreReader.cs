namespace TopDownShooter.UI {
    using TopDownShooter.Gameplay;
    using UnityEngine;
    using TMPro;

    /// <summary>
    /// Displays the score on a Text Mesh
    /// </summary>
    public class ScoreReader : MonoBehaviour {
        #region References
        [SerializeField] private TextMeshProUGUI textMesh = null;
        #endregion

        #region Callbacks
        private void OnEnable() {
            GameManager.Instance.Score.onUpdateScore += OnUpdateScore;
            textMesh.text = GameManager.Instance.Score.CurrentScore.ToString();
        }


        private void OnDisable() {
            GameManager.Instance.Score.onUpdateScore -= OnUpdateScore;
        }
        #endregion

        #region Event Callbacks
        private void OnUpdateScore(int score) {
            textMesh.text = score.ToString();
        }
        #endregion
    }
}
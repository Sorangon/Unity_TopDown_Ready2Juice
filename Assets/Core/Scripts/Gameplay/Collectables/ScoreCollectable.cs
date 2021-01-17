namespace TopDownShooter.Gameplay {
    using UnityEngine;

    public class ScoreCollectable : Collectable {
        #region Settings
        [Header("Score Settings")]
        [SerializeField, Min(0)] private int scoreBonus = 1000;
        #endregion

        #region Collect
        protected override void OnCollect(GameObject collector) {
            GameManager.Instance.Score.AddPoints(scoreBonus);
        } 
        #endregion
    }
}
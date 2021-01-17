namespace TopDownShooter.Gameplay {
    using UnityEngine;

    public class ScoreManager {
        #region Currents
        private int currentScore = 0;
        #endregion

        #region Properties
        public int CurrentScore => currentScore;
        #endregion

        #region Events
        public delegate void ScoreAction(int score);
        public event ScoreAction onUpdateScore;
        #endregion

        #region Manage Score
        public void AddPoints(int points) {
            if(points < 0) {
                return;
            }

            currentScore += points;
            onUpdateScore?.Invoke(currentScore);
        }
        #endregion
    }
}
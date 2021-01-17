namespace TopDownShooter.Gameplay {
    using TopDownShooter.Utility;

    public class GameManager : SingletonMonoBehaviour<GameManager> {
        #region Currents
        private ScoreManager scoreManager;
        #endregion

        #region Properties
        public ScoreManager Score => scoreManager;
        #endregion

        #region Callbacks
        protected override void Awake() {
            base.Awake();
            scoreManager = new ScoreManager();
        } 
        #endregion
    }
}
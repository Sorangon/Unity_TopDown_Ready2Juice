namespace TopDownShooter.Utility {
    using UnityEngine;

    /// <summary>
    /// A timer that trigger a callback when elapsed
    /// Can be paused
    /// Update by the Timer Handle
    /// </summary>
    public class Timer {
        #region Properties
        public float Duration => duration;
        #endregion

        #region Current
        public bool isPlaying = true;
        public float elapsedTime = 0f;
        private float duration = 1f;
        private System.Action callback = null;
        #endregion

        #region Constructors
        public Timer(float duration, System.Action completeCallback) {
            this.duration = duration;
            callback = completeCallback;
        }
        #endregion

        #region Behaviour
        public void Play() {
            elapsedTime = 0f;
            TimerHandle.RegisterTimer(this);
        }

        internal void Update() {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > duration) {
                Stop();
            }
        }

        public void Reset() {
            elapsedTime = 0f;
        }

        public void Cancel() {
            TimerHandle.UnregisterTimer(this);
        }

        public void Stop() {
            callback.Invoke();
            TimerHandle.UnregisterTimer(this);
        }
        #endregion
    }
}
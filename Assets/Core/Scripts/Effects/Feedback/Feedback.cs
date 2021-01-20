namespace TopDownShooter.Effects {
    using UnityEngine;
    using TopDownShooter.Utility;

    public class Feedback : MonoBehaviour {
        #region References
        [SerializeField] internal AudioSource audioSource = null;
        #endregion

        #region Currents
        internal new ParticleSystem particleSystem = null;
        private FeedbackAsset owner = null;
        private Timer timer = null;
        #endregion

        #region Behaviour
        internal void Setup(FeedbackAsset feedback) {
            owner = feedback;
            if (feedback.ParticleSystem) {
                particleSystem = Instantiate(feedback.ParticleSystem,transform);
                particleSystem.transform.localPosition = Vector3.zero;
            }
            audioSource.clip = feedback.SoundEffect;
            audioSource.spatialBlend = feedback.SpatialBlend;

            float duration = Mathf.Max(owner.SoundEffect.length, owner.ParticleSystem.main.duration) + 0.1f;
            timer = new Timer(duration, OnComplete);
        }

        internal void Play() {
            if(particleSystem != null) {
                particleSystem.Play();
            }
            audioSource.pitch = Random.Range(owner.PitchRange.x, owner.PitchRange.y);
            audioSource.volume = Random.Range(owner.VolumeRange.x, owner.VolumeRange.y);
            audioSource.Play();
            timer.Play();
        }

        internal void OnComplete() {
            owner.pool.Add(this);
        }
        #endregion
    }

}
namespace TopDownShooter.Utility {
    using UnityEngine;
    using UnityEngine.Events;

    public class OnStart : MonoBehaviour {
        [SerializeField] private UnityEvent onStart = new UnityEvent();

        void Start() {
            onStart?.Invoke();
        } 
    }
}
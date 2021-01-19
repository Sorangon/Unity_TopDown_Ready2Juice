namespace TopDownShooter.Utility {
    using UnityEngine;

    public class SetParent : MonoBehaviour {
        #region References
        public Transform targetParent = null;
        #endregion

        #region Behaviour
        public void Execute() {
            transform.parent = targetParent;
        }
        #endregion
    }
}
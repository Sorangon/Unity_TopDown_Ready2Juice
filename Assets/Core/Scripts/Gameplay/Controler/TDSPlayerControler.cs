namespace TopDownShooter.Gameplay {
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Base player controler for Top Down Shooter
    /// </summary>
    public class TDSPlayerControler : MonoBehaviour {
        #region Settings
        [SerializeField] private TDSCharacterMovements targetCharacter = null;
        [SerializeField] private Weapon currentWeapon = null;
        #endregion

        #region Current
        private TopDownInputs inputs = null;
        #endregion

        #region Callbacks
        private void Awake() {
            inputs = new TopDownInputs();
        }

        private void OnEnable() {
            inputs.Enable();
        }

        private void OnDisable() {
            inputs.Disable();
        }

        private void Update() {
            SetMovementDirection();
        }
        #endregion

        #region Sample Inputs
        private void SetMovementDirection() {
            Vector2 inputDirection = inputs.Player.Move.ReadValue<Vector2>();
            Vector2 cameraRelativeDir = Quaternion.AngleAxis(MainCameraBuffer.Get().transform.eulerAngles.y - 90f, Vector3.forward) * inputDirection;
            targetCharacter.MovementDirection = cameraRelativeDir;
        }
        #endregion
    }
}
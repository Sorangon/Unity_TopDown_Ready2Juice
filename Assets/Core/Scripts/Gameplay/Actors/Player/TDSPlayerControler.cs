﻿namespace TopDownShooter.Gameplay {
    using TopDownShooter.Utility;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Base player controler for Top Down Shooter
    /// </summary>
    public class TDSPlayerControler : TDSControler {
        #region Settings
        [Header("References")]
        [SerializeField] private TDSCharacterMovements targetCharacter = null;
        [SerializeField] private Weapon currentWeapon = null;
        [SerializeField] private Health healthSystem = null;
        [SerializeField] private WeaponSelector weaponSelector = null;

        [Header("Mouse")]
        [SerializeField] private LayerMask mouseRaycastMask = new LayerMask();
        [SerializeField] private float maxDistanceRadius = 60f;
        #endregion

        #region Properties
        public TDSCharacterMovements CharacterMovements => targetCharacter;
        public static TDSPlayerControler CurrentControlerInstance => currentControlerInstance;
        public Health HealthSystem => healthSystem;
        #endregion

        #region Current
        private static TDSPlayerControler currentControlerInstance = null;
        private TopDownInputs inputs = null;
        #endregion

        #region Callbacks
        private void Awake() {
            inputs = new TopDownInputs();
            currentWeapon.BindToControler(this);
        }

        private void OnEnable() {
            inputs.Enable();
            inputs.Player.Fire.started += OnFireStart;
            inputs.Player.Fire.canceled += OnFireCanceled;
            inputs.Player.SwitchWeapon.started += OnSwitchWeaponStart;
            currentControlerInstance = this;
        }


        private void OnDisable() {
            inputs.Player.Fire.started -= OnFireStart;
            inputs.Player.Fire.canceled -= OnFireCanceled;
            inputs.Player.SwitchWeapon.started -= OnSwitchWeaponStart;
            inputs.Disable();
        }

        private void Update() {
            SetMovementDirection();
            UpdateAimDirection();
        }
        #endregion

        #region Sample Inputs
        private void SetMovementDirection() {
            Vector2 inputDirection = inputs.Player.Move.ReadValue<Vector2>();
            Vector2 cameraRelativeDir = Quaternion.AngleAxis(MainCameraBuffer.Get().transform.eulerAngles.y - 90f, Vector3.forward) * inputDirection;
            targetCharacter.MovementDirection = cameraRelativeDir;
        }

        private void UpdateAimDirection() {
            Vector2 lookInputValue = inputs.Player.Look.ReadValue<Vector2>();

            InputControl control = inputs.Player.Look.activeControl;
            if (control != null) {
                if(control.device is Mouse) {
                    Ray ray = MainCameraBuffer.Get().ScreenPointToRay(lookInputValue);
                    if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mouseRaycastMask)) {
                        aimVector = hit.point - transform.position;
                        aimVector.y = 0f;
                        float distance = Mathf.Min(aimVector.magnitude / maxDistanceRadius, 1f);
                        aimVector = aimVector.normalized * distance;
                    } else {
                        aimVector = Vector3.zero;
                    }

                } else if (control.device is Gamepad) {
                    Vector2 cameraRelativeAim = Quaternion.AngleAxis(MainCameraBuffer.Get().transform.eulerAngles.y - 90f, Vector3.forward) * lookInputValue;
                    aimVector = new Vector3(cameraRelativeAim.x, 0f, cameraRelativeAim.y);
                } else {
                    aimVector = Vector3.zero;
                }
            } else {
                aimVector = Vector3.zero;
            }
#if UNITY_EDITOR
            Debug.DrawRay(transform.position, aimVector * 5f, Color.red); 
#endif
        }
        #endregion

        #region Buttons Callbacks
        private void OnFireStart(InputAction.CallbackContext context) {
            if(!ReferenceEquals(currentWeapon, null)) {
                currentWeapon.Trigger();
            }
        }

        private void OnFireCanceled(InputAction.CallbackContext context) {
            if (!ReferenceEquals(currentWeapon, null)) {
                currentWeapon.Release();
            }
        }
        
        private void OnSwitchWeaponStart(InputAction.CallbackContext context) {
            bool wasHolding = false;

            if(!ReferenceEquals(currentWeapon, null)) {
                wasHolding |= (currentWeapon.Holding && currentWeapon.ContinuousAttack);
            }

            weaponSelector.SwitchWeapon();
            if (wasHolding) {
                currentWeapon.Trigger();
            }
        }
        #endregion

        #region Weapon
        /// <summary>
        /// Register the current new weapon
        /// </summary>
        public void SetWeapon(Weapon weapon) {
            currentWeapon = weapon;
        }
        #endregion
    }
}
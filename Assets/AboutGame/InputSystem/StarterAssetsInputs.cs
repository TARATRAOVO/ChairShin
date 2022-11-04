using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool fire;
        public bool pick;
        public bool duplicate;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        public Actions PlayerActions;
        public Duplicate Duplicator;

        private void Start()
        {
            PlayerActions = GameObject.FindGameObjectWithTag("Player").GetComponent<Actions>();
            Duplicator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Duplicate>();
        }


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        // public void OnLook(InputValue value)
        // {
        // 	if(cursorInputForLook)
        // 	{
        // 		LookInput(value.Get<Vector2>());
        // 	}
        // }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnFire(InputValue value)
        {
            FireInput(value.isPressed);
        }
        public void OnDuplicate(InputValue value)
        {
            if (duplicate == false)
            {
                Duplicator.DoDuplicate();

            }
            DuplicateInput(value.isPressed);
        }
        public void OnPick(InputValue value)
        {
            if (pick == false)
            {
                PlayerActions.Pick();
            }
            PickInput(value.isPressed);
        }
#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void FireInput(bool newFireState)
        {
            fire = newFireState;
        }
        public void DuplicateInput(bool newDuplicateState)
        {
            duplicate = newDuplicateState;
        }
        public void PickInput(bool newPickState)
        {
            pick = newPickState;
        }

        // private void OnApplicationFocus(bool hasFocus)
        // {
        // 	SetCursorState(cursorLocked);
        // }

        // private void SetCursorState(bool newState)
        // {
        // 	Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        // }
    }

}
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeManager : MonoBehaviour
{
    public Animator AnimAxe;

    public bool ActiveAxe;


    public PlayerMovement movementPlayer;

    public InputActionReference Butt_ActiveAxe;

    private void OnEnable()
    {
        Butt_ActiveAxe.action.Enable();
        Butt_ActiveAxe.action.performed += ToggleAxe;
    }

    private void OnDisable()
    {
        Butt_ActiveAxe.action.Disable();
        Butt_ActiveAxe.action.performed -= ToggleAxe;
    }

    private void ToggleAxe(InputAction.CallbackContext context)
    {
        ActiveAxe = true;
        AnimAxe.SetBool("ActiveAxe", ActiveAxe);
        StartCoroutine(AnimDisactiveAxe());

        if (ActiveAxe)
        {
            switch (movementPlayer.direction)
            {
                case PlayerMovement.Direction.right:
                    movementPlayer.animatorPlayer.SetBool("ActiveAxeRight", ActiveAxe);
                    break;
                case PlayerMovement.Direction.left:
                    movementPlayer.animatorPlayer.SetBool("ActiveAxeLeft", ActiveAxe);
                    break;
            }
        }
    }

    IEnumerator AnimDisactiveAxe()
    {
        yield return new WaitForSeconds(0.8f);
        ActiveAxe = false;
        AnimAxe.SetBool("ActiveAxe", ActiveAxe);
    }
}

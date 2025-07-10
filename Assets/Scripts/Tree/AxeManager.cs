using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeManager : MonoBehaviour
{
    [SerializeField] private Animator AnimAxe;
    [SerializeField] private bool ActiveAxe;

    [Header("Ui Axe")]
    [SerializeField] private GameObject ButtAxe;

    public Tree CurrentTree;
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
        CurrentTree.TreeHit();
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
        //movementPlayer.animatorPlayer.SetBool("ActiveAxeRight", ActiveAxe);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tree"))
        {
            CurrentTree = collision.gameObject.GetComponent<Tree>();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tree"))
        {
            CurrentTree = null;
        }
    }
}

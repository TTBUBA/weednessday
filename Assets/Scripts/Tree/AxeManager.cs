using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeManager : MonoBehaviour
{
    [SerializeField] private Animator AnimAxe;
    [SerializeField] private bool AxeIsActive;

    [Header("Ui Axe")]
    [SerializeField] private GameObject ButtAxe;

    public Tree CurrentTree;
    public PlayerMovement movementPlayer;
    public InventoryManager InventoryManager;
    public InputActionReference Butt_ActiveAxe;

    public void Update()
    {
        ActiveAxe();
        switch (movementPlayer.direction)
        {
            case PlayerMovement.Direction.right:
                this.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0f);
                break;

            case PlayerMovement.Direction.left:
                this.gameObject.transform.localScale = new Vector3(-0.7f, 0.7f, 0f);
                break;
        }
    }
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
    
    private void ActiveAxe()
    {
        if(InventoryManager.CurrentSlotSelect != null && InventoryManager.CurrentSlotSelect.NameTools == "Axe")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if(InventoryManager.CurrentSlotSelect == null || InventoryManager.CurrentSlotSelect.NameTools != "Axe")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void ToggleAxe(InputAction.CallbackContext context)
    {
        AxeIsActive = true;
        AnimAxe.SetBool("ActiveAxe", AxeIsActive);
        StartCoroutine(AnimDisactiveAxe());
        if(CurrentTree != null)
        {
            CurrentTree.TreeHit();
        }
        if (AxeIsActive && CurrentTree != null)
        {
            switch (movementPlayer.direction)
            {
                case PlayerMovement.Direction.right:
                    movementPlayer.animatorPlayer.SetBool("ActiveAxeRight", true);
                    break;
                case PlayerMovement.Direction.left:
                    movementPlayer.animatorPlayer.SetBool("ActiveAxeLeft", true);
                    break;
            }
        }
    }

    IEnumerator AnimDisactiveAxe()
    {
        yield return new WaitForSeconds(0.8f);
        AxeIsActive = false;
        AnimAxe.SetBool("ActiveAxe", AxeIsActive);
        switch (movementPlayer.direction)
        {
            case PlayerMovement.Direction.right:
                movementPlayer.animatorPlayer.SetBool("ActiveAxeRight", false);
                break;
            case PlayerMovement.Direction.left:
                movementPlayer.animatorPlayer.SetBool("ActiveAxeLeft", false);
                break;
        }
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

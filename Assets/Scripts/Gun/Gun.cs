using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [Header("Gun Settings")]
    public GunData currentGun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private bool ActiveShoot;
    [SerializeField] private AudioSource soundShoot;

    [Header("Ui")]
    [SerializeField] private SpriteRenderer gunIcon;

    public InventoryManager InventoryManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Update()
    {
        if ((InventoryManager.CurrentSlotSelect != null && InventoryManager.CurrentSlotSelect.NameTools == "gun"))
        {
            gunIcon.enabled = true;
            gunIcon.sprite = currentGun.icon;
        }
        else
        {
            gunIcon.enabled = false;
        }
    }

    public void Shoot()
    {
        if (currentGun != null && ActiveShoot)
        {
            Debug.Log("Shooting with " + currentGun.NameGun);
            animator.SetTrigger(currentGun.NameGun);

            if (currentGun.soundShoot is AudioClip audioClip)
            {
                soundShoot.PlayOneShot(audioClip);
            }

            StartCoroutine(ResetIdle());
            ActiveShoot = false;
        }
    }

    IEnumerator ResetIdle()
    {
        yield return new WaitForSeconds(currentGun.ShootAnimation.length);
        animator.SetTrigger("Idle");
        ActiveShoot = true;
    }
}

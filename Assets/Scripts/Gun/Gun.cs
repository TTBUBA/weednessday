using System.Collections;
using TMPro;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [Header("Gun Settings")]
    public GunData currentGun;
    [SerializeField] private Transform gun;
    [SerializeField] private Animator animator;
    [SerializeField] private bool ActiveShoot;
    [SerializeField] private bool Iscollision;
    [SerializeField] private AudioSource soundShoot;
    [SerializeField] private Transform PointSpawnBullet;
    [SerializeField] private GameObject blood;
    [SerializeField] private float snappedAngle;
    [SerializeField] private int CurrentAmmo;
    [SerializeField] private int CurrentAmmoloader;


    [Header("Ui")]
    [SerializeField] private SpriteRenderer gunIcon;
    [SerializeField] private TextMeshProUGUI textAmmo;

    private RaycastHit2D hit;
    public InventoryManager InventoryManager;
    public PlayerMovement playerMovement;
    public MouseManager mouseManager;
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
            TrackerMouse();
            textAmmo.gameObject.SetActive(true);
            textAmmo.text = $"{CurrentAmmoloader}/{CurrentAmmo}";
        }
        else
        {
            gunIcon.enabled = false;
            textAmmo.gameObject.SetActive(false);
        }
    }

    public void Shoot()
    {
        if (currentGun != null && ActiveShoot && CurrentAmmoloader >= 1)
        {
            Raycast();
            animator.SetTrigger(currentGun.NameGun);
            CurrentAmmoloader--;
            if (currentGun.soundShoot is AudioClip audioClip)
            {
                soundShoot.PlayOneShot(audioClip);
            }
            if (Iscollision)
            {
                GameObject bloodEffect = Instantiate(blood, hit.point, Quaternion.identity);
                bloodEffect.transform.parent = hit.transform;
                Animator bloodAnimator = bloodEffect.GetComponent<Animator>();
                bloodAnimator.SetBool("Active", true);
                Destroy(bloodEffect, 1f);
            }
            StartCoroutine(ResetIdle());
            ActiveShoot = false;
            if (CurrentAmmoloader <= 0)
            {
                CurrentAmmoloader = 0;
                StartCoroutine(ReloadGun());
            }
        }
    }

    private void Raycast()
    {
        //convert the snapped angle to radians
        float rad = snappedAngle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        hit = Physics2D.Raycast(PointSpawnBullet.position, direction, currentGun.MaxRange, LayerMask.GetMask("Enemy"));

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Iscollision = true;
            Debug.DrawRay(PointSpawnBullet.position, direction * currentGun.MaxRange, Color.green, 0.1f);
        }
        else
        {
            Iscollision = false;
            Debug.DrawRay(PointSpawnBullet.position, direction * currentGun.MaxRange, Color.red, 0.1f);
        }
    }
    private void TrackerMouse()
    {

        Vector3 MousePos = mouseManager.MousePos;
        Vector3 direction = (MousePos - gun.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(angle < 0)
        {
            angle += 360;
        }

        int sector = Mathf.RoundToInt(angle / 45f) % 8;

        snappedAngle = sector * 45f;

        gun.eulerAngles = new Vector3(0, 0, snappedAngle);

        // Flip the gun
        if (snappedAngle == 135 || snappedAngle == 225 || snappedAngle == -180 || snappedAngle == 180)
        {
            gun.transform.localScale = new Vector3(1, -1, 1); // flip vertically
        }
        else
        {
            gun.transform.localScale = new Vector3(1, 1, 1); // normal orientation
        }
    }
    IEnumerator ResetIdle()
    {
        yield return new WaitForSeconds(currentGun.ShootAnimation.length);
        animator.SetTrigger("Idle");
        ActiveShoot = true;
    }

    IEnumerator ReloadGun()
    {
        while (true)
        {
            if (CurrentAmmo <= 0 || CurrentAmmoloader >= currentGun.MaxAmmo)
            {
                yield break; // Exit if no ammo or loader is full
            }
            yield return new WaitForSeconds(currentGun.ReloadTime);
            CurrentAmmoloader++;
            CurrentAmmo--;
        }
    }

}

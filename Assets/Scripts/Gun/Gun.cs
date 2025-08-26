using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [Header("Gun Settings")]
    public GunData currentGun;
    public GunData previousGun;
    [SerializeField] private Transform gun;
    [SerializeField] public Animator animator;
    [SerializeField] private bool ActiveShoot;
    [SerializeField] private bool Iscollision;
    [SerializeField] private AudioSource soundShoot;
    [SerializeField] private Transform PointSpawnBullet;
    [SerializeField] private GameObject blood;
    [SerializeField] private float snappedAngle;
    [SerializeField] public int CurrentAmmo;
    [SerializeField] public int CurrentAmmoloader;


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

            if (currentGun.NameGun == "glock")
            {
                animator.SetInteger("GunType", 1); // GlockIdle
            }
            else if (currentGun.NameGun == "shootgun")
            {
                animator.SetInteger("GunType", 2); // ShotGunIdle
            }

            TrackerMouse();
            textAmmo.gameObject.SetActive(true);
            int totalAmmo = GetTotalAmmoInInventory();
            textAmmo.text = $"{CurrentAmmoloader}/{totalAmmo}";
            previousGun = currentGun; 

        }
        else
        {
            gunIcon.enabled = false;
            textAmmo.gameObject.SetActive(false);
            animator.SetInteger("GunType", 0); 
            previousGun = null;
        }
    }

    public int GetTotalAmmoInInventory()
    {
        CurrentAmmo = 0;
        foreach (var ammo in InventoryManager.slootManager)
        {
            if (ammo.slootData != null && ammo.slootData.NameTools == currentGun.NameAmmo)
            {
                CurrentAmmo += ammo.CurrentStorage;
                ammo.UpdateSlot();
            }
        }
        return CurrentAmmo;
    }

    public void RemoveAmmoFromInventory(int total)
    {
        foreach (var ammo in InventoryManager.slootManager)
        {
            if (ammo.slootData != null && ammo.slootData.NameTools == currentGun.NameAmmo && ammo.CurrentStorage > 0)
            {
                int toRemove = Mathf.Min(total, ammo.CurrentStorage);
                ammo.CurrentStorage -= toRemove;
                ammo.UpdateSlot();
                total -= toRemove;
                if (total <= 0) break;
                if(ammo.CurrentStorage <= 0)
                {
                    ammo.slootData = null;
                    ammo.UpdateSlot();
                }
            }
        }

    }
    public void Shoot()
    {
        if (currentGun != null && ActiveShoot && CurrentAmmoloader >= 1)
        {
            Raycast();
            animator.SetTrigger("Shoot"); 
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
                int randomDamage = Random.Range(10, 20);
                PoliceHealth policeHealth = hit.transform.GetComponent<PoliceHealth>();
                policeHealth.TakeDamage(randomDamage);
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

        if (hit.collider != null  && hit.collider.CompareTag("Enemy") && !hit.collider.isTrigger)
        {
            Iscollision = true;
        }
        else
        {
            Iscollision = false;
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
        animator.SetTrigger("Shoot");
        ActiveShoot = true;
    }

    IEnumerator ReloadGun()
    {
        while (true)
        {
            CurrentAmmo = GetTotalAmmoInInventory();

            if (CurrentAmmo <= 0 || CurrentAmmoloader >= currentGun.MaxAmmo)
            {
                yield break; // Exit if no ammo or loader is full
            }
            yield return new WaitForSeconds(currentGun.ReloadTime);

            int ammoNeeded = currentGun.MaxAmmo - CurrentAmmoloader;

            if(CurrentAmmo >= ammoNeeded)
            {
                CurrentAmmoloader += ammoNeeded;
                RemoveAmmoFromInventory(ammoNeeded);
            }
            else
            {
                CurrentAmmoloader += CurrentAmmo;
                RemoveAmmoFromInventory(CurrentAmmo);
            }
        }
    }

}

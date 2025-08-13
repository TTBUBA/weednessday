using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceGun : MonoBehaviour
{
    [SerializeField] private GameObject Arm;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Ammo;
    [SerializeField] private Transform ContainerAmmo;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private AudioSource SoundShoot;
    [SerializeField] private List<GameObject> AmmoList = new List<GameObject>();
    [SerializeField] private int AmmoCount = 30;
    public float TimeFire;
    public bool EnableGun;


    public PoliceAi PoliceAi;
    public PlayerHealth PlayerHealth;
    private void Start()
    {
        SoundShoot.Stop();
        for (int i = 0; i < AmmoCount; i++)
        {
            GameObject ammoInstance = Instantiate(Ammo, ContainerAmmo);
            ammoInstance.SetActive(false);
            AmmoList.Add(ammoInstance);
        }
    }
    // Update is called once per frame  
    void Update()
    {
        GunTrackPlayer();
    }

    //Shoot bullet in the position of the player
    public void Shoot()
    {
        if (AmmoCount <= 0) { return; }
        SoundShoot.Play();

        GameObject Ammo = GetAmmo();

        Ammo.SetActive(true);
        float randomAngle = Random.Range(-10f, 10f);
        Ammo.transform.eulerAngles = new Vector3(0, 0, 90f + randomAngle);
        Ammo.transform.SetParent(ContainerAmmo);

        Rigidbody2D rb = Ammo.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.transform.up * 1f, ForceMode2D.Impulse);
        float randomDrag = Random.Range(0.3f, 1f);
        rb.linearDamping = randomDrag;
        StartCoroutine(DestroyAmmo(Ammo));
        AmmoCount--;
        PlayerHealth.DecreseHealth(5);
    }

    //return the first inactive ammo from the list
    private GameObject GetAmmo()
    {
        for(int i = 0; i < AmmoList.Count; i++)
        {
            if(!AmmoList[i].activeInHierarchy)
            {
                return AmmoList[i];
            }
        }
        return null;
    }
    //Reload Gun
    IEnumerator Reload()
    {
        while (AmmoCount <= 30)
        {
            yield return new WaitForSeconds(0.5f);
            AmmoCount++;
        }
    }

    IEnumerator DestroyAmmo(GameObject ammo)
    {
        yield return new WaitForSeconds(4f);
        ammo.SetActive(false);
        ammo.transform.position = transform.position;
        ammo.transform.eulerAngles = Vector3.zero;
        Debug.Log(ammo.transform.eulerAngles = Vector3.zero);
        Rigidbody2D rb = ammo.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    //Active Shoot Coroutine
    public IEnumerator ActiveShoot()
    {
        while (EnableGun)
        {
            Shoot();
            yield return new WaitForSeconds(TimeFire);
            if (AmmoCount <= 0)
            {
                yield return StartCoroutine(Reload());
            }
        }
    }

    //Track Position of player
    private void GunTrackPlayer()
    {
        // Get the target position 
        Vector3 PosTarget = new Vector2(targetPosition.position.x, targetPosition.position.y);

        // Calculate the normalized direction vector from the gun to the target
        Vector3 direction = (PosTarget - Gun.transform.position).normalized;

        // Convert the direction vector into an angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ensure the angle is positive (0 to 360 degrees)
        if (angle < 0) angle += 360f;

        // Divide the 360° circle into 8 equal sectors (45° each) and round to the nearest sector
        int sector = Mathf.RoundToInt(angle / 45f) % 8;

        // Snap the angle to the nearest 45° sector
        float snappedAngle = sector * 45f;

        // Rotate the arm to face the snapped angle
        Arm.transform.eulerAngles = new Vector3(0, 0, snappedAngle);

        // Flip the gun
        if (snappedAngle == 135 || snappedAngle == 225 || snappedAngle == -180 || snappedAngle == 180)
        {
            Gun.transform.localScale = new Vector3(1, -1, 1); // flip vertically
        }
        else
        {
            Gun.transform.localScale = new Vector3(1, 1, 1); // normal orientation
        }
    }

}
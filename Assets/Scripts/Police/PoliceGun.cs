using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class PoliceGun : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Ammo;
    [SerializeField] private Transform ContainerAmmo;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private AudioSource SoundShoot;
    [SerializeField] private List<Vector3> PosTarget;
    [SerializeField] private int AmmoCount = 30;
    public float TimeFire;
    public bool EnableGun;


    public PoliceMovement policeMovement;

    // Update is called once per frame  
    void Update()
    {
        GunTrackPlayer();
    }

    //Shoot bullet in the position of the player
    public void Shoot()
    {
        if (AmmoCount <= 0) { return; }
        GameObject Obj = Instantiate(Ammo, Gun.transform.position, Quaternion.identity);
        SoundShoot.Play();
        float randomAngle = Random.Range(-10f, 10f);
        Obj.transform.eulerAngles = new Vector3(0, 0, 90f + randomAngle);
        Obj.transform.SetParent(ContainerAmmo);
        Rigidbody2D rb = Obj.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.transform.up * 1f, ForceMode2D.Impulse);
        float randomDrag = Random.Range(0.3f, 1f);
        rb.linearDamping = randomDrag;
        StartCoroutine(DestroyAmmo());
        PosTarget.Add(targetPosition.position);
        AmmoCount--;
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

    IEnumerator DestroyAmmo()
    {
        yield return new WaitForSeconds(5f);
        foreach (Transform child in ContainerAmmo)
        {
            Destroy(child.gameObject);
        }
        PosTarget.Clear();
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
        Vector3 PosTarget = new Vector2(targetPosition.position.x, targetPosition.position.y);
        Vector3 direction = (PosTarget - Gun.transform.position).normalized;
        //convert the direction to an angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Gun.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    //DEBUG//
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Vector3 pos in PosTarget)
        {
            Gizmos.DrawLine(Gun.transform.position, pos);
        }
    }
}

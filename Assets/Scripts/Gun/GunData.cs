using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public SlootData SlootData;
    public string NameGun;
    public string NameAmmo;
    public Sprite icon;
    public float MaxRange;
    public int MaxAmmo;
    public int currentAmmo;
    public float ReloadTime;
    public float FireRate;
    public AnimationClip ShootAnimation;
    public AudioResource soundShoot;
}

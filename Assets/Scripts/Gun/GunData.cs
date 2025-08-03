using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public SlootData SlootData;
    public string NameGun;
    public Sprite icon;
    public int MaxAmmo;
    public int currentAmmo;
}

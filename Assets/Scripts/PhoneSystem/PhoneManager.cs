using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] private List<PhoneManager> phones = new List<PhoneManager>();

    public AppManager CurrentAppSelect;

}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PhoneManager : MonoBehaviour
{
    [SerializeField] private List<PhoneManager> phones = new List<PhoneManager>();

    public AppManager CurrentAppSelect;

    [SerializeField] private GameObject ContainerPhone;



    public void OpenPhone()
    {
        ContainerPhone.SetActive(true);
    }

    public void ClosePhone()
    {
        ContainerPhone.SetActive(false);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private InputActionReference Butt_doubleSpeedWord;
    [SerializeField] private InputActionReference Butt_ReduceSpeedWord;
    [SerializeField] private TextMeshProUGUI Text_Timescale;

    private void Awake()
    {
        Text_Timescale.text = "Timescale: " + Time.timeScale.ToString("F1");
    }
    private void OnEnable()
    {
        Butt_doubleSpeedWord.action.Enable();
        Butt_ReduceSpeedWord.action.Enable();
        Butt_doubleSpeedWord.action.performed += DoubleSpeedWord;
        Butt_ReduceSpeedWord.action.performed += ReduceSpeedWord;
    }
    private void OnDisable()
    {
        Butt_doubleSpeedWord.action.Disable();       
        Butt_ReduceSpeedWord.action.Disable();
        Butt_doubleSpeedWord.action.performed -= DoubleSpeedWord;
        Butt_ReduceSpeedWord.action.performed -= ReduceSpeedWord;
    }
    private void DoubleSpeedWord(InputAction.CallbackContext context)
    {
        Time.timeScale += 0.2f;
        Text_Timescale.text = "Timescale: " + Time.timeScale.ToString("F1");
    }

    private void ReduceSpeedWord(InputAction.CallbackContext context)
    {
        Time.timeScale -= 0.2f;
        Text_Timescale.text = "Timescale: " + Time.timeScale.ToString("F1");
    }

}

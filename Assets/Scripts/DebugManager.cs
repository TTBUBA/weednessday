using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Threading;
using System.Diagnostics;
using System.Linq;


public class DebugManager : MonoBehaviour
{
    [Header("Debug Manager")]
    [SerializeField] private InputActionReference Butt_doubleSpeedWord;
    [SerializeField] private InputActionReference Butt_ReduceSpeedWord;
    [SerializeField] private TextMeshProUGUI Text_Timescale;
    [SerializeField] private TextMeshProUGUI Text_FPS;
    [SerializeField] private TextMeshProUGUI Text_UseCpu;

    //Cpu
    private PerformanceCounter cpuCounter;
    private float LastCpuUsage;
    private float CpuUsage;
    private Thread threadcpu;
    private float updateInterval = 0.5f;

    private void Awake()
    {
        cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        threadcpu = new Thread(UpdateCpuUsage);
        threadcpu.Start();

        Text_Timescale.text = "Timescale: " + Time.timeScale.ToString("F1");
    }
    private void Update()
    {
        currentfps();
        UseCpu();
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

    private void currentfps()
    {
        float currentfps = Time.frameCount / Time.time;
        Text_FPS.text = "FPS: " + Mathf.RoundToInt(currentfps).ToString();
    }

    private void UseCpu()
    {
        Text_UseCpu.text = "CPU Usage: " + CpuUsage.ToString("F1") + "%";
    }

    private void UpdateCpuUsage()
    {
        var lastCpuTime = new TimeSpan(0);
        int processorCount = Environment.ProcessorCount;
        while (true)
        {
            var cpuTime = new TimeSpan(0);
            var allProcesses = Process.GetProcesses();

            cpuTime = allProcesses.Aggregate(cpuTime, (current, process) => current + process.TotalProcessorTime);

            var newCPUTime = cpuTime - lastCpuTime;
            lastCpuTime = cpuTime;

            CpuUsage = 100f * (float)newCPUTime.TotalSeconds / updateInterval / processorCount;

            Thread.Sleep(Mathf.RoundToInt(updateInterval * 1000));
        }
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
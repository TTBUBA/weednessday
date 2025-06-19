using System;

internal class PerformanceCounter
{
    private string v1;
    private string v2;
    private string v3;

    public PerformanceCounter(string v1, string v2, string v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
    }

    public string CategoryName { get; internal set; }

    internal object NextValue()
    {
        throw new NotImplementedException();
    }
}
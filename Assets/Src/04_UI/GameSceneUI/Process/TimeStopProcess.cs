using UnityEngine;

public class TimeStopProcess
{
    public void TimeStop()
    {
        Time.timeScale = 0;
    }
    public void TimeReStart()
    {
        Time.timeScale = 1;
    }
}

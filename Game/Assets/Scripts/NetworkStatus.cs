using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStatus : MonoBehaviour
{
    int datapoints_received = 0;

    public int AddDataPoint()
    {
        return ++datapoints_received;
    }

    public void Reset()
    {
        datapoints_received = 0;
    }
}

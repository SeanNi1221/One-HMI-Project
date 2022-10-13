using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if USE_XCHARTS
using XCharts;
#endif
namespace Sean21.OneHMI.Theme.Default
{
    public class TimelineChart : MonoBehaviour
    {
#if USE_XCHARTS
        public LineChart chart; 
        public float data0ScaleX = 1;
        public float data0ScaleY = 5;
        public float data0BiasX = 1;
        public float data0BiasY = 20;      
        public float data1ScaleX = 1;
        public float data1ScaleY = 4;
        public float data1BiasX = 1;
        public float data1BiasY = 16;  
        public float data2ScaleX = 1.2f;
        public float data2ScaleY = 10;
        public float data2BiasX = 1;
        public float data2BiasY = 30;
        void OnEnable()
        {
            for (int t = -60; t <= 0; t++)
            {
                float tf = (float)t;
                chart.AddData(0, tf, (data0ScaleY * Mathf.Sin(data0ScaleX * tf + data0BiasX) + data0BiasY) );
                chart.AddData(1, tf,(data1ScaleY * Mathf.Sin(data1ScaleX * tf + data1BiasX) + data1BiasY) );
                chart.AddData(2, tf,(data2ScaleY * Mathf.Sin(data2ScaleX * tf + data2BiasX) + data2BiasY) );
            }   
        }
#endif
    }    
}


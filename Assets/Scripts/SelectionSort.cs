using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SelectionSort : MonoBehaviour
{
    int i, j;
    int temp;
    private int[] a = {82, 3, 95, 6, 72, 44, 45, 92, 0, 1};
    Text text;


    // Start is called before the first frame update
    void Start()
    {
        
        for (j = 0; j < a.Length - 1; j++)
        {
            int iMin = j;
            
            for (i = j+1; i < a.Length; i++)
            {
                if (a[i] < a[iMin])
                {
                    iMin = i;
                }
            }
            if (iMin != j)
            {
                temp = a[j];
                a[j] = a[iMin];
                a[iMin] = temp;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(a[].ToString());
    }

    
}

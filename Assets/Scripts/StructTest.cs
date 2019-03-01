using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructTest : MonoBehaviour {

    ClassInts[] cInts;
    StructInts[] sInts;

    const int SIZE = 1000000;

	// Use this for initialization
	void Start () {
        cInts = new ClassInts[SIZE];
        sInts = new StructInts[SIZE];

        for (int i = 0; i < SIZE; i++)
        {
            cInts[i] = new ClassInts();
        }

        // For loop one
        for (int i = 0; i < SIZE; i++)
        {
            RandomiseClass(cInts[i]);
        }

        // For loop two
        for (int i = 0; i < SIZE; i++)
        {
            RandomiseStruct(ref sInts[i]);
        }
    }
	
    void RandomiseStruct(ref StructInts s)
    {
        s.x = Random.Range(0, 100);
    }

    void RandomiseClass(ClassInts c)
    {
        c.x = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update () {
		
	}


}

public class ClassInts
{
    public int x;
}

public struct StructInts
{
    public int x;
}
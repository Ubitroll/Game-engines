using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public GameObject camera;

    private Vector3 startPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        endPos = new Vector3(this.transform.position.x, this.transform.position.y, camera.transform.position.z);
        StartCoroutine(LerpPosition(startPos, endPos, 0.1f));
        startPos = new Vector3(this.transform.position.x, this.transform.position.y, camera.transform.position.z);
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 end, float lerpTime)
    {
        float t = 0;
        while (t < lerpTime)
        {
            t += Time.deltaTime;

            camera.transform.position = Vector3.Lerp(start, end, (t*t) / lerpTime);
            if (t>= lerpTime)
            {
                camera.transform.position = end;
            }
            yield return null;
        }
    }
}

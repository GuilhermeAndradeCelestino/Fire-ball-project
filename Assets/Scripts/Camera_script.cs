using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    
    public static bool shakeCamera;
    public float shakeDuration;
    public AnimationCurve shakeCurve;

    public bool testShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (testShake)
        {
            StartCoroutine(ScreenShake());
            testShake = false;
        }


        if (shakeCamera)
        {
            shakeCamera = false;
            StartCoroutine(ScreenShake());
            
        }
    }

    IEnumerator ScreenShake()
    {
        Vector3 startPosition = transform.position;
        float time = 0;
        
        while(time < shakeDuration)
        {
            time += Time.deltaTime;
            float strength = shakeCurve.Evaluate(time / shakeDuration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}

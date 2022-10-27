using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_script : MonoBehaviour
{
    
    public static bool shakeCamera;
    public float shakeDuration;
    public AnimationCurve shakeCurve;

    public bool testShake;

    [Space]
    [Space]

    public Transform playerTransform;
    public float speed;
    public Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        if (testShake)
        {
            StartCoroutine(ScreenShake());
            testShake = false;
        }


        if (shakeCamera)
        {
            
            StartCoroutine(ScreenShake());
            shakeCamera = false;
        }
    }


    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position - cameraOffset, speed * Time.deltaTime);
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

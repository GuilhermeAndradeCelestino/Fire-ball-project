using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuebrarParede_Script : MonoBehaviour
{
    public Transform fragmentsWall;
    public GameObject normalWall;

    public static bool quebrar;
    [Space]
    [Space]
    public float explosion; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (quebrar)
        {
            normalWall.SetActive(false);
            Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);

            foreach(Transform child in transformFragments)
            {
                if(child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                {
                    //childRigidbody.AddExplosionForce()
                }
            }
        }
    }
}

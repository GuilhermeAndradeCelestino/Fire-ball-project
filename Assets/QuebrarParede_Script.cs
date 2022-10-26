using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuebrarParede_Script : MonoBehaviour
{
    public Transform fragmentsWall;
    public GameObject normalWall;

    public static bool quebrar;
    public static float breakPower;
    [Space]
    [Space]
    public float explosionForce;
    public float explosionRadios;
    public bool teste;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (teste)
        {
            quebrar = teste;
            teste = false;
        }
        */
        if (quebrar)
        {
            if (breakPower >= 3 && breakPower < 6)
            {
                explosionForce = 500;
                normalWall.SetActive(false);
                Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);

                foreach (Transform child in transformFragments)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                    {
                        childRigidbody.AddExplosionForce(explosionForce, normalWall.transform.position, explosionRadios);
                    }
                }
                quebrar = false;
            }
            else if(breakPower >=6 && breakPower < 8)
            {
                explosionForce = 1000;
                normalWall.SetActive(false);
                Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);

                foreach (Transform child in transformFragments)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                    {
                        childRigidbody.AddExplosionForce(explosionForce, normalWall.transform.position, explosionRadios);
                    }
                }
                quebrar = false;
            }
            else if (breakPower >= 8 && breakPower <= 10)
            {
                explosionForce = 2000;
                normalWall.SetActive(false);
                Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);

                foreach (Transform child in transformFragments)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                    {
                        childRigidbody.AddExplosionForce(explosionForce, normalWall.transform.position, explosionRadios);
                    }
                }
                quebrar = false;
            }
            else
            {
                quebrar=false;
            }
            
        }
    }
}

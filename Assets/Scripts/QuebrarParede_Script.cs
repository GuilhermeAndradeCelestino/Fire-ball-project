using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuebrarParede_Script : MonoBehaviour
{
    public Transform fragmentsWall;
    public GameObject normalWall;
    public GameObject explosionParticle;
    public Collider colisao;


    public static bool quebrar;
    public static float breakPower;
    [Space]
    [Space]
    public float explosionForce;
    public float explosionRadios;
    public bool teste;

   

    private void Awake()
    {
        quebrar = false;
        
    }

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
        QuebrarParede();
    }

    
    IEnumerator destroyWall()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    

    void QuebrarParede()
    {
        if (quebrar)
        {
            if (breakPower <= 6)
            {
                colisao.enabled = false;
                explosionForce = 500;
                normalWall.SetActive(false);
                Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);
                Destroy(Instantiate(explosionParticle, normalWall.transform.position, normalWall.transform.rotation), 5);
                foreach (Transform child in transformFragments)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                    {
                        childRigidbody.AddExplosionForce(explosionForce, normalWall.transform.position, explosionRadios);
                    }
                }
        
                
                StartCoroutine(destroyWall());
                quebrar = false;
            }
            else if (breakPower > 6 && breakPower <= 8)
            {
                colisao.enabled = false;
                explosionForce = 1000;
                normalWall.SetActive(false);
                Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);
                Destroy(Instantiate(explosionParticle, normalWall.transform.position, normalWall.transform.rotation), 5);
                foreach (Transform child in transformFragments)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                    {
                        childRigidbody.AddExplosionForce(explosionForce, normalWall.transform.position, explosionRadios);
                    }
                }

            
                StartCoroutine(destroyWall());
                quebrar = false;
            }
            else if (breakPower > 8)
            {
                colisao.enabled = false;
                explosionForce = 2000;
                normalWall.SetActive(false);
                Transform transformFragments = Instantiate(fragmentsWall, normalWall.transform.position, normalWall.transform.rotation);
                Destroy(Instantiate(explosionParticle, normalWall.transform.position, normalWall.transform.rotation), 5);
                foreach (Transform child in transformFragments)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
                    {
                        childRigidbody.AddExplosionForce(explosionForce, normalWall.transform.position, explosionRadios);
                    }
                }


                StartCoroutine(destroyWall());
                quebrar = false;
            }
            else
            {
                quebrar = false;
            }

        }
    }
}

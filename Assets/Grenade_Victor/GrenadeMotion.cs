using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMotion : MonoBehaviour
{
    public GameObject aCamera;
    public GameObject Explosion;
    public GameObject part1;
    public GameObject part2;
    private Rigidbody rb;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // component of grenade
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G)) // throw grenade;
        {
            Vector3 direction = aCamera.transform.forward;
            direction.y = 1;
 
            rb.AddForce(2.1f*direction, ForceMode.Impulse); // 10* is the power of throwing
            rb.useGravity = true;
            StartCoroutine(Explode());
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2.5f);
        Explosion.SetActive(true);
        part1.SetActive(false);
        part2.SetActive(false);
        sound.Play();


        // add explosion influence on other objects
        Collider[] objectsCollider = Physics.OverlapSphere(transform.position, 20);

        for(int i = 0; i<objectsCollider.Length;i++)
        {
            Rigidbody r = objectsCollider[i].GetComponent<Rigidbody>();
            if (r!=null) // it has Rigidbody
            {
                r.AddExplosionForce(600, transform.position, 20);
            }
        }
    }
}

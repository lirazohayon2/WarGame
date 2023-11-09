using UnityEngine;
using System.Collections;

public class GGranadegunAct : MonoBehaviour, GunAgentInterface
{
    public GameObject aCamera;
    public GameObject Grenade;


    //Grenade Instance
    private GameObject grenadeCurrentInstance;
    private Rigidbody grenadeCurrentRigidbody;
    private GameObject grenadeCurrentExplosion;

    public Transform barrelEnd;
    public GameObject muzzleParticles;

    
    public AudioClip explosionSound;
    public AudioClip flareShotSound;
    public AudioClip reloadSound;

    private int minDamage = 10;
    private int maxDamage = 20;

    public void shootAct(GameObject target, KnightHealthBar hb, string player_name)
    {
        GetComponent<Animation>().CrossFade("Shoot");
        GetComponent<AudioSource>().PlayOneShot(flareShotSound);

        Vector3 direction = target.transform.position - barrelEnd.transform.position;
        direction.y += target.transform.localScale.y;


        grenadeCurrentInstance = Instantiate(Grenade, barrelEnd.transform.position, Quaternion.identity);
        grenadeCurrentInstance.SetActive(true);

        grenadeCurrentRigidbody = grenadeCurrentInstance.GetComponent<Rigidbody>();
        grenadeCurrentRigidbody.AddForce( direction/2, ForceMode.Impulse); // 10* is the power of throwing
        grenadeCurrentRigidbody.useGravity = true;

        grenadeCurrentExplosion = Utils.FindChildWithTag(grenadeCurrentInstance, "Explosion");
        StartCoroutine(Explode(target, hb, player_name));

        Instantiate(muzzleParticles, barrelEnd.position, barrelEnd.rotation);
        Reload();

    }

    void Reload()
    {
        GetComponent<AudioSource>().PlayOneShot(reloadSound);
        GetComponent<Animation>().CrossFade("Reload");

    }


    IEnumerator Explode(GameObject target, KnightHealthBar hb, string player_name)
    {
        //Debug.Log("Wait - Before");
        yield return new WaitForSeconds(1.5f);
        //Debug.Log("Explode");
        grenadeCurrentExplosion.SetActive(true);
        grenadeCurrentInstance.GetComponent<AudioSource>().Play();

        hb.takeRandomDamage(minDamage, maxDamage);
        if (!hb.isAlive())
        {
            Utils.announceAKill(player_name, target.name);
            Utils.dieAct(target);
        }

        /*
            // add explosion influence on other objects
            Collider[] objectsCollider = Physics.OverlapSphere(transform.position, 20);

            for (int i = 0; i < objectsCollider.Length; i++)
            {
                Rigidbody r = objectsCollider[i].GetComponent<Rigidbody>();
                if (r != null) // it has Rigidbody
                {
                    r.AddExplosionForce(600, transform.position, 20);
                }
            }
        */

        yield return new WaitForSeconds(1f);
        Destroy(grenadeCurrentInstance);





    }
}

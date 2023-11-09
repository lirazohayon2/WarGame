using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerShooting : MonoBehaviour
{
    public GameObject muzzle;
    public LayerMask whatIsEnemy;
    public Camera camera;

    private LineRenderer lr;
    private Vector3 targetPosition;

    public ParticleSystem fireFlash;
    public GameObject gun;
    public Material LaserMaterial;
    public Material ShootingMaterial;


    private int minDamage = 20;
    private int maxDamage = 30;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;
    }


    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit,5000, whatIsEnemy))
        {
            targetPosition = hit.point;
            DrawLaserLine();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource sound = gun.GetComponent<AudioSource>();
                sound.Play();
                fireFlash.Play();
                StartCoroutine(DrawShootingLine());
                KnightHealthBar hb = hit.transform.gameObject.GetComponent<KnightHealthBar>();
                shootAct(hit.transform.gameObject, hb, "MainPlayer");
            }
        }
        else
        {
            lr.enabled = false;
        }
    }

    void DrawLaserLine()
    {
        lr.SetPosition(0, muzzle.transform.position);
        lr.SetPosition(1, targetPosition);
        lr.enabled = true;        
    }

    IEnumerator DrawShootingLine()
    {
        // 1. draw line
        lr.SetPosition(0, muzzle.transform.position);
        lr.SetPosition(1, targetPosition);
        lr.material = ShootingMaterial;
        lr.enabled = true;
        // 2. delay
        yield return new WaitForSeconds(0.1f);
        // 3. remove line
        lr.enabled = false;
        lr.material = LaserMaterial;
    }


    public void shootAct(GameObject target, KnightHealthBar hb, string player_name)
    {
        if (!hb.isAlive())
            return;
        hb.takeRandomDamage(minDamage, maxDamage);
        if (!hb.isAlive())
        {
            Utils.announceAKill(player_name, target.name);
            Utils.dieAct(target);
        }
    }
}

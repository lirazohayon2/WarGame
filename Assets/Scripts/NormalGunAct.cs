using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGunAct : MonoBehaviour, GunAgentInterface
{
    public GameObject gun;
    public GameObject muzzle;
    public ParticleSystem fireFlash;
    private LineRenderer line;
    private int minDamage = 10;
    private int maxDamage = 20;
     
    public void shootAct(GameObject target, KnightHealthBar hb, string player_name)
    {
        line = gun.GetComponent<LineRenderer>();
        AudioSource sound = gun.GetComponent<AudioSource>();
        sound.Play();
        fireFlash.Play();
        StartCoroutine(DrawShootingLine(target));

        hb.takeRandomDamage(minDamage, maxDamage);
        if (!hb.isAlive())
        {
            Utils.announceAKill(player_name, target.name);
            Utils.dieAct(target);
        }
    }

    IEnumerator DrawShootingLine(GameObject target)
    {
        // 1. draw line
        line.SetPosition(0, muzzle.transform.position);
        Vector3 tar_pos = target.transform.position;
        tar_pos.y += target.transform.localScale.y;
        line.SetPosition(1, tar_pos);
        line.enabled = true;
        // 2. delay
        yield return new WaitForSeconds(0.4f);
        // 3. remove line
        line.enabled = false;
    }



}

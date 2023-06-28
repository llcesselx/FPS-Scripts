using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scr_Bullet : MonoBehaviour
{
    public float aliveTime;
    public float bulletDamage;
    public float moveSpeed;

    public GameObject bulletSpawn;

    // Refrences kill counter script to add to score per kill
    private scr_KillCounter _killCounter;

    // Enemy behavior variables
    private GameObject enemyTriggered;
    private scr_Enemy enemy;
    public float enemyHealth;

    void Start()
    {
        // Spawns bullet relative to bulletSpawn point
        bulletSpawn = GameObject.Find("BulletSpawn");
        this.transform.rotation = bulletSpawn.transform.rotation;

    }

    void Update()
    {
        BulletBehavior();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {            
            enemyTriggered = other.gameObject;
            enemyTriggered.GetComponent<scr_Enemy>().enemyHealth -= bulletDamage;
            Destroy(this.gameObject);
            _killCounter = FindObjectOfType<scr_KillCounter>();
            _killCounter.AddKill();
        }
    }
    
    private void BulletBehavior()
    {
        aliveTime -= 1 * Time.deltaTime;

        if (aliveTime <= 0)
        {
            Destroy(this.gameObject);
        }

        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }


}

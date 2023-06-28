using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Enemy : MonoBehaviour
{
    public float enemyHealth = 10;

    void Update()
    {
        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        Destroy(this.gameObject);
    }

}

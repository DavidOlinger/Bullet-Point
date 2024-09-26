using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manageEnemiesInWave : MonoBehaviour
{
    // Start is called before the first frame update

    public TriangleMoveAndShoot[] enemies = new TriangleMoveAndShoot[10];

    public manageEnemiesInWave prevWave;

    public bool allEnemiesDead = false;

    public int enemiesAlive;

    void Start()
    {
        enemiesAlive = enemies.Length;

        foreach (TriangleMoveAndShoot enemy in enemies)
        {
            if (enemy == null)
            {
                enemiesAlive--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (prevWave.allEnemiesDead)
        {
            foreach (TriangleMoveAndShoot enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.active = true;
                }
            }
        }
    }

    public void enemyDied()
    {
        enemiesAlive--;
        if(enemiesAlive <= 0)
        {
            allEnemiesDead = true;
        }
    }



}

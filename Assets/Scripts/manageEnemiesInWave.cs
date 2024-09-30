using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manageEnemiesInWave : MonoBehaviour
{
    // Start is called before the first frame update

    private TriangleMoveAndShoot[] enemies;

    public manageEnemiesInWave prevWave;

    public bool allEnemiesDead = false;

    public int enemiesAlive;

    public bool isLastWave = false;






    void Start()
    {
        enemies = GetComponentsInChildren<TriangleMoveAndShoot>();

        enemiesAlive = enemies.Length;

        foreach( TriangleMoveAndShoot enemy in enemies)
        {
            if(enemy == null)
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
            if (isLastWave)
            {
                Invoke("loader", 2);
            }
        }
    }


    void loader()
    {
        SceneManager.LoadSceneAsync("Transition");
    }



}

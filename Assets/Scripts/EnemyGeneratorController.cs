using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour {

    public GameObject enemyPrefab;
    public float generatorTimer = 1.75f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateEnemy()
    {
        float y = Random.Range(-4f, 4f);
        Vector3 position = new Vector3(transform.position.x, y);
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }

    public void StartGenerator()
    {
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
    }

    public void CancelGenerator(bool clean = false)
    {
        CancelInvoke("CreateEnemy");
        if (clean)
        {
            Object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }

    }
}

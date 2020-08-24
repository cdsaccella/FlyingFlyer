using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorController : MonoBehaviour, IFinalizable {

    public GameObject [] enemiesPrefab;

    public float initialForce = 1f;

    public float forceTolerance = 0.1f;
    public float forceInc = 0.1f;

    public float timerDec = 0.1f;
    public float timerThreshold = 0.25f;
    public float initialGeneratorTimer = 1.5f;

    public float updaterTimer = 5f;

    private float force;
    private float generatorTimer;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () { 
		
	}

    void CreateEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemiesPrefab.Length);
        GameObject enemy = Instantiate(enemiesPrefab[randomEnemyIndex], GetPosition(), Quaternion.identity);
        AddForce(enemy);
    }

    void UpdateDifficulty()
    {
        force += forceInc;
        if(generatorTimer - timerDec >= timerThreshold)
        {
            generatorTimer = generatorTimer - timerDec;
        }
        CancelInvoke("CreateEnemy");
        InvokeRepeating("CreateEnemy", generatorTimer, generatorTimer);
    }

    public void StartGenerator()
    {
        ResetInitialValues();
        InvokeRepeating("CreateEnemy", 0f, generatorTimer);
        InvokeRepeating("UpdateDifficulty", updaterTimer, updaterTimer);
    }

    public void CancelGenerator(bool clean = false)
    {
        CancelInvoke("CreateEnemy");
        CancelInvoke("UpdateDifficulty");      
        if (clean)
        {
            Object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in allEnemies)
            {
                Destroy(enemy);
            }
        }
    }

    private void ResetInitialValues()
    {
        force = initialForce;
        generatorTimer = initialGeneratorTimer;
    }

    private Vector3 GetPosition()
    {
        float y = Random.Range(-4f, 4f);
        return new Vector3(transform.position.x, y);
    }

    private void AddForce(GameObject enemy)
    {
        float forceMin = force - forceTolerance * force;
        float forceMax = force + forceTolerance * force;
        float finalForce = Random.Range(forceMin, forceMax);

        Vector2 variableVector = Vector2.zero;

        if(Random.value > 0.5)
        {
            variableVector = Vector2.up * Random.Range(-1f, 1f);
        }

        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.left * finalForce + variableVector, ForceMode2D.Impulse);
    }

    public void EndGame()
    {
        CancelGenerator(false);
    }
}

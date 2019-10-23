using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFlow : MonoBehaviour
{
    /*
    // public Transform spawn;
    [SerializeField] GameObject bombPrefab;
    Transform bombParent;
    public BoxCollider boxCollider;
    float timer = 1f;
    bool activateBomb = false;


    void Start()
    {
        bombParent = new GameObject().transform;
        bombPrefab = Resources.Load<GameObject>("Prefabs/BomberBomb");
        boxCollider.GetComponent<BoxCollider>();

        if (activateBomb == true)
            SpawnBomb();
        // base.Initialize(startingEnergy);

    }


    void FixedUpdate()
    {
        
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            activateBomb = true;
            if (activateBomb == true)
                SpawnBomb();
            activateBomb = false;
            timer = 1f;
        }
            
    }

    public void SpawnBomb()
    {
        GameObject newBomb = Instantiate(bombPrefab, bombParent);
        GameObject bomber = GameObject.FindGameObjectWithTag("Bomber");
        newBomb.transform.position = bomber.transform.position + new Vector3(0, -2f, 0);

        boxCollider.enabled = false;
    }
    */
}
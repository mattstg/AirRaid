using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFlow : MonoBehaviour
{
    int numberOfBomb;
    public Transform spawn;
    [SerializeField] GameObject bombPrefab;
    Transform bombParent;
    public BoxCollider boxCollider;
    float timer = 1f;
    bool activateBomb = false;

    private void Awake()
    {
        bombParent = new GameObject().transform;
        bombPrefab = Resources.Load<GameObject>("Prefabs/Bomb");
        boxCollider.GetComponent<BoxCollider>();
        
    }

    void Start()
    {

        if (activateBomb == true)
            SpawnBomb();


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

    void SpawnBomb()
    {
        GameObject newBomb = GameObject.Instantiate(bombPrefab, bombParent);
        newBomb.transform.position = spawn.position + new Vector3(0, -2f, 0);

        boxCollider.enabled = false;
    }

}
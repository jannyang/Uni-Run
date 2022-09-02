using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public PlatformSpawner platformSpawner;
    public float coinSpawnTime = 0.1f;
    public int coinCount = 10;

    private GameObject[] coins;
    private Vector2 poolPosition = new Vector2(0, -30);

    private float xPos = 20f;
    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private int currentIdx = 0;
    private float lastTime = 0f;
    
    void Start()
    {
        coins = new GameObject[coinCount];
        platformSpawner = GetComponent<PlatformSpawner>();

        for(int i = 0; i < coinCount; i++)
        {
            coins[i] = Instantiate(coinPrefab, poolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameManager.instance.isGameover)
        {
            return;
        }

        if (Time.time >= lastTime + coinSpawnTime)
        {
            
            lastTime = Time.time;
            //float yPos = 3f;
            
            //coins[currentIdx].transform.position = new Vector2(xPos, yPos);

            currentIdx++;
            
            if (currentIdx >= coinCount)
            {
                currentIdx = 0;
            }
        }
        

    }
}

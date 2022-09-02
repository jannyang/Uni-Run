using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public GameObject coinPrefab;
    public ScrollingObject scrollingObject;
    
    [SerializeField]
    private int count = 3; // 생성할 발판의 개수
    [SerializeField]
    private int coinCount = 30;

    [SerializeField]
    private float coinSpawnTime = 0.1f;
    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    public float yPos = 0f;
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private GameObject[] coins;

    private int currentIndex = 0; // 사용할 현재 순번의 발판
    private int coinIdx = 0;

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점
    private float lastCoinSpawnTime;
    private int intervalSpawnNum;
    private float lastYpos;
    private float posInterval;
    private float coinSpawnSum;
    private float emptySpawnTime;
    private float intervalChange;

    void Start() {

        platforms = new GameObject[count];
        coins = new GameObject[coinCount];

        
        for(int i = 0; i< count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        for(int j = 0; j< coinCount; j++)
        {
            coins[j] = Instantiate(coinPrefab, poolPosition, Quaternion.identity);
        }

        emptySpawnTime = 0f;
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
    }
    
    void Update() {
        if (GameManager.instance.isGameover)
        {
            return;
        }


        if (Time.time >= lastSpawnTime + timeBetSpawn)
        { 
            lastSpawnTime = Time.time;

            timeBetSpawn = Mathf.Round(Random.Range(timeBetSpawnMin, timeBetSpawnMax)*10) / 10;

            lastYpos = yPos;
            yPos = Random.Range(yMin, yMax);
            
            posInterval = lastYpos - yPos;

            platforms[currentIndex].SetActive(false); // Platform 컴포넌트의 OnEnable() 메서드를 통한 reset
            platforms[currentIndex].SetActive(true); // OnEnable() -> 컴포넌트 활성화될 때마다 실행

            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            currentIndex++;

            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
        // 순서를 돌아가며 주기적으로 발판을 배치


        emptySpawnTime = timeBetSpawn - 1f;
        intervalChange = coinSpawnTime * posInterval / emptySpawnTime;

        if (Time.time >= lastCoinSpawnTime + coinSpawnTime)
        {
            lastCoinSpawnTime = Time.time;
            coinSpawnSum += coinSpawnTime;

            if (coinSpawnSum < 1f)
            {
                coins[coinIdx].transform.position = new Vector2(xPos - 4.3f, yPos + 1.4f);
                coinIdx++;
            }

            else if (coinSpawnSum >= 1f)
            {
                yPos += intervalChange;
                coins[coinIdx].transform.position = new Vector2(xPos - 4.3f, yPos + 1.4f);
                coinIdx++;
            }

            if (coinSpawnSum >= timeBetSpawn)
            {
                coinSpawnSum = 0;
            }

            if (coinIdx >= coinCount)
            {
                coinIdx = 0;
            }
        }
    }
}
using UnityEngine;

enum CoinType
{
    Obstacle,
    Line,
    Bezier,
    MAX
}
// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    public void Init(GameObject coinpref)
    {
        stepped = false;

        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }

            else
            {
                obstacles[i].SetActive(false);
            }
        }

        for(int i = 0; i< transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if(go.name.Contains("Coin"))
            {
                Destroy(go);
            }
        }

        CoinType type = (CoinType)Random.Range(0, (int)CoinType.MAX);

        switch (type)
        {
            case CoinType.Obstacle:
                //장애물 위에 생성
                for (int i = 0; i < obstacles.Length; i++)
                {
                    if (obstacles[i].activeSelf)
                    {
                        Vector3 pos = obstacles[i].transform.position;
                        pos.y += 1;
                        GameObject go = Instantiate(coinpref, this.transform); //Instantiate(coinpref, this.transform) ->Platform(this)의 하위로 coinpref을 생성한다 
                        go.transform.position = pos;                           //-> ScrollingObject 스크립트 사용안해도 된다.
                    }
                }
                break;

            case CoinType.Line:
                for (int i = 0; i< obstacles.Length; i++)
                {
                    obstacles[i].SetActive(false);
                    GameObject go = Instantiate(coinpref, this.transform);
                    go.transform.position = obstacles[i].transform.position;
                }
                break;

            case CoinType.Bezier:
                for (int i = 0; i< obstacles.Length; i++)
                {
                    if(obstacles[i].activeSelf)
                    {
                        Vector3 pos = obstacles[i].transform.position;
                        pos.y += 1;
                        GameObject go = Instantiate(coinpref, this.transform);
                        go.transform.position = pos;
                    }

                    else
                    {
                        GameObject go = Instantiate(coinpref, this.transform);
                        go.transform.position = obstacles[i].transform.position;
                    }
                }
                break;
        }
    }
    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    //private void OnEnable() {
    //    // 발판을 리셋하는 처리
    //    stepped = false;

    //    for(int i = 0; i< obstacles.Length; i++)
    //    {
    //        if(Random.Range(0, 3) == 0)
    //        {
    //            obstacles[i].SetActive(true);
    //        }

    //        else
    //        {
    //            obstacles[i].SetActive(false);
    //        }
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        if(collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;
            GameManager.instance.AddScore(10);
        }
    }
}
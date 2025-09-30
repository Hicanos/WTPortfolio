using UnityEngine;

public class BGController : MonoBehaviour
{

    // IsTrigger에 걸리면 해당 오브젝트를 x축 +81.92f만큼 이동
    // 게임 종료 시 다시 원점으로 이동

    // 원상복구용 변수(원점, 배경 2개씩 한 쌍 4개의 위치, 0,0,0부터 20.48f의 간격으로 x축)
    [SerializeField] private Vector3 originalPos1;
    [SerializeField] private Vector3 originalPos2;
    [SerializeField] private Vector3 originalPos3;
    [SerializeField] private Vector3 originalPos4;

    // 이동시킬 배경(오브젝트)
    [SerializeField] private GameObject[] bgs;
    [SerializeField] private GameObject[] skys;

    private void Start()
    {
        originalPos1 = new Vector3(0f, 0f, 0f);
        originalPos2 = new Vector3(20.48f, 0f, 0f);
        originalPos3 = new Vector3(40.96f, 0f, 0f);
        originalPos4 = new Vector3(61.44f, 0f, 0f);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BackGround"))
        {
            Debug.Log("백그라운드 감지됨");
            collision.transform.position = new Vector3(collision.transform.position.x + 81.92f, 0, 0);
        }
    }

    public void ResetPosition()
    {
        // bg0과 sky0은 0,0,0, 이후 1,2,3 순서대로 20.48f씩 증가
        bgs[0].transform.position = originalPos1;
        bgs[1].transform.position = originalPos2;
        bgs[2].transform.position = originalPos3;
        bgs[3].transform.position = originalPos4;
        skys[0].transform.position = originalPos1;
        skys[1].transform.position = originalPos2;
        skys[2].transform.position = originalPos3;
        skys[3].transform.position = originalPos4;
    }
}

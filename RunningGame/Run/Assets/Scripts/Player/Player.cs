using UnityEngine;

// Player 오브젝트에 부착, 자동 이동만 담당
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject character; // Character 오브젝트 참조
    [SerializeField] private PLAnimController animController; // Character 오브젝트의 애니메이션 컨트롤러
    [SerializeField] private float speed = 7f; // 이동 속도
    private bool isMoving = false;

    public void SetGameStart()
    {
        isMoving = true;
        animController.PlayRunAnim();
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}

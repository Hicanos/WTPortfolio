using UnityEngine;

/// <summary>
/// 소리 관리 매니저
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    [SerializeField] private AudioClip[] SfxClip; // 효과음 클립 배열
    [SerializeField] private AudioSource AudioSource; // 오디오 소스
    [SerializeField] private AudioClip BgmClip; // 배경음악 클립

    public void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(AudioSource == null)
            AudioSource = GetComponent<AudioSource>();
    }

    public void SfxStart(int index)
    {
        // 효과음재생(외부에서 인덱스 지정, 배열로 관리)
        // 0 선택 1 점프 2 충돌 3 게임오버 4 슬라이드
        AudioClip sfx = SfxClip[index];
        AudioSource.PlayOneShot(sfx);
    }
    public void BgmStart()
    {
        // 배경음악재생
        AudioSource.clip = BgmClip;
        AudioSource.loop = true; // 반복재생 설정
        AudioSource.Play();
    }
}

# WTPortfolio
포트폴리오를 겸하여 개인 제작한 게임, 프로그램 모음집

# 🏃Running Game
> 👾모티브 게임: 🦖크롬 다이노(Chrome Dino)   
> 🖥️개발 엔진: Unity 6000.0.41f1

구글에서 오류가 나면 볼 수 있는 크롬 다이노를 토대로 한 러닝게임.
유사한 게임으로 쿠키런, 3D 장르로는 템플런 등이 유명하다.

점프와 슬라이드를 통해 다양한 장애물(무당벌레, 벌, 톱)을 피하고 최고 점수를 내면 되는 게임이다.

## 🎥플레이 영상
 
https://github.com/user-attachments/assets/9eb6f011-d749-45bc-aba3-483cacb32352

> 🎮조작법
- 점프: 스페이스, 상 방향키
- 숙이기: 하 방향키

> 🗂️빌드파일 경로
- WTPortfolio/RunningGame/Build.zip
- !주의! 윈도우 게임입니다. PC 사양에서만 플레이 가능합니다.

## 사용 기술 스택

1. **Object Pooling**: 애너미/타일맵의 생성-파괴를 진행하는 대신, SetActive false를 진행시킴으로서 가비지 컬렉터를 최소화. 사실상 게임오버 전까지 무한히 이어지는 게임에 있어선 핵심 기능.
2. **tilemap.WorldToCell()**: 타일맵-캐릭터간 거리 차이가 발생하면서 사용함. 타일맵의 사이즈가 달라서 발생한 타일맵-월드 좌표(플레이어 좌표)의 차이를 줄이기 위해 사용.

# 💎3Match Game(제작 중) 
> 👾모티브 게임: 👑로얄 매치(Royal Match)   
> 🖥️개발 엔진: Unity 6000.2.14f1

블록 3개를 연달아 연결해 파괴하는 3 매치 게임 중, 쉽게 접할 수 있는(그리고 접근 난이도가 낮은) Royal Match를 토대로 제작한 3Match 게임.
원조격 게임은 Bejeweled 게임이며, 그 외로 유명한 게임은 애니팡 시리즈/캔디 크러시 사가 등이 있다.

같은 색의 블럭들을 맞춰 장애물을 부수거나, 특정한 모양으로 특수 아이템을 만들어 사용해서 일정 횟수 안에 제시된 조건을 만족하면 클리어하는 게임이다.

> 🎮 조작법
- 휴대폰 터치: 특수 아이템 사용, 아이템 사용 등의 상호작용
- 휴대폰 슬라이드: 인근 블록과 자리 변경


---

## GitHub Commit Convention

GitHub Desktop에 Commit 시 사용하는 Commit 스타일 가이드입니다.


### 구성 요소 설명

*   **type:** 커밋의 종류를 나타냅니다.
    *   :white_check_mark:`Add`: 새로운 파일 추가
    *   :wastebasket:`Remove`: 기존 파일 삭제
    *   🚚: `Move`: 파일 옮김/정리
    *   :sparkles:`Feat`: 새로운 기능 추가
    *   :hammer:`Fix`: 버그 수정
    *   :twisted_rightwards_arrows:`merge`: 머지 작업
    *   :rewind:`Revert`: 리버트
    *   :memo:`Docs`: 문서 수정
    *   🗒️ `Script` : package.json 변경(npm 설치 등)
    *   :art:`Style`: 코드 포맷, 세미콜론 등 (코드 내용 변경 없음)
    *   :recycle:`Refactor`: 코드 리팩토링 (기능 변경 없음)
    *   :test_tube:`Test`: 테스트 코드 추가/수정
    *   :package:`Chore`: 빌드, 패키지 설정 등 기타 사항
    *   :camera_flash:`Design`: UI 디자인 변경 
    *   :rotating_light:`!HOTFIX`: 치명적인 버그 긴급 수정
*   **subject:** 변경 내용을 간결하게 요약합니다 (영문 작성 시 동사 원형 시작, 50자 이내, 마침표 없음 권장).
*   **body (본문):** 커밋 내용을 자세히 설명합니다 ("어떻게"보다는 "무엇을" "왜" 변경했는지 초점). 각 줄은 75자 이내 권장.
*   **footer (꼬리말):** : 필요 시 사용

### 커밋 메시지 예시

type: subject

body (optional)

footer (optional) 

```
Feat: Add player health system(한글 작성 가능)

플레이어 HP 바와 피해 처리가 구현되었습니다.
피해를 받으면 체력이 감소합니다.
체력이 0이 되면 게임 오버가 됩니다.

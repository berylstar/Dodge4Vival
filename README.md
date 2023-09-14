# Dodge4Vival

[시연영상](https://www.youtube.com/watch?v=-6np2qZMkPI)

[발표 PPT](https://docs.google.com/presentation/d/1TKiOyxPhmPMTmCGOBBr5Aj4L6ehShFg4nScoegQ4WOo/edit#slide=id.g27e4279b86d_5_23)
 
---

### 몬스터

그룹 | 이름|체력|스피드|이동|
--|--|--|--| -- |
1 | TinyZombie | 2 | 2 | Straight
1 | Goblin | 1 | 3 | Straight
1 | Imp | 1 | 3 | Straight
2 | Angel | 1 | 2 | Follower
2 | Pumpkin | 2 | 3 | Straight
2 | Doc | 1 | 5 | Straight
3 | MaskedOrc | 3 | 3 | Straight
3 | OrcShaman | 1 | 4 | Follower
3 | OrcWarrior | 6 | 1 | Straight
3 | Orge | 10 | 2 | Follower
4 | Chort | 2 | 4 | Follower
4 | Wogol | 4 | 2 | Follower
4 | BigDemon | 15 | 3 | Follower

### 레벨 디자인

시간(s) | 1 | 2 | 3 | 3-Orge | 4 | 4-BigDemon
--|--|--|--|--|--|--|
30| O |  |  |  |  |  |
60|  | O |  |  |  |  |
90| O |  |  |  |  |  |
120|  |  | O |  |  |  |
150|  | O |  |  |  |  |
180| O |  |  |  |  |  |
210|  |  |  | X |  |  |
240|  |  | O |  |  |  |
270|  |  |  |  | O |  |
300|  | O |  |  |  |  |
330|  |  |  | X |  |  |
360| O |  |  |  |  |  |
390|  |  |  |  | O |  |
420|  |  | O |  |  |  |
450|  |  |  |  |  | X |
 |  |  |  |  | 30s  |  |
 |  |  |  |  |  |  | 50s


# 구현 목록 

🔽 김민상
- 플레이어
    - InputSystem 구축
        - WASD : 이동
        - Left Click : 공격
        - Mouse Scroll : 카메라 확대/축소
    - 애니메이터
    - 피격 시각 효과
    - 기본 로직 구현
    - 코루틴을 이용한 쿨타임 구현

- 몬스터
    - 애니메이터 : Override Controller 활용
    - 피격 시각 효과
    - 스크립터블 오브젝트로 상수 관리
    - 몬스터 패턴별 분리 구현 : Follower, Straight

- 게임 시스템
    - 몬스터 레벨 디자인
    - 무작위 몬스터 출현 이벤트 구축
    - 오브젝트 간 충돌 이벤트 및 시각 효과 구현

- 투사체
    - 요소 디자인 및 프리팹화

- 트랩 오브젝트 추가


🔽 장성림
- 스크립터블 오브젝트
    - 데이터 정리
    - 이벤트 관리

- 아이템
    - 요소 디자인 및 프리팹화
    - 아이템 드롭 컨트롤러 구축

- 무기
    - 랜덤 무기 획득 시스템
    - UI

- 투사체
    - 같은 무기 획득 시 투사체 증가

- 플레이어
    - 구르기 기능 추가
        - InputSystem : SPACE
        - 애니메이션

- GameEventEditor
    - 출처 : https://github.com/roboryantron/Unite2017


🔽 신현주
- 시작 배경화면 제작
- 게임 타이틀 제작
- StartScene
    - UI 구성
    - 애니매이션 추가
    - Scene간 이동 관리


🔽 이장원
- 게임 기본 로직 디자인
- Grid 및 Tilemap 구현
- 청각 효과 관리
- 게임 테스트 및 밸런스 조정

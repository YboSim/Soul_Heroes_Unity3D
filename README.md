<div align="center">
<h2>SoulHeroes - Unity3D🎮</h2>
Photon Pun2를 이용한 3D MORPG 게임입니다.   
  
3가지 클래스중 가장 마음에 드는 클래스를 선택하여 Pve, Pvp 요소를 모두 경험할 수 있는 게임입니다.
</div>

## 목차
  - [개요](#개요)
  - [빌드 파일 및 유튜브 영상](#빌드-파일-및-유튜브-영상)
  - [게임 설명](#게임-설명)
  - [주요 활용 기술](#주요-활용-기술)

## 개요
- 프로젝트 이름 : SoulHeroes
- 게임 장르 : 3D MORPG
- 개발 기간 : 3개월(2023.09 - 2023.11)
- 개발 목적 : 가장 좋아하는 MMORPG(리니지라이크류)게임 기능 직접 구현
- 개발 엔진 및 언어 : Unity(2021.3.5f1) & C#

## 빌드 파일 및 유튜브 영상
- [유튜브 포트폴리오 소개 영상]
- [구글 드라이브 다운로드 링크(빌드)]
- [구글 드라이브 다운로드 링크(유니티)]
- [네이버 MyBox 다운로드 링크(빌드)]
- [네이버 MyBox 다운로드 링크(유니티)]
  
## 게임 설명

- 캐릭터 이동 관련 조작법 간단 소개
1. 키보드 이동 - W / A / S / D 로 상하좌우 이동 (Alt 누르고 이동 시 달리기 가능)
2. 마우스 이동 - 원하는 위치에 우클릭 시 자동 이동(이동 중 Alt 누를 시 달리기 가능)
3. 시점 변경 - 마우스 좌클릭 한 상태로 좌우로 드래그 
4. 카메라 거리 조절 - 마우스 휠다운 시 줌 아웃, 마우스 휠업 시 줌 인

- 인게임 단축키
1. Enter - 채팅 On/Off
2. 1~8번 Key - 스킬 사용
3. R Key - 일정거리내 적 스캔 기능
4. S Key - Soul창 On
5. I Key - 장비창 On
6. K Key - 스킬창 On
7. C Key - 설정창 On
8. X Key - 스킬셋편집 On/Off

- 3가지 클래스와 스킬 소개<br>

|Knight|Mage|Healer|
|:---:|:---:|:---:|
|![Knight](https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/a82b7459-819d-4311-8f81-488699c640fe)|![Mage](https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/e9e51c7f-fd94-4c0d-b052-d1935f895fae)|![Healer](https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/daf04e97-14f5-44b0-b0ad-7adfdda83562)|
|근접형 한손검 클래스|원거리형 딜러 클래스|원거리 지원형 클래스|

|클래스|스킬명|아이콘|설명|
|---|---|---|---|
|Knight|강타|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/a3cb6f29-dec9-4d33-9533-11806c497350" width="100" height="100"/>|한순간 힘을 모아 대상(몬스터와 히어로 모두 적용)에게 스턴효과를 부여 하는 액티브형 스킬|
|Knight|분노|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/68aa941f-755f-46e3-ba5d-0fb0b726109d" width="100" height="100"/>|힘을 방출해 공격력과 방어력을 일시적으로 올려주는 버프형 스킬|
|Mage|매직클로|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/96e52f18-b461-4770-8f37-45c451dadbc3" width="100" height="100"/>|마력을 모아 단일대상 적에게 큰 피해를 입히는 액티브형 스킬|
|Mage|메테오샤워|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/31d18db0-7bba-4118-be12-c4c0bbc36367" width="100" height="100"/>|하늘에서 메테오를 소환해 다중대상 적에게 피해를 입히는 액티브형 스킬|
|Healer|힐|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/438c38a8-6755-47c1-9eb3-b18ab248277c" width="100" height="100"/>|자신을 포함한 파티원의 체력을 일정량 회복시키는 버프형 스킬|
|Healer&nbsp;&nbsp;&nbsp;|매혹|&nbsp;<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/75e36021-3f8f-423c-8b85-7c26fadc903d" width="100" height="100"/>&nbsp;|단일대상 적(몬스터 적용)을 매혹시켜 일정시간 방어력을 낮추며 피해를 입힌다|

- 4가지 몹들과 보스몹 소개<br>

|Slime|Turtle|Orc|Golem|Slayer(Boss)|
|:---:|:---:|:---:|:---:|:---:|
|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/a75d0f40-c426-484f-b019-edc7f04f06b0" width="150" height="150"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/529a9fa7-822f-45b5-baa8-1dc8b8a7074f" width="150" height="150"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/f6033f50-a6eb-4c8b-a02f-d1b756280ff3" width="150" height="150"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/7ca2a243-abe9-4049-8911-7ea2a5daae12" width="150" height="150"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/59945d86-aab0-4708-9333-59bd01128d0e" width="150" height="150"/>|

- 5가지 Soul 소개<br>

|Dark|Fire|Ice|Nature|Water|
|:---:|:---:|:---:|:---:|:---:|
|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/9fad8130-ab08-40fe-9b36-4ebabeefe073" width="150" height="200"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/63c552d2-e4d6-42ea-a138-686f2c0a5d19" width="150" height="200"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/cf91c0f9-d9a0-4732-80f4-e5c7478a3c54" width="150" height="200"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/cf084526-4345-4711-9338-edaecfead013" width="150" height="200"/>|<img src="https://github.com/YboSim/Soul_Heroes_Unity3D/assets/142956423/c98a87d4-8186-4d4a-b788-f20f755e8274" width="150" height="200"/>

## 주요 활용 기술
---
* #1-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/HeroCtrl.cs)) Enum형을 이용한 캐릭터의 이동상태 결정

<details>
<summary>소스 코드</summary>
  
```csharp
    void MoveMethodUpdate()
    {
        if (GameMgr.Inst.m_Enter == true)
            return;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h != 0.0f || v != 0.0f)
            m_MoveMethod = MoveMethod.KeyBoardMove;
        else if (Input.GetMouseButtonDown(1))
            m_MoveMethod = MoveMethod.MouseMove;

        if (m_MoveMethod == MoveMethod.KeyBoardMove)
            KeyBDMove();
        else if (m_MoveMethod == MoveMethod.MouseMove)
            MouseMoveUpdate();
        else if (m_MoveMethod == MoveMethod.AttackMove)
            AttackMoveUpdate();
        else if (m_MoveMethod == MoveMethod.SkillMove)
            SkillMoveUpdate();
    }
```
</details>

---
* #1-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/HeroCtrl.cs)) 캐릭터 키보드 이동 구현

<details>
<summary>소스 코드</summary>
  
```csharp
    void KeyBDMove()
    {
        if (m_NavMeshAgent.isStopped == false) //마우스 이동중이였다면
        {
            m_NavMeshAgent.ResetPath();

            if (GameMgr.Inst.m_CursorMark.activeSelf == true)
                GameMgr.Inst.CursorMarkOff();
        }

        if(h != 0.0f || v != 0.0f)
        {
            m_MoveStep = new Vector3(h, 0, v);

            if (v < 0.0f) // 뒤로 걷기
            {
                m_KBMoveSpeed = 2.0f;
                AnimationChange("Walk");
            }
            else if (v > 0.0f && Input.GetButton("Run") == false &&
                        h == 0.0f) //앞으로 걷기 
            {
                m_KBMoveSpeed = 3.0f;
                AnimationChange("Walk");
            }
            else if(v >= 0.0f && Input.GetButton("Run") == false &&
                        h > 0.0f) //오른쪽으로 걷기
            {
                m_KBMoveSpeed = 2.5f;
                AnimationChange("Walk_R");
            }
            else if(v >= 0.0f && Input.GetButton("Run") == false &&
                        h < 0.0f) //왼쪽으로 걷기
            {
                m_KBMoveSpeed = 2.5f;
                AnimationChange("Walk_L");
            }
            else if (v > 0.0f && Input.GetButton("Run") == true &&
                        h == 0.0f) //앞으로 달리기
            {
                m_KBMoveSpeed = 6.0f;
                AnimationChange("Run");
            }
            else if (v > 0.0f && Input.GetButton("Run") == true &&
                        h > 0.0f) //우측대각 달리기
            {
                m_KBMoveSpeed = 5.5f;
                AnimationChange("Run_R");
            }
            else if (v > 0.0f && Input.GetButton("Run") == true &&
                        h < 0.0f) //좌측대각 달리기
            {
                m_KBMoveSpeed = 5.5f;
                AnimationChange("Run_L");
            }

            transform.Translate(m_MoveStep * m_KBMoveSpeed * Time.deltaTime);
        }
        else if (h == 0.0f && v == 0.0f)
        {
            AnimationChange("Idle");

            m_MoveMethod = MoveMethod.Stop;
        }
    }
```
</details>

---

* #1-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/HeroCtrl.cs)) 캐릭터 마우스 이동 구현

<details>
<summary>소스 코드</summary>
  
```csharp
    void MouseMoveUpdate()
    {
        if(Input.GetMouseButtonDown(1) == true)
        {
            m_MoveMethod = MoveMethod.MouseMove;

            m_MsPickPos = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(m_MsPickPos, out hitInfo))
            {
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {//MsPickPos가 땅이면
                    m_NavMeshAgent.SetDestination(hitInfo.point);

                    GameMgr.Inst.CursorMarkOn(hitInfo.point);
                }
                else if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                {//MsPickPos가 오브젝트이면
                    m_NavMeshAgent.SetDestination(hitInfo.collider.gameObject.transform.position);

                    m_AttTarget = hitInfo.collider.gameObject;

                    TargetCheck();

                    m_MoveMethod = MoveMethod.AttackMove;
                }
            }
        }
```
</details>

---

* #1-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/HeroCtrl.cs)) 캐릭터 방향 전환

<details>
<summary>소스 코드</summary>
  
```csharp
    void RotateUpdate()
    {
        if (m_SkillSlotMgr.m_Editing == true)
            return;

        if (m_MoveMethod == MoveMethod.MouseMove ||
            m_MoveMethod == MoveMethod.AttackMove)
            return;

        if (Input.GetMouseButton(0) == true)
        {
            m_MouseX = Input.GetAxis("Mouse X");

            transform.Rotate(0, m_MouseX * m_RotSpeed, 0, Space.World);
        }
    }

```
</details>

---

* #1-5)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs)) 캐릭터의 정면방향을 따라가는 카메라 이동 및 회전 구현

<details>
<summary>소스 코드</summary>
  
```csharp
    void CamPosUpdate()
    {
        if (m_Player == null)
            return;

        m_TargetPos = m_Player.transform.position;
        m_CamForward = m_Player.transform.forward;
        //카메라 바라보는 방향과 플레이어의 바라보는 방향 일치시키기

        CamZoomInOut();

        //카메라 위치 보간하여 이동 및 회전
        m_CamPos = m_TargetPos - m_CamForward * m_PlayerCamDist;
        m_CamPos.y = m_CamPos.y + m_CamHeight;
        transform.position = Vector3.Lerp(transform.position,
                                m_CamPos, m_CamSpeed *Time.deltaTime);
        
        transform.LookAt(m_TargetPos);
        //카메라 위치 보간하여 이동 및 회전
    }

```
</details>

---

* #1-6)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs)) 마우스 스크롤 휠을 이용한 카메라 줌인,아웃
  
<details>
<summary>소스 코드</summary>
  
```csharp
    void CamZoomInOut()
    {
        float a_MouseSW = Input.GetAxis("Mouse ScrollWheel");
        m_PlayerCamDist -= a_MouseSW * m_ZoomSpeed;

        //카메라 와 플레이어간 최대,최소 거리 설정
        if (m_PlayerCamDist > m_MaxDist)
            m_PlayerCamDist = m_MaxDist;
        else if (m_PlayerCamDist < m_MinDist)
            m_PlayerCamDist = m_MinDist;
        //카메라 와 플레이어간 최대,최소 거리 설정
    }
```
</details>

---

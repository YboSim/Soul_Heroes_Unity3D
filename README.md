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

* #2-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs)) 주변 적 목록을 가져와 짧은 거리순으로 공격버튼으로 나열해주기
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void EnemySensor()
    {
        if (m_ScannerTimer > 0.0f)
            return;
        else
            m_ScannerTimer = 3.0f;

        Sound_Mgr.Instance.PlayEffSound("Scan", 0.5f);

        //초기화
        if (m_EnemyList.Count > 0)
            m_EnemyList.Clear();

        EnemyBtn[] a_EnemyBtns = m_EnemeyPanel.GetComponentsInChildren<EnemyBtn>();
        for(int ii = 0; ii < a_EnemyBtns.Length; ii++)
        {
            Destroy(a_EnemyBtns[ii].gameObject);
        }
        //초기화

        //히어로 주변 적들을 List로 가져오기
        Collider[] a_Enemies = Physics.OverlapSphere(transform.position, 30.0f);

        if (a_Enemies.Length < 1)
            return;

        for (int ii = 0; ii < a_Enemies.Length; ii++)
        {
            if (a_Enemies[ii].gameObject.tag == "Monster")
            {
                m_EnemyList.Add(a_Enemies[ii].gameObject);
            }
            else if(a_Enemies[ii].gameObject.tag == "Hero")
            {
                if(a_Enemies[ii].gameObject.GetComponent<HeroCtrl>().m_PhotonView.IsMine == false)
                {
                    m_EnemyList.Add(a_Enemies[ii].gameObject);
                }
            }
        }

        m_ShortDist = 100;
        float a_Dist;
        int a_EnemyCount = m_EnemyList.Count;

        if (a_EnemyCount < 1)
            return;

        for (int ii = 0; ii < a_EnemyCount; ii++)
        {
            //거리가 가장 짧은 Enemy 구하기
            foreach (GameObject a_Enemy in m_EnemyList)
            {
                m_EnemyPos = a_Enemy.transform.position;
                m_EnemyPos.y = 0.0f;
                m_MyPos = transform.position;
                m_MyPos.y = 0.0f;
                a_Dist = (m_EnemyPos - m_MyPos).magnitude;
                if (a_Dist < m_ShortDist)
                {
                    m_ShortDist = a_Dist;
                    m_ShortEnemy = a_Enemy;
                }
            }
            //거리가 가장 짧은 Enemy 구하기

            //가장짧은 Enemy 버튼 생성후 정보 담기
            GameObject a_EnemyBtnObj = Instantiate(m_EnemyBtnPrefab) as GameObject;
            a_EnemyBtnObj.transform.SetParent(m_EnemeyPanel.transform, false);

            EnemyBtn a_EnemyBtn = a_EnemyBtnObj.GetComponent<EnemyBtn>();
            a_EnemyBtn.m_Enemy = m_ShortEnemy; //버튼클릭으로 공격할때 타겟을 저장함
            ObjectInfo a_ObjInfo = m_ShortEnemy.GetComponent<ObjectInfo>();
            a_EnemyBtn.m_NameText.text = a_ObjInfo.m_ObjName;
            a_EnemyBtn.m_DistText.text = m_ShortDist.ToString("N0") + "m"; //버튼에 Enemy 이름 써주기
            //가장짧은 Enemy 버튼 생성후 정보 담기

            m_EnemyList.Remove(m_ShortEnemy);
            m_ShortDist = 100; //m_ShortDist는 리스트에서 삭제된 m_ShortEnemy의 거리기 때문에 'if (a_Dist < m_ShortDist)' 구문을 위해 충분히 큰 수로 바꿔준다

            //각 오브젝트 타입에 따라 글씨색 변경
            if (a_ObjInfo.m_ObjType == ObjectType.Monster)
            {
                a_EnemyBtn.m_NameText.color = Color.black;
            }
            else if (a_ObjInfo.m_ObjType == ObjectType.Hero)
            {
                a_EnemyBtn.m_NameText.color = Color.red;
            }
            else if (a_ObjInfo.m_ObjType == ObjectType.Hero_ally)
            {
                a_EnemyBtn.m_NameText.color = Color.blue;
            }
            //각 오브젝트 타입에 따라 글씨색 변경
        }
    }
```
</details>

---

* #2-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs))ShaderGraph를 이용한 적 스캔 시각적 효과 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Scanner()
    {
        if (m_Scanner == null)
            return;

        if (m_Scanner.activeSelf == false)
        {
            m_Scanner.transform.position = gameObject.transform.position;
            m_Scanner.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

            m_Scanner.SetActive(true);
        }

        m_Scanner.transform.localScale += new Vector3(2.0f, 2.0f, 2.0f);
    }
```
</details>

---

* #2-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs))EnemyBtn을 누를 시 버튼에 담긴 정보를 가져와 대상 공격 기능 구현
  
<details>
<summary>소스 코드</summary>
  
```csharp
    public void Attack(GameObject a_Target) //Enemy버튼을 눌러 공격한 메서드
    {
        m_AttTarget = a_Target;

        TargetCheck();

        if (gameObject.name.Contains("Knight"))
        {
            if (m_AttTarget.name.Contains("Boss"))
                m_AttDist = 3.0f;
            else
                m_AttDist = 2.0f;
        }
        else
            m_AttDist = 6.0f;

        if ((transform.position - m_AttTarget.transform.position).magnitude > m_AttDist)
        {//타겟과의 거리가 공격 사거리 보다 멀 경우
            m_NavMeshAgent.ResetPath();

            AnimationChange("Walk");

            m_NavMeshAgent.SetDestination(a_Target.transform.position);

            m_MoveMethod = MoveMethod.AttackMove;
        }
        else
        {//타겟과의 거리가 공격 사거리내에 도달했을 경우
            m_NavMeshAgent.ResetPath();

            if (m_AttTarget.GetComponent<ObjectInfo>().m_IsDie == false)
                AnimationChange("Attack_B"); //공격 애니메이션 재생

            m_MoveMethod = MoveMethod.AttackMove;
        }
    }
```
</details>

---

* #2-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs))공격 시 자신이 공격한 타겟의 시각화를 위해 타겟 오브젝트 위의 NameText에 타겟표시 해주는 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void TargetCheck()
    {
        ObjectInfo[] a_ObjectInfo = FindObjectsOfType<ObjectInfo>();

        for(int ii = 0; ii < a_ObjectInfo.Length; ii++)
        {
            if(a_ObjectInfo[ii].gameObject == m_AttTarget) //타겟이면
            {
                a_ObjectInfo[ii].TargetMarkOn();
            }
            else if(a_ObjectInfo[ii].gameObject != m_AttTarget) //타겟이 아니면
            {
                a_ObjectInfo[ii].TargetMarkOff();
            }
        }
    }
```
</details>

---

* #2-5)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Hero/CameraCtrl.cs))애니메이션 이벤트 함수를 통해 소울 소환 공격 및 타겟에게 데미지를 입히는 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void Attack_Event() //애니메이션 이벤트 함수
    {
        if (m_AttTarget == null)
            return;

        if(m_AttTarget.GetComponent<ObjectInfo>().m_IsDie == true)
        {
            m_AttTarget = null;
            AnimationChange("Idle");
            return;
        }

        if (m_SoulPrefab != null)
        {   //소울 소환
            if (m_AttTarget != null)
            {
                GameObject a_SoulPrefab = Instantiate(m_SoulPrefab, m_SoulStartPos.position, Quaternion.identity);
                a_SoulPrefab.GetComponent<Rigidbody>().velocity = Vector3.up;
                Destroy(a_SoulPrefab, 3.0f);
            }
        }

        GameObject a_BasicHitEff = Instantiate(m_BasicHitEffect);
        if (gameObject.name.Contains("Knight"))
            a_BasicHitEff.transform.position = transform.position + transform.forward;
        else
            a_BasicHitEff.transform.position = m_AttTarget.transform.position + transform.forward;

        Destroy(a_BasicHitEff, 1.0f);

        // 사운드
        if (m_MagicClaw == true)
            Sound_Mgr.Instance.PlayEffSound("MagicClaw", 1.0f);
        else if (m_Charm == true)
            Sound_Mgr.Instance.PlayEffSound("Charm", 7.0f);
        else
            Sound_Mgr.Instance.PlayEffSound("BasicAttack", 1.0f);
        // 사운드

        #region//스킬관련
        //강타 스킬 관련
        if (m_KnightStrokeHit == true)
        {
            ObjectInfo a_ObjectInfo = m_AttTarget.GetComponent<ObjectInfo>();
            a_ObjectInfo.m_PhotonView.RPC("Stun", RpcTarget.AllBuffered);

            m_KnightStrokeHit = false;

            if (m_KnightStrokeHitObj != null)
                m_KnightStrokeHitObj.SetActive(false);

            //스킬 슬롯에 있는 강타스킬을 전부찾아 쿨타임 돌려주기
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                Knight_StrokeHit a_StrokeHit = m_Skills[ii].GetComponentInChildren<Knight_StrokeHit>();
                if (a_StrokeHit != null)
                {
                    Knight_StrokeHit.m_CoolTime = 10.0f;
                    a_StrokeHit.m_RefTimer = 10.0f;
                }
            }
            //스킬 슬롯에 있는 강타스킬을 전부찾아 쿨타임 돌려주기

            m_AddDmg = GlobalValue.m_Att * 0.3f;
        }
        //강타 스킬 관련

        //매직클로 스킬 관련
        if (m_MagicClaw == true)
        {
            m_MagicClaw = false;

            GameObject a_MagicClawObj = Instantiate(m_MagicClawObj) as GameObject;
            a_MagicClawObj.transform.position = m_AttTarget.transform.position + transform.forward;

            if (m_MagicClawObj2 != null)
                m_MagicClawObj2.SetActive(false);

            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                Mage_Claw a_Mage_Claw = m_Skills[ii].GetComponentInChildren<Mage_Claw>();
                if (a_Mage_Claw != null)
                {
                    Mage_Claw.m_CoolTime = 10.0f;
                    a_Mage_Claw.m_RefTimer = 10.0f;
                }
            }
            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기

            m_AddDmg = GlobalValue.m_Att * 0.6f;
        }
        //매직클로 스킬 관련

        //매혹 스킬 관련
        if (m_Charm == true)
        {
            m_Charm = false;

            GameObject a_CharmObj = Instantiate(m_CharmObj2) as GameObject;
            a_CharmObj.transform.position = m_AttTarget.transform.position;
            a_CharmObj.transform.SetParent(m_AttTarget.transform);
            a_CharmObj.transform.gameObject.SetActive(true);
            Destroy(a_CharmObj, 5.0f);

            if (m_CharmObj != null)
                m_CharmObj.SetActive(false);

            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기
            for (int ii = 0; ii < m_Skills.Length; ii++)
            {
                Healer_Charm a_Healer_Charm = m_Skills[ii].GetComponentInChildren<Healer_Charm>();
                if (a_Healer_Charm != null)
                {
                    Healer_Charm.m_CoolTime = 10.0f;
                    a_Healer_Charm.m_RefTimer = 10.0f;
                }
            }
            //스킬 슬롯에 있는 매직클로스킬을 전부찾아 쿨타임 돌려주기

            if (m_AttTarget.gameObject.tag == "Monster")
            {
                if (!m_AttTarget.gameObject.name.Contains("Boss"))
                {
                    MonsterCtrl a_Monster = m_AttTarget.GetComponent<MonsterCtrl>();
                    a_Monster.m_Def = a_Monster.m_Def / 2;
                    a_Monster.m_IsCharmed = true;
                }
            }
        }
        //매혹 스킬 관련
        #endregion

        if (m_AttTarget.gameObject.tag == "Monster")
        {
            if (m_AttTarget.gameObject.name.Contains("Boss"))
            {
                BossCtrl a_BossCtrl = m_AttTarget.GetComponent<BossCtrl>();
                float a_Dmg = GlobalValue.m_Att + m_AddDmg - a_BossCtrl.m_Def;
                if (a_Dmg <= 0.0f)
                    a_Dmg = 0.0f;
                if (a_BossCtrl != null)
                {
                    a_BossCtrl.m_PhotonView.RPC("TakeDamage",RpcTarget.AllBuffered,a_Dmg, m_PhotonView.Owner.NickName);
                    a_BossCtrl.StopAllCoroutines();
                }
            }
            else
            {
                MonsterCtrl a_Target = m_AttTarget.GetComponent<MonsterCtrl>();
                float a_Dmg = (GlobalValue.m_Att + m_AddDmg - a_Target.m_Def);
                if (a_Dmg <= 0.0f)
                    a_Dmg = 0.0f;
                if (a_Target != null)
                    a_Target.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg);
            }
        }
        else if (m_AttTarget.gameObject.tag == "Hero")
        {
            HeroCtrl a_Hero = m_AttTarget.GetComponent<HeroCtrl>();
            float a_Dmg = GlobalValue.m_Att + m_AddDmg;
            if (a_Dmg <= 0.0f)
                a_Dmg = 0.0f;
            if (a_Hero != null)
                a_Hero.m_PhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, a_Dmg);
        }

        m_AddDmg = 0.0f; //초기화
    }
```
</details>

---

* #3-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/ItemPanel.cs)) 인게임 시작 시 아이템 전체 데이터베이스, 보유 아이템, 착용아이템 Load 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    IEnumerator Load()
    {
        yield return new WaitUntil(() => GameMgr.Inst.m_Player != null);

        string[] line = m_ItemDB.text.Substring(0, m_ItemDB.text.Length - 1).Split("\n");
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            AllItemList.Add(new ItemData(row[0], row[1], row[2], row[3], int.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]),
                                         int.Parse(row[7]), row[8], int.Parse(row[9]), row[10] == "TRUE"));
        }

        //string Jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemText.txt");
        if (PlayfabMgr.inst.m_Data.TryGetValue("Item", out string a_Item))
        {//PlayFab에 저장되어 있는 나의 아이템 리스트 가져오기
            MyItemList = JsonConvert.DeserializeObject<List<ItemData>>(a_Item);
        }

        TabClick("All");

        if (CurItemList != null && CurItemList.Count > 0)
        {
            for (int ii = 0; ii < CurItemList.Count; ii++)
            {//착용되어 있던 아이템 세팅
                if (CurItemList[ii].m_IsUsing == true)
                {
                    SlotClick(ii);
                }
            }
        }

        GameMgr.Inst.UIRefresh();
    }
```
</details>

---

* #3-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/ItemPanel.cs)) 아이템 획득 및 착용에 따른 보유 아이템 리스트 및 각 아이템 정보 저장
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void Save()
    {
        string Jdata = JsonConvert.SerializeObject(MyItemList);
        PlayfabMgr.inst.SetPlayerData("Item", Jdata);
        //File.WriteAllText(Application.dataPath + "/Resources/MyItemText.txt", Jdata);

        TabClick(m_CurItemType);
    }
```
</details>

---

* #3-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/ItemPanel.cs)) 원하는 아이템 종류 나열해 보여주는 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void TabClick(string a_TabName)
    {
        //현재 아이템 리스트에 클릭한 타입만 추가
        m_CurItemType = a_TabName;
        if (m_CurItemType == "All")
        {
            if (MyItemList != null && MyItemList.Count > 0)
                CurItemList = MyItemList;
            else
            {
                MyItemList = new List<ItemData>();
                CurItemList = MyItemList;
            }
        }
        else
        {
            Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);
            CurItemList = MyItemList.FindAll(a_Item => a_Item.m_ItemType == a_TabName);
        }
        //현재 아이템 리스트에 클릭한 타입만 추가
        if (CurItemList != null && CurItemList.Count > 0)
        {
            //슬롯과 텍스트 보이게 하기
            for (int i = 0; i < CurItemList.Count; i++)
            {
                m_ItemSlot[i].SetActive(i < CurItemList.Count);
                //아이템슬롯에 정보저장
                Item a_Item = m_ItemSlot[i].GetComponent<Item>();

                a_Item._Grade = CurItemList[i].m_Grade;
                if (a_Item._Grade == "고급")
                    a_Item.m_BackgroundImg.sprite = m_GreenSprite;
                else if (a_Item._Grade == "희귀")
                    a_Item.m_BackgroundImg.sprite = m_BlueSprite;
                a_Item._Name = CurItemList[i].m_Name;
                a_Item._Type = CurItemList[i].m_DetailType;
                a_Item._Att = CurItemList[i].m_Att;
                a_Item._Def = CurItemList[i].m_Def;
                a_Item._Acc = CurItemList[i].m_Acc;
                a_Item._Hp = CurItemList[i].m_Hp;
                a_Item._SpawnArea = CurItemList[i].m_SpawnArea;
                a_Item._HaveCount = CurItemList[i].m_HaveCount;
                a_Item._IsUsing = CurItemList[i].m_IsUsing;
                a_Item.m_UsingImg.gameObject.SetActive(a_Item._IsUsing);

                bool isExist = i < CurItemList.Count;
                if (isExist)
                    a_Item.m_ItemIconImg.sprite = m_ItemIconSprite[AllItemList.FindIndex(x => x.m_Name == CurItemList[i].m_Name)];
                //아이템슬롯에 정보저장
            }
            //슬롯과 텍스트 보이게 하기

            for (int ii = CurItemList.Count; ii < MyItemList.Count; ii++)
            { 
                m_ItemSlot[ii].SetActive(false);
            }
        }
        else
        {//현재 탭에 갖고있는 아이템이 없으면
            for (int iii = 0; iii < m_ItemSlot.Length; iii++)
            {
                m_ItemSlot[iii].SetActive(false);
            }
        }

        //Tab버튼 이미지 바꿔주기
        int a_TabNum = -1;
        switch (m_CurItemType)
        {
            case "All": a_TabNum = 0; break;
            case "Armor": a_TabNum = 1; break;
            case "Weapon": a_TabNum = 2; break;
            case "Etc": a_TabNum = 3; break;
        }

        for (int ii = 0; ii < m_TypeTabBtn.Length; ii++)
        {
            m_TypeTabBtn[ii].GetComponent<Image>().sprite = ii == a_TabNum ? TabSelectSprite : TabIdleSprite;
        }
        //Tab버튼 이미지 바꿔주기

        if (MyItemList.Count == m_MaxItemSlot) //장비창에 아이템이 가득차 있으면
            m_MyItemCountText.text = "<color=red>" + MyItemList.Count + "/" + m_MaxItemSlot + "</color>";
        else
            m_MyItemCountText.text = MyItemList.Count + "/" + m_MaxItemSlot;
    }
```
</details>

---

* #3-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/ItemPanel.cs)) 아이템에 마우스포인트를 가져다 댔을 때 해당 아이템의 정보 표시해주는 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    bool IsItemSlot(Item a_Item) //아이템위에 마우스가 있는지 확인하는 함수
    {
        Vector3[] v = new Vector3[4];
        a_Item.GetComponent<RectTransform>().GetWorldCorners(v);

        if (v[0].x <= Input.mousePosition.x && Input.mousePosition.x <= v[2].x &&
            v[0].y <= Input.mousePosition.y && Input.mousePosition.y <= v[2].y)
        {
            return true;
        }

        return false;
    }

    void MousePointOnItem() //마우스를 아이템 위로 갖다댔을 때
    {
        int a_ItemIdx = -1;

        Item a_Item;

        for (int ii = 0; ii < m_ItemSlot.Length; ii++)
        {
            a_Item = m_ItemSlot[ii].GetComponent<Item>();

            if (IsItemSlot(a_Item) == true)
            {
                if (a_Item.gameObject.activeSelf == false)
                    return;

                if (a_Item._Grade == "고급")
                    m_IDGradeText.text = "<color=green>[등급 : " + a_Item._Grade + "]</color>";
                else if(a_Item._Grade == "희귀")
                    m_IDGradeText.text = "<color=blue>[등급 : " + a_Item._Grade + "]</color>";

                m_IDNameText.text = a_Item._Name;

                m_IDClassText.text = a_Item._Type;

                m_IDEffectText.text = "";
                if (a_Item._Att != 0)
                {
                    m_IDEffectText.text += "공격력 : + " + a_Item._Att + "\n" +
                                           "명중 : + " + a_Item._Acc;
                }
                else
                {
                    m_IDEffectText.text += "방어력 : + " + a_Item._Def + "\n" +
                                           "체력 : + " + a_Item._Hp;
                }

                m_IDSpawnAreaText.text = "<color=orange>[" + a_Item._SpawnArea + "]</color>";

                m_ItemDescriptionWd.transform.position = Input.mousePosition;
                m_ItemDescriptionWd.SetActive(true);

                a_ItemIdx = ii;
                break;
            }
        }

        if (a_ItemIdx == -1) //마우스를 땐 곳이 스킬슬롯이 아니면(없애기)
        {
            m_ItemDescriptionWd.SetActive(false);
        }
    }
```
</details>

---

* #3-5)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/ItemPanel.cs)) 장비 착용 및 해제
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void SlotClick(int a_SlotNum)
    {
        ItemData a_CurItem = CurItemList[a_SlotNum];
        Item a_ItemSlot = m_ItemSlot[a_SlotNum].GetComponent<Item>();

        if (a_CurItem.m_ItemType == "Weapon")
        {
            InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Weapon], a_ItemSlot);
            Sound_Mgr.Instance.PlayEffSound("Weapon", 1.0f);
        }
        else if (a_CurItem.m_ItemType == "Armor")
        {
            if (a_CurItem.m_DetailType == "머리장식(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Helmet], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "갑옷(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Top], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "반지(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Ring], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Ring", 1.0f);
            }
            else if (a_CurItem.m_DetailType == "신발(공용)")
            {
                InputItem(a_CurItem, m_WearingSlot[(int)ItemType.Boots], a_ItemSlot);
                Sound_Mgr.Instance.PlayEffSound("Cloth", 1.0f);
            }
        }
        //else if(a_CurItem.m_ItemType == "소모품")
        //{

        //}

        a_CurItem.m_IsUsing = true;
        GameMgr.Inst.UIRefresh();
        Save();
    }

    void InputItem(ItemData a_Item,Item a_WearingSlot, Item a_ItemSlot) //a_Item : 착용할 아이템 , a_WearingSlot : 착용 되어있는 아이템, a_ItemSlot : 착용할 아이템의 슬롯
    {
        // 착용되어 있는 아이템이 있으면 제거
        for (int ii = 0; ii < CurItemList.Count; ii++)
        {
            if (CurItemList[ii].m_IsUsing == true && a_Item.m_DetailType == CurItemList[ii].m_DetailType)
                CurItemList[ii].m_IsUsing = false;
        }

        if (a_Item.m_ItemType == "Weapon")
        {
            GlobalValue.m_Att -= a_WearingSlot._Att;
            GlobalValue.m_Acc -= a_WearingSlot._Acc;
        }
        else
        {
            GlobalValue.m_Def -= a_WearingSlot._Def;
            GameMgr.Inst.m_Player.m_MaxHp -= a_WearingSlot._Hp;
        }
        // 착용되어 있는 아이템이 있으면 제거

        //UI 및 정보 저장
        a_WearingSlot.m_BackgroundImg.gameObject.SetActive(true);
        if (a_Item.m_Grade == "고급")
            a_WearingSlot.m_BackgroundImg.sprite = m_GreenSprite;
        else
            a_WearingSlot.m_BackgroundImg.sprite = m_BlueSprite;
        a_WearingSlot.m_ItemIconImg.gameObject.SetActive(true);
        a_WearingSlot.m_ItemIconImg.sprite = a_ItemSlot.m_ItemIconImg.sprite;
        a_WearingSlot._Grade = a_Item.m_Grade;
        a_WearingSlot._Name = a_Item.m_Name;
        a_WearingSlot._Type = a_Item.m_DetailType;
        a_WearingSlot._Att = a_Item.m_Att;
        a_WearingSlot._Acc = a_Item.m_Acc;
        a_WearingSlot._Def = a_Item.m_Def;
        a_WearingSlot._Hp = a_Item.m_Hp;
        a_WearingSlot._SpawnArea = a_Item.m_SpawnArea;
        a_WearingSlot._HaveCount = a_Item.m_HaveCount;
        a_WearingSlot._IsUsing = true;
        a_ItemSlot.m_UsingImg.gameObject.SetActive(true);
        //UI 및 정보 저장

        //능력치
        if (a_Item.m_ItemType == "Weapon")
        {
            GlobalValue.m_Att += a_Item.m_Att;
            GlobalValue.m_Acc += a_Item.m_Acc;
        }
        else
        {
            GlobalValue.m_Def += a_Item.m_Def;
            GameMgr.Inst.m_Player.m_MaxHp += a_Item.m_Hp;
            GameMgr.Inst.m_Player.m_CurHp += a_Item.m_Hp;
        }
        //능력치
    }

    public void WearingSlotClick(int a_SlotNum) //착용아이템 제거
    {
        Item a_CurItem = m_WearingSlot[a_SlotNum];

        if (a_CurItem._IsUsing == false)
            return;

        if(a_CurItem._Type == "머리장식(공용)" || a_CurItem._Type == "갑옷(공용)" ||
                a_CurItem._Type == "반지(공용)" || a_CurItem._Type == "신발(공용)")
        {
            GlobalValue.m_Def -= a_CurItem._Def;
            GameMgr.Inst.m_Player.m_MaxHp -= a_CurItem._Hp;
        }
        else
        {
            GlobalValue.m_Att -= a_CurItem._Att;
            GlobalValue.m_Acc -= a_CurItem._Acc;
        }

        a_CurItem.m_BackgroundImg.gameObject.SetActive(false);
        a_CurItem.m_ItemIconImg.gameObject.SetActive(false);

        for (int ii = 0; ii < MyItemList.Count; ii++)
        {
            if (MyItemList[ii].m_Name == a_CurItem._Name)
                MyItemList[ii].m_IsUsing = false;
        }

        for(int ii = 0; ii < m_ItemSlot.Length; ii++)
        {
            if (a_CurItem._Name == m_ItemSlot[ii].GetComponent<Item>()._Name)
                m_ItemSlot[ii].GetComponent<Item>().m_UsingImg.gameObject.SetActive(false);
        }

        //착용아이템 정보 초기화
        a_CurItem._Grade = "";
        a_CurItem._Name = "";
        a_CurItem._Type = "";
        a_CurItem._Att = 0;
        a_CurItem._Def = 0;
        a_CurItem._Acc = 0;
        a_CurItem._Hp = 0;
        a_CurItem._SpawnArea = "";
        a_CurItem._HaveCount = 0;
        a_CurItem._IsUsing = false;

        GameMgr.Inst.UIRefresh();
        Save();
    }

```
</details>

---

* #3-6)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/ItemPanel.cs)) 아이템 획득
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void GainItem(MonsterCtrl a_Monster)
    {
        if (ItemPanel.Inst.MyItemList.Count >= ItemPanel.Inst.m_MaxItemSlot)
            return;

        int a_RdmIdx = Random.Range(0, 1); //(0, 2); (몬스터 처치 시 원하는 확률에 따라 아이템을 획득 여부 결정)
        if (a_RdmIdx == 0) // 100% 확률로 아이템 드랍되도록 설정
        {
            int a_RandomIdx = Random.Range(0, AllItemList.Count);
            ItemData a_GainItem = AllItemList[a_RandomIdx];
            if (a_GainItem.m_SpawnArea == a_Monster.m_SpawnArea) //50%, 총 아이템획득 확률 50%
            {
                if (MyItemList == null) //아이템 첫획득
                    MyItemList = new List<ItemData>() { a_GainItem };
                else
                    MyItemList.Add(a_GainItem);

                if (MyItemList.Count == m_MaxItemSlot)
                    m_MyItemCountText.text = "<color=red>" + MyItemList.Count + "/" + m_MaxItemSlot + "</color>";
                else
                    m_MyItemCountText.text = MyItemList.Count + "/" + m_MaxItemSlot;

                for (int ii = 0; ii < AllItemList.Count; ii++)
                {
                    if (a_GainItem.m_Name == m_ItemIconImgList[ii].name)
                    {
                        m_IconImg.sprite = m_ItemIconImgList[ii];
                    }
                }
                if (a_GainItem.m_Grade == "고급")
                    m_BackImg.sprite = m_GreenSprite;
                else
                    m_BackImg.sprite = m_BlueSprite;

                m_GainItemRoot.gameObject.SetActive(true); 
                TabClick(m_CurItemType);
                Save();
            }
        }


    }
```
</details>

---

* #4-1) Knight Class 스킬
  
<details>
  
<summary>동영상</summary>

</details>

---

* #4-2) Mage Class 스킬
  
<details>
  
<summary>동영상</summary>

</details>

---

* #4-3) Healer Class 스킬
  
<details>
  
<summary>동영상</summary>

</details>

---

* #4-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/ManagerBox/SkillPanel.cs)) 스킬 장착
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void UseSkillBtnClick(GameObject a_Skill)
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        for (int ii = 0;  ii < m_Skill.Length; ii++)
        {//비어있는 스킬 창을 찾아서 넣어주기
            Button a_SkillBtn = m_Skill[ii].GetComponentInChildren<Button>();

            if(a_SkillBtn == null)
            {//스킬창이 비어있으면
                GameObject a_SkillObj = Instantiate(a_Skill) as GameObject;
                a_SkillObj.transform.SetParent(m_Skill[ii], false);

                return;
            }
        }
    }
```
</details>

---

* #4-5)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Skill/SkillSlotMgr.cs)) 스킬창 편집 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Update()
    {
        if (m_Editing == true)
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                MouseClickDown();
            }

            if (Input.GetMouseButton(0) == true)
            {
                MouseClick();
            }

            if (Input.GetMouseButtonUp(0) == true)
            {
                MouseClickUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            EditOnOff();
        }
    }

    void MouseClickDown() //마우스 클릭했을때
    {
        for (int ii = 0; ii < m_SkillSlot.Length; ii++)
        {
            if (IsSkillSlot(m_SkillSlot[ii]) == true) //클릭한 곳이 스킬슬롯 이면
            {
                m_SaveIdx = ii;

                Skill a_Skill = m_SkillSlot[ii].GetComponentInChildren<Skill>();
                if (a_Skill != null) //클릭한슬롯에 스킬이 담겨져있으면
                {
                    Transform a_MouseImg = m_MouseObj.transform.Find("MouseImg");
                    if (a_MouseImg != null)
                        a_MouseImg.GetComponent<Image>().sprite =  a_Skill.m_SkillIconImg.sprite;

                    m_MouseObj.gameObject.SetActive(true);

                    break;
                }
                else //클릭한슬롯이 빈칸이면
                {
                    m_SaveIdx = -1;
                }
            }
        }
    }

    void MouseClick() //마우스 클릭중
    {
        if (m_SaveIdx >= 0)
            m_MouseObj.transform.position = Input.mousePosition;
    }

    void MouseClickUp() //마우스 클릭을 땟을때
    {
        if (m_SaveIdx < 0)
            return;

        int a_InstallSlot = -1;

        for (int ii = 0; ii < m_SkillSlot.Length; ii++)
        {
            if (IsSkillSlot(m_SkillSlot[ii]) == true) //마우스를 땐 곳이 스킬슬롯이면(옮기기)
            {
                Skill a_Skill = m_SkillSlot[m_SaveIdx].GetComponentInChildren<Skill>();
                a_Skill.transform.SetParent(m_SkillSlot[ii].transform, false);

                a_InstallSlot = ii;
                break;
            }
        }

        if(a_InstallSlot == -1) //마우스를 땐 곳이 스킬슬롯이 아니면(없애기)
        {
            Skill a_Skill = m_SkillSlot[m_SaveIdx].GetComponentInChildren<Skill>();
            Destroy(a_Skill.gameObject);
        }

        m_SaveIdx = -1;
        m_MouseObj.gameObject.SetActive(false);
    }

    bool IsSkillSlot(SkillSlot a_SkillSlot) //스킬슬롯위에 있는지 확인하는 함수
    {
        Vector3[] v = new Vector3[4];
        a_SkillSlot.GetComponent<RectTransform>().GetWorldCorners(v);

        if (v[0].x <= Input.mousePosition.x && Input.mousePosition.x <= v[2].x &&
            v[0].y <= Input.mousePosition.y && Input.mousePosition.y <= v[2].y)
        {
            return true;
        }

        return false;
    }
```
</details>

---

* #5-1) 소울 뽑기 기능 구현
  
<details>
  
<summary>동영상</summary>

</details>

---

* #5-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Soul/SoulCtrl.cs)) 소울 이동
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Update()
    {
        if(m_Target != null && m_ObjInfo.m_IsDie == false)
        {
            m_TargetPos = m_Target.transform.position;

            if (m_CurSpeed <= m_MaxSpeed)
                m_CurSpeed += m_MaxSpeed * Time.deltaTime;

            transform.position += transform.up * m_CurSpeed * Time.deltaTime;

            Vector3 a_SoulDir = (m_TargetPos - transform.position).normalized
                                  + new Vector3(0, 0.6f, 0);

            transform.up = Vector3.Lerp(transform.up, a_SoulDir, 0.6f);
        }
        else if(m_Target != null && m_ObjInfo.m_IsDie == true)
        {
            if (transform.localEulerAngles.y > 1.0f)
                transform.position += transform.up * m_CurSpeed * Time.deltaTime;
        }
    }
```
</details>

---

* #5-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Soul/SoulCtrl.cs)) 소울 공격
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    IEnumerator Attack()
    {
        yield return new WaitUntil(() => transform.localPosition.y > 1.0f); //소울 위치가 1m이상 올라갈 때까지

        if (m_Hero.m_AttTarget == null) //타겟이 없으면
            Destroy(gameObject);
        else
        {
            m_Target = m_Hero.m_AttTarget.transform;

            m_ObjInfo = m_Hero.m_AttTarget.GetComponent<ObjectInfo>();
            if(m_ObjInfo != null)
            {
                if (m_ObjInfo.m_IsDie == true) //타겟이 사망 상태이면
                    Destroy(gameObject);
            }
        }
    }
```
</details>

---

* #6-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/BossCtrl.cs)) 보스 상태 체크
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Update()
    {
        if (m_Target != null) //전투,추격,Returning
        {
            BossStateUpdate();

            if (m_NavMeshAgent.destination != null)//Wandering중이였으면 중지
                m_NavMeshAgent.ResetPath();

            //보스 기본 공격 딜레이 타이머
            if(m_AttDelayTimer > 0.0f)
            {
                m_AttDelayTimer -= Time.deltaTime;
                if(m_AttDelayTimer < 0.0f)
                {
                    AnimationChange("Attack");
                    m_AttDelayTimer = 4.0f;
                }
            }
            //보스 기본 공격 딜레이 타이머
        }
        else //Wandering
        {
            if (m_PhotonView.IsMine == true) //PhotonNetwork.InstantiateRoomObject로 생성했기 때문에 Room에서 제어
            {
                WanderingAnimationUpdate();

                StartCoroutine(Wandering());
            }
            else //Room안에 유저들 입장에서
            {
                if (10.0f < (transform.position - currPos).magnitude)
                {   //중계 받은 좌표와 현재 좌표의 거리차가 10m 이상이면 즉시 보정
                    transform.position = currPos;
                }
                else
                {
                    //원격 플레이어의 위치를 수신받은 위치까지 부드럽게 이동시킴
                    transform.position = Vector3.Lerp(transform.position,
                                                    currPos, Time.deltaTime * 10.0f);
                }

                //원격 플레이어의 위치를 수신받은 각도만큼 부드럽게 회전시킴
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                currRot, Time.deltaTime * 10.0f);

                NotMineAnimUpdate(animState);
            }
        }
    }
```
</details>

---

* #6-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/BossCtrl.cs)) 보스의 타겟이 없는 Wandering상태 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public IEnumerator Wandering()
    {
        while (m_WanderingStart == true)
        {
            float a_RanX = m_SpawnPos.x + Random.Range(-20, 20);
            float a_RanZ = m_SpawnPos.z + Random.Range(-20, 20);

            m_TargetPos = new Vector3(a_RanX, 100, a_RanZ);

            if (Physics.Raycast(m_TargetPos, Vector3.down, out m_HitInfo, Mathf.Infinity))
            {
                if (m_HitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    m_NavMeshAgent.SetDestination(m_HitInfo.point);                  //목적지 설정
                    m_TargetDist = (transform.position - m_HitInfo.point).magnitude; //거리
                    m_MoveTime = m_TargetDist / m_NavMeshAgent.speed;                //시간
                }
            }

            m_WanderingStart = false;
            yield return new WaitForSeconds(m_MoveTime + 10.0f); //도착후 10초후에 다시 Wandering
            m_WanderingStart = true;
        }
    }
```
</details>

---

* #6-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/BossCtrl.cs)) 보스의 타겟이 존재할때 보스상태
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void BossStateUpdate()
    {
        if (m_Target == null)
            return;

        //이동 및 회전 관련
        m_TargetPPos = m_Target.transform.position;
        m_BossPos = transform.position;
        m_TargetCacVec = new Vector3(m_TargetPPos.x, 0, m_TargetPPos.z) - new Vector3(m_BossPos.x, 0, m_BossPos.z);
        m_TargetDir = m_TargetCacVec.normalized;  //타겟 방향
        m_TargetPDist = m_TargetCacVec.magnitude; //타겟 거리
        //이동 및 회전 관련

        //return 관련
        m_SpawnCacVec = new Vector3(m_SpawnPos.x, 0, m_SpawnPos.z) - new Vector3(m_BossPos.x, 0, m_BossPos.z);
        m_SpawnDir = m_SpawnCacVec.normalized;    //스폰 방향
        m_SpawnPosDist = m_SpawnCacVec.magnitude; //스폰위치 거리

        if (m_SpawnPosDist > m_MaxMoveDist) //초기스폰위치로부터 60m이상 밖으로 나가면
        {
            m_IsReturning = true;
        }
        else if(m_SpawnPosDist < 1.0f) //초기스폰위치 도착하면
        {
            m_IsReturning = false;
        }
        //return 관련

        if (m_IsReturning == true)
        {//초기스폰위치와 거리가 멀어져 되돌아가는 상태 일때
            m_TargetRot = Quaternion.LookRotation(m_SpawnDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

            transform.position += m_SpawnDir * Time.deltaTime * m_ReturnSpeed;
            AnimationChange("Move");

            if (m_SpawnPosDist < 1.1f)
                AnimationChange("Idle");
        }
        else
        {
            if (m_TargetPDist > m_AgroDist) // 타겟과의거리가 어그로 사거리보다 멀어지면(Wandering상태로 돌려줌)
            {
                AnimationChange("Idle");
                m_WanderingStart = true;
                m_Target = null;
            }
            else if (m_AttDist < m_TargetPDist && m_TargetPDist <= m_AgroDist) //타겟과의 거리가 어그로 사거리 안에있고 공격 사거리보다 멀면(타겟을 쫓아가는 상태)
            {
                if (m_IsAttacking == false)
                {
                    m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                    transform.position += m_TargetDir * Time.deltaTime * m_MoveSpeed;
                    AnimationChange("Move");
                }
            }
            else if (m_TargetPDist <= m_AttDist) //타겟과의 거리가 공격사거리 내에 있으면(타겟을 공격중인 상태)
            {
                if (m_TargetPDist > m_AttDist - 1.0f) // 공격사거리보다 1m더 가까이와서 때리도록 설정
                {
                    if (m_IsAttacking == false)
                    {
                        m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                        transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                        transform.position += m_TargetDir * Time.deltaTime * m_MoveSpeed;
                    }

                    if (m_IsAttacking == false && m_AttDelayTimer <= 0.0f)
                        m_IsAttacking = true;
                }

                m_TargetRot = Quaternion.LookRotation(m_TargetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, m_TargetRot, Time.deltaTime * m_RotSpeed);

                if (m_AttDelayTimer <= 0.0f)
                    AnimationChange("Attack");
            }
        }
    }
```
</details>

---

* #6-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/BossCtrl.cs))보스의 기본공격후 딜레이를 주고 3번째 기본공격마다 랜덤패턴을 발생시키기 위한 애니메이션 이벤트 함수
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void AttackEndEvent() //기본공격후 딜레이를 주고 3번째 기본공격마다 랜덤패턴을 발생시키기 위한 이벤트 함수
    {
        if (m_LastAnimState == "Attack" && m_AttDelayTimer > 0.0f)
        {
            AnimationChange("Idle");
        }

        float a_Dmg = (40 - GlobalValue.m_Def / 2) / 2;
        if (a_Dmg < 0)
            a_Dmg = 0;

        if (m_Target != null)
            m_Target.m_PhotonView.RPC("TakeDamage", Photon.Pun.RpcTarget.AllBuffered, a_Dmg);

        m_AttDelayTimer = 4.0f;
        m_AttCount += 1;

        if(m_PhotonView.IsMine)
        {
            if (m_AttCount == 2)
            {// 3번째 랜덤패턴에 어떤패턴나올지 미리결정
                m_RndPatternIdx = Random.Range(1, 3);
            }


            if (m_AttCount == 3)
            {
                if (m_RndPatternIdx == 1)  //패턴1
                {
                    Pattern1();
                }
                else if (m_RndPatternIdx == 2) //패턴2
                {
                    Pattern2();
                }

                m_AttCount = 0;
            }
        }
        else
        {//OnPhotonSrealizeView를 통해 어떤패턴이 나올지 넘겨받는다
            if (m_AttCount == 3)
            {
                if (rndPatterIdx == 1)  //패턴1
                {
                    Pattern1();
                }
                else if (rndPatterIdx == 2) //패턴2
                {
                    Pattern2();
                }

                m_AttCount = 0;
            }
        }

        m_IsAttacking = false;
    }
```
</details>

---

* #6-5)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/BossCtrl.cs))보스패턴1
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Pattern1()
    {
        GameObject[] a_Heroes = GameObject.FindGameObjectsWithTag("Hero");

        for (int ii = 0; ii < a_Heroes.Length; ii++)
        {
            Vector3 a_HeroPos = a_Heroes[ii].transform.position;
            GameObject a_MagicBoard = Instantiate(m_MagicBoardPrefab) as GameObject;
            a_MagicBoard.transform.position = new Vector3(a_HeroPos.x, a_HeroPos.y + 0.1f, a_HeroPos.z);
        }
    }

    void Bomb()
    {
        GameObject a_ExplosionObj = Instantiate(m_ExplosionEff, transform.position, Quaternion.identity);
        Destroy(a_ExplosionObj, 1.0f);

        Collider[] colls = Physics.OverlapSphere(transform.position, 1.5f);
        HeroCtrl a_Hero;

        foreach (Collider coll in colls)
        {
            a_Hero = coll.GetComponent<HeroCtrl>();
            if (a_Hero == null)
                continue;

            float a_Dmg = (100 - GlobalValue.m_Def / 2) / 2;
            if (a_Dmg < 0.0f)
                a_Dmg = 0.0f;

            a_Hero.m_PhotonView.RPC("TakeDamage", Photon.Pun.RpcTarget.AllBuffered, a_Dmg);
        }

        Sound_Mgr.Instance.PlayEffSound("Pattern1", 8.0f);

        Destroy(this.gameObject);
    }
```
</details>

---

* #6-6)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/BossCtrl.cs))보스패턴2
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Pattern2()
    {
        if (m_Target == null)
            return;

        AnimationChange("Skill2");
    }

    void Pattern2_Event()
    {
        GameObject a_MagicBoardObj = Instantiate(m_MagicBoardPrefab2);
        a_MagicBoardObj.transform.position = new Vector3(m_TargetPPos.x, m_TargetPPos.y + 0.1f, m_TargetPPos.z);
        GameObject a_ElecBall = Instantiate(m_ElectronicBall);
        a_ElecBall.transform.position = new Vector3(m_TargetPPos.x, m_TargetPPos.y + 1.4f, m_TargetPPos.z);

        Collider[] colls = Physics.OverlapSphere(m_TargetPPos, 10.0f);

        foreach(Collider coll in colls)
        {
            HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();
            if (a_Hero == null)
                continue;

            Vector3 a_HeroPos = a_Hero.transform.position;
            Vector3 a_BossPos = transform.position;
            Vector3 a_CacVec = new Vector3(a_HeroPos.x ,0,a_HeroPos.z) - new Vector3(a_BossPos.x , 0, a_BossPos.z);
            float a_Dist = a_CacVec.magnitude;

            float a_Dmg;
            if (a_Dist < 1.0f)
                a_Dmg = (100 - GlobalValue.m_Def / 2) / 2;
            else if (a_Dist < 5.0f && a_Dist >= 1.0f)
                a_Dmg = (80 - GlobalValue.m_Def / 2) / 2;
            else
                a_Dmg = (60 - GlobalValue.m_Def / 2) / 2;

            if (a_Dmg < 0)
                a_Dmg = 0;

            a_Hero.m_PhotonView.RPC("TakeDamage",Photon.Pun.RpcTarget.AllBuffered,a_Dmg);
        }
    }
```
</details>

---

* #7-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PartySystemMgr.cs)) 파티 초대 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void InviteBtnClick()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        if (m_AlreadyHasParty == true) //파티에 소속되어 있는 상태이면
            return;

        Player[] a_Players = PhotonNetwork.PlayerListOthers; //나를 제외한 현재방에 참가한 플레이어 리스트

        for (int ii = 0; ii < a_Players.Length; ii++)
        {
            if (m_AroundPlayerInviteBtn[ii].activeSelf == false)
                m_AroundPlayerInviteBtn[ii].SetActive(true);

            m_AroundPlayerInviteBtn[ii].GetComponent<AroundPlayerInviteBtn>().m_Player = a_Players[ii];

            m_AroundPlayerInviteBtn[ii].GetComponentInChildren<Text>().text
                = a_Players[ii].NickName; //닉네임 표시
        }
    }
```
</details>

---

* #7-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PartySystemMgr.cs)) 파티초대(PunRPC를 통해 Local PC에서 Local이 아닌 PC들에게 초대함수 호출)
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    [PunRPC]
    void RecieveInviteMsg(Player a_Inviter) //Local이아닌 초대를 받는Pc에서 호출 될 RPC 함수
    {
        if (m_AlreadyHasParty == true) //이미 파티에 가입되어 있으면
            return;

        m_Inviting = true; //m_InviteMsgBox 보이게 하기 
        m_Timer = 5.0f;

        m_InviteMsgBox.GetComponentInChildren<Text>().text =
              a_Inviter.NickName + "님 파티초대에 수락하시겠습니까?";

        m_PartyMemberList = new List<Player>(); //초기화

        //파티 멤버 구성
        m_PartyMemberList.Add(PhotonNetwork.LocalPlayer);
        m_PartyMemberList.Add(a_Inviter);
        //파티 멤버 구성
    }
```
</details>

---

* #7-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PartySystemMgr.cs)) 파티 수락(PunRPC를 통해 파티를 보낸Pc와 초대를 받은Pc 모두 파티멤버 리스트 갱신)
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void InviteAcceptRejectBtnClick(bool a_Accept)
    {
        if (a_Accept == true) //파티 수락
        {
            //InviteAccept(); //초대를 한 Pc에서 호출 되어야 할 함수

            //초대를 받은 Pc에서 호출 되어야 할 함수
            for (int ii = 0; ii < m_PartyMemberList.Count; ii++)
            {
                //if (m_PartyMemberList[ii] != PhotonNetwork.LocalPlayer) // 나를 제외한
                    m_PhotonView.RPC("InviteAccept", m_PartyMemberList[ii]);
            }
            //초대를 받은 Pc에서 호출 되어야 할 함수
        }
        else //파티 거절
        {
            InviteReject();
        }
    }

    [PunRPC]
    void InviteAccept()//초대를 한 Pc에서 호출 되어야 할 함수
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        m_AlreadyHasParty = true; //파티 소속 여부 활성화

        PartyMemberRefresh();

        m_Timer = 0.1f; // InviteMsgBox 끄기
    }

    void InviteReject()
    {
        Sound_Mgr.Instance.PlayGUISound("UIButton", 1.0f);

        m_Timer = 0.1f; // InviteMsgBox 끄기
    }

    void PartyMemberRefresh()
    {
        // 파티원들의 닉네임 표시해 주기, 정보 담기
        for (int ii = 0; ii < m_PartyMemberList.Count; ii++)
        {
            m_PartyMember[ii].GetComponent<PartyMember>().m_Player = m_PartyMemberList[ii];

            if (m_PartyMember[ii].activeSelf == false)
                m_PartyMember[ii].SetActive(true);

            m_PartyMember[ii].GetComponentInChildren<Text>().text = m_PartyMemberList[ii].NickName;
        }
        // 파티원들의 닉네임 표시해 주기, 정보 담기
    }

```
</details>

---

* #8-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/ChatMgr.cs)) 채팅 기능 구현 
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Update()
    {
        //채팅 구현 텍스트 입력
        if (Input.GetKeyDown(KeyCode.Return))
        { //엔터키를 누르면 인풋 필드 활성화

            if (m_ChatRoot.activeSelf == true)
            {
                if (string.IsNullOrEmpty(m_ChatIF.text) == true)
                {
                    //m_Enter = false;
                    m_ChatRoot.SetActive(false);
                }
                else
                {
                    BroadcastingChat();
                    m_ChatIF.ActivateInputField();
                }
            }
            else
            {
                //m_Enter = true;
                m_ChatRoot.SetActive(true);
                m_ChatIF.ActivateInputField();
            }
        }
    }

    void BroadcastingChat()
    {
        string msg = "\n<color=#ffffff>[" +
                PhotonNetwork.LocalPlayer.NickName + "] " +
                m_ChatIF.text + "</color>";
        pv.RPC("LogMsg", RpcTarget.AllBuffered, msg);

        m_ChatIF.text = "";
    }

    [PunRPC]
    void LogMsg(string msg)
    {
        m_ChatList.Add(msg);
        if (20 < m_ChatList.Count)
            m_ChatList.RemoveAt(0);

        m_ChatMsg.text = "";
        for (int ii = 0; ii < m_ChatList.Count; ii++)
        {
            m_ChatMsg.text += m_ChatList[ii];
        }

        ////로그 메시지 Text UI에 텍스트를 누적시켜 표시
        //txtLogMsg.text = txtLogMsg.text + msg;
    }
```
</details>

---

* #8-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/ExpMgr.cs)) 레벨 관리 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
public class ExpMgr : MonoBehaviour
{
    List<int> m_LvTable = new List<int>() {0, 1000, 2000, 3000, 4000, 5000,
                                              6000, 7000, 8000, 9000, 10000};

    float m_ExpPer = 0.0f;
    public Image m_ExpBar;
    public Text m_ExpPerText;
    public Text m_LevelText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayfabMgr.inst.m_Data.TryGetValue("Exp", out string v))
        {
            GlobalValue.m_CurExp = int.Parse(v);
        }

        LevelUp(0);
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public void LevelUp(float a_MonExp)
    {
        float a_Exp;
        GlobalValue.m_CurExp = GlobalValue.m_CurExp + a_MonExp;
        a_Exp = GlobalValue.m_CurExp;

        if(a_Exp > 55000.0f) //ÃÖ´ë °æÇèÄ¡
        {
            m_LevelText.text = "Lv.10";
            m_ExpBar.fillAmount = 1.0f;
            GlobalValue.m_CurExp = 55000.0f;
        }

        for(int ii = 0; ii < m_LvTable.Count; ii++)
        {
            if(a_Exp >= m_LvTable[ii])
            {
                a_Exp = a_Exp - m_LvTable[ii];
            }
            else
            {
                GlobalValue.m_Level = ii;
                m_ExpPer = ((a_Exp / m_LvTable[ii])) * 100.0f;
                break;
            }
        }
        m_ExpBar.fillAmount = m_ExpPer / 100.0f;
        m_ExpPerText.text = m_ExpPer.ToString("F1") + "%";
        m_LevelText.text = "Lv." + GlobalValue.m_Level;

        PlayfabMgr.inst.SetPlayerData("Exp", GlobalValue.m_CurExp.ToString());
    }
}
```
</details>

---

* #8-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/UI/DamageText.cs)) 타겟 공격 시 데미지,보상 텍스트 띄워주기 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Start()
    {
        m_RefText = this.gameObject.GetComponentInChildren<Text>();
        if(m_RefText != null)
        {
            if (m_DamageVal > 0)
                m_RefText.text = "_" + m_DamageVal.ToString() + " Dmg";
            else if (m_DamageVal == 0)
                m_RefText.text = "0";
            else
                m_RefText.text = "+" + (-m_DamageVal).ToString() + " Heal";
        }

        m_RefAnimator = GetComponentInChildren<Animator>();
        if(m_RefAnimator != null)
        {
            AnimatorStateInfo a_AnimStateInfo = m_RefAnimator.GetCurrentAnimatorStateInfo(0); //애니메이션 재생 시간

            float a_LifeTime = a_AnimStateInfo.length;
            Destroy(gameObject, a_LifeTime); //재생완료 후 파괴
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //--World 좌표를 UGUI좌표로 환산해 주는 코드
        CanvasRect = GameMgr.Inst.m_Canvas.GetComponent<RectTransform>();
        ScreenPos = Camera.main.WorldToViewportPoint(m_BaseWdPos);
        WdScPos.x = ((ScreenPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f));
        WdScPos.y = ((ScreenPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f));
        transform.GetComponent<RectTransform>().anchoredPosition = WdScPos;
        //--World 좌표를 UGUI좌표로 환산해 주는 코드

        //카메라 컬링...
        m_CacVec = m_BaseWdPos - Camera.main.transform.position;
        if (m_CacVec.magnitude <= 0.0f)
        {  //데미지텍스트와 카메라가 같은 위치에 있어도 보일 필요 없음
            if (m_RefText.gameObject.activeSelf == true)
                m_RefText.gameObject.SetActive(false);
        }
        else if (0.0f < Vector3.Dot(Camera.main.transform.forward, m_CacVec.normalized))
        {  //카메라 앞쪽에 있다는 뜻
            if (m_RefText.gameObject.activeSelf == false)
                m_RefText.gameObject.SetActive(true);
        }
        else  //if (Vector3.Dot(Camera.main.transform.forward, m_CacVec.normalized) <= 0.0f)
        { //카메라 뒤쪽에 있다는 뜻
            if (m_RefText.gameObject.activeSelf == true)
                m_RefText.gameObject.SetActive(false);
        }
        //카메라 컬링...
    }

```
</details>

---

* #8-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/UI/BillBoard.cs)) NameText등의 UI 빌보드 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    void Start()
    {
        m_CameraTr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = m_CameraTr.forward; //ºôº¸µå
    }
```
</details>

---

* #8-5)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Monster/HuntingGround.cs)) OnTriggerEnter,Exit 함수를 이용한 캐릭터 위치 표시 및 BGM 변경 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    private void OnTriggerEnter(Collider coll)
    {
        if (m_huntingG == HuntingG.SAndT) //SlimeAndTurtle
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("견습생 사냥터");
                    Sound_Mgr.Instance.PlayBGM("BattleBGM", 0.7f);
                }
            }
        }
        else if(m_huntingG == HuntingG.GAndO)//GolemAndOrc
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("숙련자 사냥터");
                    Sound_Mgr.Instance.PlayBGM("BattleBGM", 0.7f);
                }
            }
        }
        else if(m_huntingG == HuntingG.Town)//Town
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                    GameMgr.Inst.SetMsg("마을");
            }
        }
        else
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("슬레이어 서식지");
                    Sound_Mgr.Instance.PlayBGM("BossBGM", 0.7f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (m_huntingG == HuntingG.SAndT) //SlimeAndTurtle
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("견습생 사냥터 길목");
                    Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
                }
            }
        }
        else if (m_huntingG == HuntingG.GAndO)//GolemAndOrc
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("숙련자 사냥터 길목");
                    Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
                }
            }
        }
        else if (m_huntingG == HuntingG.Town)//Town
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                    GameMgr.Inst.SetMsg("마을 외곽");
            }
        }
        else // 보스
        {
            if (coll.gameObject.tag == "Hero")
            {
                HeroCtrl a_Hero = coll.GetComponent<HeroCtrl>();

                if (a_Hero.m_PhotonView.IsMine)
                {
                    GameMgr.Inst.SetMsg("슬레이어 서식지 길목");
                    Sound_Mgr.Instance.PlayBGM("GameBGM", 1.0f);
                }
            }
        }
    }
```
</details>

---

* #8-6)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/GameMgr.cs)) '캐릭터 위치 표시 메시지' 리스트 관리 및 순차적 표시 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    IEnumerator MsgController()
    {
        while (true)
        {
            if (m_MsgList.Count == 0)
            {
                
            }
            else
            {
                if (m_MsgRoot.activeSelf == false)
                {//메시지가 진행중이지 않으면

                    for (int ii = 0; ii < m_MsgList.Count; ii++)
                    {//메세지 리스트에 쌓여있는 가장 첫번째 메시지를 찾아 띄워준다.
                        if (m_MsgList[ii] != null)
                        {
                            m_MsgText.text = m_MsgList[ii];
                            m_LastMsg = m_MsgList[ii]; //MsgList에서 지울 메시지 저장
                            break;
                        }
                    }

                    m_MsgRoot.SetActive(true); //메시지On
                    m_MsgTimer = 5.0f; //5초동안 메시지 보이게 하기
                }
            }
            yield return new WaitForSeconds(0.25f); // 0.25초 마다 List에 쌓인 메시지가 있는지 확인 한다.
        }
    }
```
</details>

---

* #9-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PlayfabMgr.cs)) PlayFab을 통한 로그인,아웃 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void Login(string a_Id, string a_Pw)
    {
        a_Id = a_Id.Trim();
        a_Pw = a_Pw.Trim();

        if (string.IsNullOrEmpty(a_Id) == true ||
           string.IsNullOrEmpty(a_Pw) == true)
        {
            m_LoginMgr.Message("ID, PW 빈칸 없이 입력해 주세요.");
            return;
        }

        if (!(6 <= a_Id.Length && a_Id.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("ID는 6글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        if (!(6 <= a_Pw.Length && a_Pw.Length < 20)) //6 ~ 20
        {
            m_LoginMgr.Message("비밀번호는 6글자 이상 20글자 이하로 작성해 주세요.");
            return;
        }

        //--- 로그인 성공시 어떤 유저 정보를 가져올지를 설정하는 옵션 객체 생성
        var option = new GetPlayerCombinedInfoRequestParams()
        {
            //--- DisplayName(닉네임)을 가져오기 위한 옵션
            GetPlayerProfile = true,
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true,  //DisplayName(닉네임) 가져오기 위한 요청 옵션
                //ShowAvatarUrl = true     //AvatarUrl 을 가져오는 옵션
            },
            //--- DisplayName(닉네임)을 가져오기 위한 옵션

            //--- BestScore 통계값(순위표에 관여하는)을 불러올 수 있는 옵션
            GetPlayerStatistics = true,

            //--- < 플레이어 데이터(타이틀) > 값을 불러올 수 있게 하는 옵션
            GetUserData = true
        };

        var request = new LoginWithEmailAddressRequest
        {
            Email = a_Id,
            Password = a_Pw,
            InfoRequestParameters = option
        };

        PlayFabClientAPI.LoginWithEmailAddress(request,
                                        OnLoginSuccess, OnLoginFailure);

        //SceneManager.LoadScene("scLobby");
    }
    void OnLoginSuccess(LoginResult result)
    {
        m_LoginMgr.Message("로그인 성공");

        GlobalValue.m_ID = result.PlayFabId;

        if (result.InfoResultPayload != null)
        {
            GlobalValue.m_NickName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }

        if (result.InfoResultPayload.PlayerProfile.DisplayName == null) //게임 처음시작이면
            LoadSceneMgr.LoadScene("CharacterScene");//캐릭터 선택 씬으로
        else //게임을 플레이했던 유저면
        {
            GetPlayerData(); //유저 데이터 불러오기
            GlobalValue.m_NickName = result.InfoResultPayload.PlayerProfile.DisplayName;

            LoadSceneMgr.LoadScene("InGame"); //인게임 씬으로
        }
    }
```
</details>

---

* #9-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PlayfabMgr.cs)) PlayFab유저 정보 저장
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void SetPlayerData(string a_Key, string a_Value)
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                {a_Key,a_Value}
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            (result)=> SetDataSuccess(),
            (error)=> SetDataFailure());
    }
```
</details>

---

* #9-3)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PlayfabMgr.cs)) PlayFab에 저장된 유저정보 불러오기 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void GetPlayerData()
    {
        var request = new GetUserDataRequest()
        {
            PlayFabId = GlobalValue.m_ID
        };

        PlayFabClientAPI.GetUserData(request,
            (result) =>
            {
                foreach (var eachData in result.Data)
                {
                    m_Data.Add(eachData.Key, eachData.Value.Value);
                }
            },
            (error) => GetDataFailure());
    }
```
</details>

---

* #9-4)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/PhotonInit.cs)) Photon을 통한 룸 생성 및 참가등의 서버 관리
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public override void OnConnectedToMaster() //ConnectUsingSettings 성공 콜백 함수
    {
        //Debug.Log("서버 접속 완료");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() //JoinLobby 성공 콜백 함수
    {
        //Debug.Log("로비 접속 완료");
    }

    public void JoinInGameField() //플래이팹 로그인 성공 시 호출 되는 함수
    {
        PhotonNetwork.LocalPlayer.NickName = GlobalValue.m_NickName; //닉네임 저장

        PhotonNetwork.JoinRandomRoom();
        //Debug.Log("조인 인게임 필드");
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // JoinRandomRoom 실패 시 호출되는 콜백 함수
    {
        //Debug.Log("방 없음");

        //생성 할 룸의 조건 설정
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 16;

        PhotonNetwork.CreateRoom("InGameField", roomOptions); //방 생성 및 입장(방이 없을 때)
    }

    public override void OnJoinedRoom() //CreateRoom, JoinRoom, JoinRandomRoom 성공 시 호출되는 콜백 함수
    {
        //Debug.Log("방 참가");

        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            //Debug.Log(player.Value.NickName);
        }

        //캐릭터,몬스터 생성
        if (PlayfabMgr.inst.m_Data.TryGetValue("Class", out string a_Class))
        {
            float a_RndX = Random.Range(-5.0f, 5.0f);
            float a_RndZ = Random.Range(-7.0f, 7.0f);

            Vector3 a_SpawnPos = GameMgr.Inst.m_SpawnPos.position;
            Vector3 a_RndSpawnPos = new Vector3(a_SpawnPos.x + a_RndX, a_SpawnPos.y, a_SpawnPos.z + a_RndZ);

            if (a_Class == "Knight")
                PhotonNetwork.Instantiate("Knight", a_RndSpawnPos, Quaternion.identity);
            else if (a_Class == "Mage")
                PhotonNetwork.Instantiate("Mage", a_RndSpawnPos, Quaternion.identity);
            else if (a_Class == "Healer")
                PhotonNetwork.Instantiate("Healer", a_RndSpawnPos, Quaternion.identity);
        }
        //캐릭터,몬스터 생성
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode.ToString());
        Debug.Log(message);
    }

    public override void OnCreatedRoom()
    {
        MonSpawnMgr a_MS = FindObjectOfType<MonSpawnMgr>();
        for (int ii = 0; ii < a_MS.m_SATSpawnPos.Length; ii++)
        { //SlimeAndTurtle Spawn
            a_MS.SATSpawn();
        }
        for (int ii = 0; ii < a_MS.m_GAOSpawnPos.Length; ii++)
        { //GolemAndOrc Spawn
            a_MS.GAOSpawn();
        }

        //Slayer Spawn
        PhotonNetwork.InstantiateRoomObject("Slayer_Boss", a_MS.m_SlayerSpawnPos.position, Quaternion.identity);
    }

    public void LeaveRoom()
    {
        //마지막 사람이 방을 떠날 때 룸의 CustomProperties를 초기화 해 주어야 한다.
        if (PhotonNetwork.PlayerList != null && PhotonNetwork.PlayerList.Length <= 1)
        {
            if (PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.CurrentRoom.CustomProperties.Clear();
        }

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() //LeaveRoom 성공 콜백 함수
    {
        if (m_SceneIdx == 1)
            LoadSceneMgr.LoadScene("StoreScene");
        else if (m_SceneIdx == 2)
            LoadSceneMgr.LoadScene("TitleScene");
    }
```
</details>

---

* #9-5) 캐릭터 생성 씬
  
<details>
<summary>소스 코드 및 이미지</summary>
  
</details>

---

* #9-6) 상점 씬
  
<details>
<summary>소스 코드 및 이미지</summary>
  
</details>

---

* #10-1)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/Sound_Mgr.cs)) 배경음, 효과음 재생 함수 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
  public void PlayBGM(string a_FileName, float fVolume = 1.0f)
    {
        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;

        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }


        if (m_AudioSrc == null)
            return;

        if (m_AudioSrc.clip != null && m_AudioSrc.clip.name == a_FileName)
            return;

        m_AudioSrc.clip = a_GAudioClip;
        m_AudioSrc.volume = fVolume * m_BGMSoundVolume;
        m_bgmVolume = fVolume;
        m_AudioSrc.loop = true;
        m_AudioSrc.Play();
    }

    public void PlayEffSound(string a_FileName, float fVolume = 0.3f)
    {
        if (m_SoundOnOff == false)
            return;

        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (a_GAudioClip == null)
            return;

        if (m_SndSrcList[m_SoundCount] != null)
        {
            m_SndSrcList[m_SoundCount].volume = fVolume * m_EffSoundVolume;
            m_SndSrcList[m_SoundCount].PlayOneShot(a_GAudioClip, fVolume * m_EffSoundVolume);
            m_EffVolume[m_SoundCount] = fVolume;

            m_SoundCount++;
            if (m_EffSdCount <= m_SoundCount)
                m_SoundCount = 0;
        }
    }

    public void PlayGUISound(string a_FileName, float fVolume = 0.2f)
    {//GUI 효과음 플레이 하기 위한 함수
        if (m_SoundOnOff == false)
            return;

        AudioClip a_GAudioClip = null;
        if (m_ADClipList.ContainsKey(a_FileName) == true)
        {
            a_GAudioClip = m_ADClipList[a_FileName] as AudioClip;
        }
        else
        {
            a_GAudioClip = Resources.Load("Sounds/" + a_FileName) as AudioClip;
            m_ADClipList.Add(a_FileName, a_GAudioClip);
        }

        if (m_AudioSrc == null)
            return;

        m_AudioSrc.PlayOneShot(a_GAudioClip, fVolume * m_EffSoundVolume);
    }

```
</details>

---

* #10-2)([Script](https://github.com/YboSim/Soul_Heroes_Unity3D/blob/main/SoulHeros/Assets/02.Scripts/Mgr/Sound_Mgr.cs)) 배경음, 효과음 음소거 및 볼륨 조절 기능 구현
  
<details>
<summary>소스 코드 및 이미지</summary>
  
```csharp
    public void SoundOnOff(bool a_OnOff = true) //BGM과 EFF 사운드 OnOff 조절해주는 함수
    {
        bool a_MuteOnOff = !a_OnOff;

        if (m_AudioSrc != null)
        {
            m_AudioSrc.mute = a_MuteOnOff; //mute == true 끄기 mute == false 켜기
            if (a_MuteOnOff == false)
                m_AudioSrc.time = 0;      //처음부터 다시 플레이
        }

        for (int ii = 0; ii < m_EffSdCount; ii++)
        {
            if (m_SndSrcList[ii] != null)
            {
                m_SndSrcList[ii].mute = a_MuteOnOff;

                if (a_MuteOnOff == false)
                    m_SndSrcList[ii].time = 0;
            }
        }

        m_SoundOnOff = a_OnOff;
    }

    //배경음은 지금 볼륨을 가져온 후에 플레이 해 준다.
    public void EffSoundVolume(float fVolume) //EFF 사운드 볼륨 조절해주는 함수
    {
        for (int ii = 0; ii < m_EffSdCount; ii++)
        {
            if (m_SndSrcList[ii] != null)
                m_SndSrcList[ii].volume = m_EffVolume[ii] * fVolume;
        }

        m_EffSoundVolume = fVolume;
    }

    public void BGMSoundVolume(float fVolume) //BGM 사운드 볼륨 조절해주는 함수
    {
        if (m_AudioSrc != null)
            m_AudioSrc.volume = m_bgmVolume * fVolume;

        m_BGMSoundVolume = fVolume;
    }
```
</details>

---

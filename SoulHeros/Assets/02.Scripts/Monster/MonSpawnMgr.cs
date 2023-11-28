using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonSpawnMgr : MonoBehaviour
{
    public Transform[] m_SATSpawnPos;
    public Transform[] m_GAOSpawnPos;

    public Transform m_MonsterDieZone;
    public Transform m_SlayerSpawnPos;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public IEnumerator SATSpawn_Cor(float a_DelayTime)
    {
        yield return new WaitForSeconds(a_DelayTime);

        int a_Rnd = Random.Range(0, 2);
        string a_MonName;
        if (a_Rnd == 0)
            a_MonName = "Slime";
        else
            a_MonName = "Turtle";

        float a_RndY = Random.Range(0.0f, 360.0f);
        PhotonNetwork.InstantiateRoomObject(a_MonName, Vector3.zero, Quaternion.Euler(0.0f, a_RndY, 0.0f));
    }

    public IEnumerator GAOSpawn_Cor(float a_DelayTime)
    {
        yield return new WaitForSeconds(a_DelayTime);

        int a_Rnd = Random.Range(0, 2);
        string a_MonName;
        if (a_Rnd == 0)
            a_MonName = "Golem";
        else
            a_MonName = "Orc";

        float a_RndY = Random.Range(0.0f, 360.0f);
        PhotonNetwork.InstantiateRoomObject(a_MonName, Vector3.zero, Quaternion.Euler(0.0f, a_RndY, 0.0f));
    }

    public void SATSpawn()
    {
        int a_Rnd = Random.Range(0, 2);
        string a_MonName;
        if (a_Rnd == 0)
            a_MonName = "Slime";
        else
            a_MonName = "Turtle";

        float a_RndY = Random.Range(0.0f, 360.0f);
        PhotonNetwork.InstantiateRoomObject(a_MonName, Vector3.zero, Quaternion.Euler(0.0f, a_RndY, 0.0f));
    }

    public void GAOSpawn()
    {
        int a_Rnd = Random.Range(0, 2);
        string a_MonName;
        if (a_Rnd == 0)
            a_MonName = "Golem";
        else
            a_MonName = "Orc";

        float a_RndY = Random.Range(0.0f, 360.0f);
        PhotonNetwork.InstantiateRoomObject(a_MonName, Vector3.zero, Quaternion.Euler(0.0f, a_RndY, 0.0f));
    }
}

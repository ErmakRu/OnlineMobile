using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//public class SpawnEnemyNew : MonoBehaviour
//{
//    [SerializeField]
//    public EnemyWavesNew[] EnemyWave;
//    public GameObject PowerUp;
//    public float timeForNewPowerup;
//    Camera mainCamera;
//    public int countWave;

//    // Start is called before the first frame update
//    void Start()
//    {
//        mainCamera = Camera.main;
//        //for each element in 'enemyWaves' array creating coroutine which generates the wave
//        for (int i = 0; i < EnemyWave.Length; i++)
//        {
//            CreateenemyWave(EnemyWave[i].timeToStart, EnemyWave[i].wave);
//        }
//        countWave = EnemyWave.Length;
//        //StartCoroutine(PowerupBonusCreation());
//    }

//    void CreateenemyWave(float delay, GameObject Wave)
//    {

//        Instantiate(Wave);
//    }

//    //private void Update()
//    //{
//    //    if(countWave != EnemyWave.Length)
//    //    {
//    //        if()
//    //        {

//    //        }
//    //    }
//    //}

//    //IEnumerator CreateenemyWave(float delay, GameObject Wave)
//    //{
//    //    if (delay != 0)
//    //        yield return new WaitForSeconds(delay);
//    //}
//}

#region Serializable classes
[System.Serializable]
public class EnemyWavesNew
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;

    [Tooltip("Enemy wave was spawn ?")]
    public bool spawnWaveBolean;

}
#endregion
public class SpawnEnemyNew : MonoBehaviourPun, IPunObservable
{
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(EnemyWave[WaveCounter].spawnWaveBolean);
        }
        else
        {
            BoleanWave[WaveCounter] = (bool) stream.ReceiveNext(); 
        }
    }

    public EnemyWavesNew[] EnemyWave;
    public bool[] BoleanWave;
    Camera MainCamera;
    private int WaveCounter;
    private float TimeWave;

    private void Start()
    {
        MainCamera = Camera.main;
        WaveCounter = 0;
        TimeWave = 0.0f;
    }

    private void CreateEnemyWave(float delay, GameObject wave)
    {
        PhotonNetwork.Instantiate(wave.name,new Vector3(0,0,0), Quaternion.identity, 0);

    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TimeWave += Time.deltaTime;
            if ((WaveCounter < EnemyWave.Length) && (EnemyWave[WaveCounter].timeToStart <= TimeWave) && EnemyWave[WaveCounter].spawnWaveBolean == false && BoleanWave[WaveCounter] == false)
            {
                CreateEnemyWave(EnemyWave[WaveCounter].timeToStart, EnemyWave[WaveCounter].wave);
                //TimeWave = 0.0f;
                EnemyWave[WaveCounter].spawnWaveBolean = true;
                BoleanWave[WaveCounter] = true;
                WaveCounter++;

            }
        }
    }


}


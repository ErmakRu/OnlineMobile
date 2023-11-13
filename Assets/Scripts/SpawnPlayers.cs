using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlaayers : MonoBehaviour
{
    public GameObject Player;
    public float minX, minY, maxX, maxY;
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX,minY), Random.Range(maxX,maxY));
        PhotonNetwork.Instantiate(Player.name, randomPosition, Quaternion.identity);
    }

}

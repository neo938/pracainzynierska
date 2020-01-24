﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public Transform arena;
    public List<GameObject> monsterPrefabs;

    List<GameObject> spawnPoints;
    List<GameObject> pieces;
    

    private void Start()
    {
        RefreshList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Random.value > 0.5f)
            {
                SpawnMonster(0);
            }
            else
            {
                SpawnMonster(1);
            }
               
        }
    }

    void RefreshList()
    {
        pieces = new List<GameObject>(GameObject.FindGameObjectsWithTag("Piece"));
        pieces.Sort(SortByName);

        spawnPoints = new List<GameObject>();
        foreach(GameObject child in pieces)
        {
            spawnPoints.Add(child.transform.GetChild(0).Find("Spawner left down").gameObject);
            spawnPoints.Add(child.transform.GetChild(0).Find("Spawner right down").gameObject);
        }
    }

    void SpawnMonster(int monsterID)
    {
        int randomSpawnNumber = Random.Range(0, spawnPoints.Count);
        Vector3 positionForSpawn = spawnPoints[randomSpawnNumber].transform.position;
        GameObject enemy = Instantiate(monsterPrefabs[monsterID],positionForSpawn,Quaternion.identity,gameObject.transform) as GameObject;
        BlinkPiece(spawnPoints[randomSpawnNumber].transform.parent.parent.gameObject);
    }

    void BlinkPiece(GameObject piece)
    {
        piece.GetComponent<PieceScript>().Blink();
    }

    private static int SortByName(GameObject o1, GameObject o2)
    {
        return o1.name.CompareTo(o2.name);
    }
}
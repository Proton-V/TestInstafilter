using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    //UI
    public CanvasManager CanvasManager;

    public GameObject MapObject;

    public Queue<GameObject> EnemySpawnList = new Queue<GameObject>();
    [HideInInspector]
    public int MaxListCount = 10;
    public GameObject EnemyParent;
    public GameObject EnemyPrefab;
    public float EnemySpawnSecValue;
    public List<float> EnemySpawnPointX;

    [Range(1, 500)]
    public int HealthValue;
    [Range(1f, 10f)]
    public float WaitSecMinusHealthValue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}

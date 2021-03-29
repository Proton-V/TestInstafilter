using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameManager _gm;
    private PlayerController _player;
    [Range(1f, 10f)]
    public float EnemySpawnDistance;

    private int _health;
    [HideInInspector]
    public bool GameStart = true;

    private Action EventByTime;

    private void Start()
    {
        EventByTime += MinusHealth;
        _gm = GameManager.Instance;
        _player = PlayerController.Instance;
        _health = _gm.HealthValue;

        SetListenerToSpeedSlider();

        StartCoroutine(SpawnEnemy());
        StartCoroutine(GameEventByTime());
    }

    private IEnumerator SpawnEnemy()
    {
        while (GameStart)
        {
            yield return new WaitForSeconds(_gm.EnemySpawnSecValue);
            if (_gm.EnemySpawnList.Count < _gm.MaxListCount)
            {
                GameObject newEnemy = Instantiate(_gm.EnemyPrefab,
    new Vector3(_gm.EnemySpawnPointX[UnityEngine.Random.Range(0, _gm.EnemySpawnPointX.Count)], _gm.EnemyPrefab.transform.localScale.y / 2, _player.transform.position.z + EnemySpawnDistance),
    Quaternion.identity);
                newEnemy.transform.SetParent(_gm.EnemyParent.transform);
                _gm.EnemySpawnList.Enqueue(newEnemy);
            }
            else
            {
                GameObject newEnemy = _gm.EnemySpawnList.Dequeue();
                newEnemy.transform.position = new Vector3(_gm.EnemySpawnPointX[UnityEngine.Random.Range(0, _gm.EnemySpawnPointX.Count)], _gm.EnemyPrefab.transform.localScale.y / 2, _player.transform.position.z + EnemySpawnDistance);
                _gm.EnemySpawnList.Enqueue(newEnemy);
            }
        }
    }
    private IEnumerator GameEventByTime()
    {
        while (GameStart)
        {
            yield return new WaitForSeconds(_gm.WaitSecMinusHealthValue);
            EventByTime.Invoke();
        }
    }
    private void MinusHealth()
    {
        _health--;
        _gm.CanvasManager.HealthText.text = $"{_health}";
    }
    private void SetListenerToSpeedSlider()
    {
        Slider speedSlider = _gm.CanvasManager.SpeedSlider;
        speedSlider.minValue = PlayerController.Min_Speed;
        speedSlider.maxValue = PlayerController.Max_Speed;
        speedSlider.value = _player.PlayerSpeed;

        speedSlider.onValueChanged.AddListener(ChangePlayerSpeed);
    }
    private void ChangePlayerSpeed(float value)
    {
        
        _player.PlayerSpeed = value;
    }
}

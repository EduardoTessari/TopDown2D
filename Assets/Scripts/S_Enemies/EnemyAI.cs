using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _roamingCD = 2f;
    private enum State
    {
        Roaming
    }

    private State _state;
    private EnemyPath _enemyPath;

    private void Awake()
    {
        _enemyPath = GetComponent<EnemyPath>();
        _state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        /* While in Roaming state it will:
             #1 - Get the random position
             #2 - Inform the EnemyPath Script what is the random position
             #3 - Do it every X seconds
        */

        while (_state == State.Roaming)
        {
            
            Vector2 roamPosition = GetRoamingPosition();
            
            _enemyPath.MoveTo(roamPosition);
            
            yield return new WaitForSeconds(_roamingCD);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        // Responsible for defining a random position for the assete
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}

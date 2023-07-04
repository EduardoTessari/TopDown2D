using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Knockback _knockback;

    [SerializeField] private float _moveSpeed = 2f;


    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_knockback.gettingKnockedBack) 
        { 
            return; 
        }

     // Responsible for generate the movePosition for the asset based in the random position.
        _rb.MovePosition(_rb.position + _moveDir * (_moveSpeed * Time.fixedDeltaTime));
    }


    // Responsible for getting the random position from the EnemyAI scrpit
    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }

}

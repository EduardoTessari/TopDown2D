using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }


    [SerializeField] private float _knockBackTime = .2f;
    private Rigidbody2D _rb;




    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack (Transform damageSource, float knockBackThrust)
    {
        gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * _rb.mass;
        _rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockBackRoutine());
    }

    private IEnumerator KnockBackRoutine()
    {
        yield return new WaitForSeconds(_knockBackTime);
        _rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}

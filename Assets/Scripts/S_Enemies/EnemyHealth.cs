using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;
    [SerializeField] private GameObject _deathVFHPreFab;
    [SerializeField] private float _knockBackThrust = 15f;

    private int _currentHealth;
    private Knockback _knockback;
    private FlashHit _flashHit;

    private void Awake()
    {
        _flashHit = GetComponent<FlashHit>();
        _knockback = GetComponent<Knockback>();
    }

    public void Start()
    {
        _currentHealth = _startingHealth;
    }


    public void TakeDamage(int damage)
    {
        // This class makes de calculation of the damage taken by the object and also validate if the DetectDeath was activate.

        _currentHealth -= damage; //curent = curent - damage

        // start the coroutine set int he FlashHit Script
        StartCoroutine(_flashHit.FlashRoutine());

        // start the coroutine set int he KnockBack Script
        _knockback.GetKnockedBack(PlayerControler.Instance.transform, _knockBackThrust);

        // start the coroutine set in this scprit to check death.
        StartCoroutine(CheckDetectDeathRoutine());

    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        // this coroutine is used for give a effec to the game object. It will only check if the object is "dead" after the time set in the FlashHit script
        yield return new WaitForSeconds(_flashHit.GetRestoreMatTime());
        DetectDeath();
    }

    
    private void DetectDeath()
    {
        // This class validate  if the health is lesser than 0, if so destroy the Game object

        if (_currentHealth <= 0)
        {
            Instantiate(_deathVFHPreFab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _slashAnim;
    [SerializeField] private Transform _slashAnimSpawn;
    [SerializeField] private Transform _swordCollider;
    [SerializeField] private float _SwordAttackCD = 0.3f;

    private PlayerControls _playerControls;
    private Animator _anim;
    private PlayerControler _playercontroler;
    private ActiveWeapon _activeWeapon;

    private GameObject _slash;
    private bool _attackButtonDown = false;
    private bool _isAttacking = false;


    private void Awake()
    {
        _playercontroler = GetComponentInParent<PlayerControler>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _anim = GetComponent<Animator>();
        _playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        FlipSword();
        Attack();
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private IEnumerator AttackCDRoutine()
    {
        
        yield return new WaitForSeconds(_SwordAttackCD);
        _isAttacking = false;

    }

    void Attack()
    {
        if(_attackButtonDown && !_isAttacking)
        {
            _isAttacking = true;
            _anim.SetTrigger("Attack");
            _swordCollider.gameObject.SetActive(true);

            _slash = Instantiate(_slashAnim, _slashAnimSpawn.position, Quaternion.identity);
            _slash.transform.parent = this.transform.parent;

            StartCoroutine(AttackCDRoutine());
        }
    }

    public void DoneAttackingAnim()
    {
        _swordCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimation()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (!_playercontroler.LookingRight)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public void SwingDownFlipAnimation()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if(!_playercontroler.LookingRight)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }

    }


    private void FlipSword()
    {
        /* In this class we are:
         * #1 - checking if the player is looking to the left
         * #2 - If so, we're going to rotate  the weapon 180 degrees to change it position
         * #3 - Because of a error where the rotate was not following the player position, we defined that that the Active Weapon position were the position it already had
         * #4 - As we want it to follow the player, if the x value were negative, the position defined on #3 were multiplied fot -1 to keep it original value, but negative
         * #5 - At last, the defined that the new location would be the one defined at #4.
         * */
        
         
        if (_playercontroler.LookingRight == false)
        {

            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            _swordCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
            Vector3 weaponLocalPosition = _activeWeapon.transform.localPosition;
            weaponLocalPosition.x = Mathf.Abs(weaponLocalPosition.x) * -1f;
            _activeWeapon.transform.localPosition = weaponLocalPosition;
        }

        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            _swordCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
            Vector3 weaponLocalPosition = _activeWeapon.transform.localPosition;
            weaponLocalPosition.x = Mathf.Abs(weaponLocalPosition.x);
            _activeWeapon.transform.localPosition = weaponLocalPosition;

        }



    }
}

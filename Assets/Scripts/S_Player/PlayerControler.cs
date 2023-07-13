using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This line says that this script is associated to the sigleton one, in other word, singleton is the father.
public class PlayerControler : Singleton<PlayerControler>
{
    public bool LookingRight {  get { return _lookingRight; } }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _dashSpeed = 4f;
    [SerializeField] private TrailRenderer _tr;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private float _startingMoveSpeed;

    private bool _lookingRight;
    private bool _isDashing = false;

    protected override void Awake()
    {
        //fist call the Awake function on the Singleton Class
        base.Awake();

        _playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        _lookingRight = true;

    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();
        _startingMoveSpeed = _moveSpeed;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }


    private void Update()
    {
        PlayerInput();

        anim.SetFloat("MoveX", _movement.x);
        anim.SetFloat("MoveY", _movement.y);
    }

    private void FixedUpdate()
    {
        PlayerDirection();
        Move();
    }

    // Class responsible for identify which button is being pressed, based at the player Control Map
    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
    }

    // Class responsible for generate the movement based on the player Contol Map and the speed variable
    private void Move()
    {
        rb.MovePosition(rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }

    // Class responsible for fliping the sprite accorind with the direction pressd on the player control Map.
    private void PlayerDirection()
    {
        if(_movement.x > 0)
        {
            _lookingRight = true;
            sprite.flipX = false;
            
        }
        else if (_movement.x < 0)
        {
            _lookingRight = false;
            sprite.flipX = true;
        }
       
    }

    private void Dash()
    {
        if (!_isDashing)
        {
            _isDashing = true;
            _moveSpeed *= _dashSpeed;
            _tr.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
        

    }

    private IEnumerator EndDashRoutine()
    {
        float _dashTime = 0.2f;
        float _dashCD = 0.25f;
        yield return new WaitForSeconds(_dashTime);
        _moveSpeed = _startingMoveSpeed;
        _tr.emitting = false;
        yield return new WaitForSeconds(_dashCD);
        _isDashing = false;
    }

}



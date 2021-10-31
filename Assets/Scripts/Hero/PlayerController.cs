using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private float speedX = -1f;
    [SerializeField] private float jumpForce = 500f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private CheckGround checkGround;
    [SerializeField] private GameObject taskOne;
    [SerializeField] private GameObject taskTwo;


    private float _horizontal = 0f;
    private bool _isFacingRight = true;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFinish = false;
    private bool _isLeverArm = false;

    private Rigidbody2D _rb;
    private Finish _finish;
    private LeverArm _leverArm;
    private FixedJoystick _fixedJoystick;

    const float speedXMultiplier = 50f;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _leverArm = FindObjectOfType<LeverArm>();
        _fixedJoystick = GameObject.FindGameObjectWithTag("Fixed Joystick").GetComponent<FixedJoystick>();
    }

    void Update()
    {
        // for UNITY_EDITOR
       // _horizontal = Input.GetAxis("Horizontal");


        // for UNITY_ANDROID
        _horizontal = _fixedJoystick.Horizontal;
        
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
        if (checkGround.OnGround)
        {
            _isGround = true;
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.AddForce(new Vector2(0f, jumpForce),ForceMode2D.Impulse);
            _isGround = false;
            _isJump = false;
        }

        if (_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0f && _isFacingRight)
        {
            Flip();
        }
    }

    public void Jump()
    {
        if (!_isGround)
            return;
        
        _isJump = true;
        jumpSound.Play();     
    }
    public void Interact()
    {
        if (_isFinish)
        {
            _finish.FinishLevel();
        }
        if (_isLeverArm)
        { 
            _leverArm.ActivateLeverArm();
            if (taskOne != null && taskTwo != null)
            {
            taskOne.SetActive(false);
            taskTwo.SetActive(true);
            }
        }
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();

        if (other.CompareTag("Finish"))
        {
            _isFinish = true;
        }
        if (leverArmTemp != null)
        {
            _isLeverArm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        LeverArm leverArmTemp = other.GetComponent<LeverArm>();
        if (other.CompareTag("Finish"))
        {
            _isFinish = false;
        }
        if (leverArmTemp != null)
        {
            _isLeverArm = false;
        }
    }


}

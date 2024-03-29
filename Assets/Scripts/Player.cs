using UnityEngine;
public class Player : Movement {
    private float _h, _v;
    Rigidbody2D _rb2d;
    WeaponController _wc;
    AudioSource _audioSource;

    public Animator animator;
    void Start ()
    {
        _wc = GetComponentInChildren<WeaponController>();
        _rb2d = GetComponent<Rigidbody2D>();
        animator.SetTrigger("Create");
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (_h != 0 && !isMoving)
        { 
            StartCoroutine(MoveHorizontal(_h, _rb2d));
        }
        else if (_v != 0 && !isMoving)
        {
            StartCoroutine(MoveVertical(_v, _rb2d));
        } 
    }
    void Update () {
        if(isMoving)
            _audioSource.UnPause();
        else
            _audioSource.Pause();
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(_h)+Mathf.Abs(_v));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _wc.Fire();
        }
    }
}

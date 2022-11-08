using UnityEngine;
public class Player : Movement {
    private float _h, _v;
    Rigidbody2D _rb2d;
    void Start ()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (_h != 0 && !isMoving) StartCoroutine(MoveHorizontal(_h, _rb2d));
        else if (_v != 0 && !isMoving) StartCoroutine(MoveVertical(_v, _rb2d));
    }
    void Update () {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }
}

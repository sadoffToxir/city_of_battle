using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Movement {
    Rigidbody2D _rb2d;
    float _h, _v;
    [SerializeField]
    LayerMask blockingLayer;
    enum Direction { Up, Down, Left, Right };
    
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        RandomDirection();
    }

public void RandomDirection()
    {
        CancelInvoke(nameof(RandomDirection));
        
        List<Direction> lottery = new List<Direction>();
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(1, 0), blockingLayer))
        {
            lottery.Add(Direction.Right);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(-1, 0), blockingLayer))
        {
            lottery.Add(Direction.Left);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, 1), blockingLayer))
        {
            lottery.Add(Direction.Up);
        }
        if (!Physics2D.Linecast(transform.position, (Vector2)transform.position + new Vector2(0, -1), blockingLayer))
        {
            lottery.Add(Direction.Down);
        }
        
        Direction selection = lottery[Random.Range(0, lottery.Count)];
        if (selection == Direction.Up)
        {
            _v = 1;
            _h = 0;
        }
        if (selection == Direction.Down)
        {
            _v = -1;
            _h = 0;
        }
        if (selection == Direction.Right)
        {
            _v = 0;
            _h = 1;
        }
        if (selection == Direction.Left)
        {
            _v = 0;
            _h = -1;
        }
        Invoke(nameof(RandomDirection), Random.Range(3, 6));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomDirection();
    }

    private void FixedUpdate()
    {
        if (_v != 0 && isMoving == false) StartCoroutine(MoveVertical(_v, _rb2d));
        else if (_h != 0 && isMoving == false) StartCoroutine(MoveHorizontal(_h, _rb2d));
    }
}

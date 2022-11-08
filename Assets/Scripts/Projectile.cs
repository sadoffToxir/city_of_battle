using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Projectile : MonoBehaviour
{
    public bool destroySteel;
    [SerializeField] bool tobeDestroyed;
    GameObject _brickGameObject, _steelGameObject;
    Tilemap _tilemap;
    public int speed = 1;
    Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.velocity = transform.up * speed;
        _brickGameObject = GameObject.FindGameObjectWithTag("Brick");
        _steelGameObject = GameObject.FindGameObjectWithTag("Steel");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rb2d.velocity = Vector2.zero;
        _tilemap = collision.gameObject.GetComponent<Tilemap>();
        if ((collision.gameObject == _brickGameObject) || (destroySteel && collision.gameObject == _steelGameObject))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.5f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.5f * hit.normal.y;
                _tilemap.SetTile(_tilemap.WorldToCell(hitPosition), null);
            }

            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (_rb2d != null)
        {
            _rb2d.velocity = transform.up * speed;
        }
    }

    private void OnDisable()
    {
        if (tobeDestroyed)
        {
            Destroy(this.gameObject);
        }
    }

    public void DestroyProjectile()
    {
        if (gameObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }

        tobeDestroyed = true;
    }
}

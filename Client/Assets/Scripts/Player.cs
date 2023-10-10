using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    private bool _isPlayer = false;

    private void Update()
    {
        if(!_isPlayer)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, vertical, 0).normalized * Time.deltaTime * _speed;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
}

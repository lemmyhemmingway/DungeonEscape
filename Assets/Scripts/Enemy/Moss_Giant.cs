using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss_Giant : Enemy
{
    private Vector3 _currentTarget;
    private SpriteRenderer _mossSprite;

    public void Start()
    {
        _mossSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Debug.Log(transform.position);
        if (_mossSprite.transform.position == pointA.position)
        {
            _currentTarget = pointB.position;
        }
        else if (_mossSprite.transform.position == pointB.position)
        {
            _currentTarget = pointA.position;
        }

        transform.position = Vector3.MoveTowards(_mossSprite.transform.position, _currentTarget, speed * Time.deltaTime);
    }
}

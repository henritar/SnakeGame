using System;
using Project.Snake.UMVCS.Controller;
using Project.Snake.UMVCS.View;
using Project.UMVCS.Controller.Events;
using UnityEngine;

public class SnakePlayerView : SnakeView
{
    public SnakeHitSnakeEvent OnSnakeHitSnake = new SnakeHitSnakeEvent();
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        SnakeController snake = other.gameObject.GetComponentInChildren<SnakeController>();

        if (snake != null)
        {
            OnSnakeHitSnake?.Invoke(snake);
        }
    }

}

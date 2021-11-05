using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;

    CharacterAnimator animator;

    public bool isMoving { get; private set; }

    public float offSetY { get; private set; } = 0.3f;

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        setPositionAndSnapToTile(transform.position);
    }

    public void setPositionAndSnapToTile(Vector2 pos)
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.5f + offSetY;

        transform.position = pos;
    }   

    public IEnumerator Move(Vector2 moveVec, Action onMoveOver=null)
    {
        animator.moveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.moveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!isPathClear(targetPos))
        {
            yield break;
        }

        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

        onMoveOver?.Invoke();
    }

    public void HandleUpdate()
    {
        animator.isMoving = isMoving;
    }

    private bool isPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;

        if(Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, GameLayers.i.solidLayer | GameLayers.i.interactableLayer | GameLayers.i.playerLayer))
        {
            return false;
        }
        return true;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, GameLayers.i.solidLayer | GameLayers.i.interactableLayer) == true)
        {
            return false;
        }
        return true;
    }

    public void lookDirection(Vector3 targetPos)
    {
        var xDiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var yDiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if (xDiff == 0 || yDiff == 0)
        {
            animator.moveX = Mathf.Clamp(xDiff, -1f, 1f);
            animator.moveY = Mathf.Clamp(yDiff, -1f, 1f);
        }
        else
        {
            Debug.LogError("Error look direction: Character cannot look DIAGONALLY!!");
        }
    }

    public CharacterAnimator anim
    {
        get => animator;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Character character;
   
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void HandleUpdate()
    {
        if (!character.isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, onMoveOver));
            }
        }

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    void Interact()
    {
        var facingDir = new Vector3(character.anim.moveX, character.anim.moveY);
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact(transform);
        }
    }

    private void onMoveOver()
    {
        var triggerableCollider = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.offSetY), 0.2f, GameLayers.i.triggerableLayers);

        foreach (var collider in triggerableCollider)
        {
            var triggerable = collider.GetComponent<PlayerTriggerable>();

            if (triggerable != null)
            {
                character.anim.isMoving = false;
                triggerable.onPlayerTriggered(this);
                break;
            }
        }
    }

    public Character Char => character;

}

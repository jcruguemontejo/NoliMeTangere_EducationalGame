using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkLeftSprites;
    [SerializeField] faceDir defualtFaceDirection = faceDir.Down;

    public float moveX { get; set; }
    public float moveY { get; set; }
    public bool isMoving { get; set; }

    SpriteAnimator walkDown;
    SpriteAnimator walkUp;
    SpriteAnimator walkRight;
    SpriteAnimator walkLeft;

    SpriteAnimator currentAnim;
    bool preveiouslyMoving;

    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDown = new SpriteAnimator(walkDownSprites, spriteRenderer);
        walkUp = new SpriteAnimator(walkUpSprites, spriteRenderer);
        walkRight = new SpriteAnimator(walkRightSprites, spriteRenderer);
        walkLeft = new SpriteAnimator(walkLeftSprites, spriteRenderer);
        setFaceDir(defualtFaceDirection);
        currentAnim = walkDown;
    }

    // Update is called once per frame
    void Update()
    {
        var prevAnima = currentAnim;

        if(moveX == 1)
        {
            currentAnim = walkRight;
        }else if(moveX == -1)
        {
            currentAnim = walkLeft;
        }else if(moveY == 1)
        {
            currentAnim = walkUp;
        }
        else if(moveY == -1)
        {
            currentAnim = walkDown;
        }


        if(currentAnim != prevAnima || isMoving != preveiouslyMoving)
        {
            currentAnim.Start();
        }



        if (isMoving)
        {
            currentAnim.HandleUpdate();
        }
        else
        {
            spriteRenderer.sprite = currentAnim.Frames[0];
        }

        preveiouslyMoving = isMoving;
    }

    public void setFaceDir(faceDir dir)
    {
        if (dir == faceDir.Right)
        {
            moveX = 1;
        }else if(dir == faceDir.Left)
        {
            moveX = -1;
        }else if(dir == faceDir.Up)
        {
            moveY = 1;
        }else if(dir == faceDir.Down)
        {
            moveY = -1;
        }
    }

    public faceDir defaultDir
    {
        get => defualtFaceDirection;
    }
}


public enum faceDir { Up, Down, Left, Right}
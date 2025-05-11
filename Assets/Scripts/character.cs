using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Character : MonoBehaviour
{
    [SerializeField]
    private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;
    private Animator animator;
    private Vector3 right = new Vector3(1f, 1f, 1f);
    private Vector3 left = new Vector3(-1f, 1f, 1f);
    private Vector2 direction = Vector2.zero;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            movement.SetDirection(Vector2.right);
        }


         if (this.movement.direction != this.direction)
        {
            this.direction = this.movement.direction;
            if (this.movement.direction == Vector2.up)
            {
                this.animator.SetBool("back", true);
            }
            else if (this.movement.direction == Vector2.down)
            {
                this.animator.SetBool("back", false);
            }
            else if (this.movement.direction == Vector2.left)
            {
                this.animator.SetBool("back", false);
                this.gameObject.transform.localScale = left;
            }
            else if (this.movement.direction == Vector2.right)
            {
                this.movement.SetDirection(Vector2.right);
                this.gameObject.transform.localScale = right;
            }
        }
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
        deathSequence.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }

}

using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
    public Movement movement;
    public Animator animator;
    private Vector2 direction;
    private Vector3 right = new Vector3(1f, 1f, 1f);
    private Vector3 left = new Vector3(-1f, 1f, 1f);

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.animator = GetComponent<Animator>();
        this.direction = this.movement.direction;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
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
}

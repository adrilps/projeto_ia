using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class character : MonoBehaviour
{

    public Rigidbody2D Rigidbody2d { get; private set; }
    public float speed = 8.0f;
    public float speedmultiplier = 1f;
    public Vector2 direcaoinicio;
    public Vector2 direcao {  get; private set; }
    public Vector2 proxdirecao { get; private set; }
    public Vector3 inicio;
    public LayerMask paredes;

    private void Awake()
    {
        this.Rigidbody2d = GetComponent<Rigidbody2D>();
        this.inicio = this.transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = this.Rigidbody2d.position;
        Vector2 translation = this.direcao * this.speed * this.speedmultiplier * Time.fixedDeltaTime;
        this.Rigidbody2d.MovePosition(position +  translation);  
    }

    public void Update()
    {
        if (this.proxdirecao != Vector2.zero)
        {
            SetDirection(proxdirecao);
        }

    }

    public void ResetState()
    {
        this.speedmultiplier = 1f;
        this.direcao = direcaoinicio;
        this.proxdirecao = Vector2.zero;
        this.transform.position = this.inicio;
        this.enabled = true;
    }

    public void SetDirection(Vector2 direcao)
    {
        if (!Ocupado(direcao))
        {
            this.direcao = direcao;
            this.proxdirecao = Vector2.zero;
        }
        else
        {
            this.proxdirecao = direcao;
        }
    }

    public bool Ocupado(Vector2 direcao)
    {
        RaycastHit2D ray = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direcao, 1.5f, paredes);
        return ray.collider!=null;
    }
}

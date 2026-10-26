using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    
    private InputSystem_Actions _inputActions;
    private Rigidbody2D _rb;
    public float speed = 2f;
    private Vector2 input;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;

    }
    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    void Update() {
        //float input = Input.GetAxis("Horizontal"); - это просто вектор по горизонтальной оси
        input = _inputActions.Player.Move.ReadValue<Vector2>(); // управление через умни систему

        _animator.SetBool("IsWalking", input != Vector2.zero);

        if (input.x != 0)
        {
            _spriteRenderer.flipX = input.x < 0; // если input.x < 0 (вводимая компонента по х) то отзеркаливает, иначе не отзеркаливает (и смотрим по стандарту влево)
        }
    }
    void FixedUpdate()
    {



        _rb.MovePosition(_rb.position + input * speed * Time.fixedDeltaTime); // движение через Rigidbody2D
        //transform.position += new Vector3(input.x, input.y, 0) * speed * Time.deltaTime; // обычное движение

        // старое:
        //transform.position += Vector3.right * input * speed * Time.deltaTime;
        // transform.position = (x,y,z) - координаты обьекта
        // Vector3.right = (1,1,1) - какой то стандартный вектор 
        // без * Time.deltaTime будет двигаться в зависимости от фпс


    }
}

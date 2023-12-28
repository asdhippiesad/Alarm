using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    private const string Vertical = nameof(Vertical);
    private const string Horizontal = nameof(Horizontal);
    private const string Walk = nameof(Walk);

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Coroutine _coroutine;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float vertical = Input.GetAxisRaw(Vertical);
        float horizontal = Input.GetAxisRaw(Horizontal);

        float walkAnimation = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
        _animator.SetFloat(Walk, walkAnimation);

        transform.Translate(new Vector2(horizontal, vertical) * _speed * Time.deltaTime);
    }
}
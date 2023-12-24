using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private const string Vertical = nameof(Vertical);
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private Coroutine _coroutine;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            float vertical = Input.GetAxisRaw(Vertical);
            float horizontal = Input.GetAxisRaw(Horizontal);

            float walkAnimation = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
            _animator.SetFloat("Walk", walkAnimation);

            transform.Translate(new Vector2(horizontal, vertical) * _speed * Time.deltaTime);

            yield return null;
        }
    }
}

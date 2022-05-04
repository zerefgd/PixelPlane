using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _playerTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 current = transform.position;
        current.y = _playerTransform.position.y + offset.y;
        transform.position = current;
    }
}

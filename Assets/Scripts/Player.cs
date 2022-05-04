using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _maxYSpeed, _maxXSpeed, _acceleration, _boundsX;

    [SerializeField]
    private List<Sprite> _planes;

    [SerializeField]
    private AudioClip _coinClip, _explosionClip;

    private float currentYSpeed, currentXSpeed,horizontalInput;

    private bool hasGameFinished;
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        currentYSpeed = _maxYSpeed;
        currentXSpeed = 0f;
        hasGameFinished = false;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (hasGameFinished) return;

        if(Mathf.Abs(horizontalInput) > 0.05f)
        {
            currentXSpeed += horizontalInput * _acceleration * Time.fixedDeltaTime;

            if (currentXSpeed > _maxXSpeed)
                currentXSpeed = _maxXSpeed;
            if (currentXSpeed < -_maxXSpeed)
                currentXSpeed = -_maxXSpeed;
        }

        float currentImage = (currentXSpeed / _maxXSpeed) * (_planes.Count - 1);
        int imageIndex = (int)Mathf.Floor(Mathf.Abs(currentImage));
        sr.sprite = _planes[imageIndex];
        sr.flipX = currentImage < 0f;

        currentYSpeed = _maxYSpeed - Mathf.Abs(currentXSpeed);
        Vector3 offset = new(currentXSpeed, -currentYSpeed, 0);
        transform.position += Time.fixedDeltaTime * offset;

        Vector3 temp = transform.position;

        if(temp.x > _boundsX || temp.x < -_boundsX)
        {
            temp.x = -temp.x;
            transform.position = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.FINISH))
        {
            AudioManager.instance.PlaySound(_explosionClip);
            GameManager.instance.GameOver();
            hasGameFinished = true;
            enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.Tags.RESPAWN))
        {
            GameManager.instance.SpawnObstacle();
            AudioManager.instance.PlaySound(_coinClip);
            Destroy(collision.transform.parent.gameObject, 1f);
        }
    }
}
    
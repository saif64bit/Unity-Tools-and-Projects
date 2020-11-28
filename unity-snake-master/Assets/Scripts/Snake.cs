using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private static long intialLength = 1;
    public static long length = intialLength;
    Vector2 direction = Vector2.right;
    List<Transform> tail = new List<Transform>();
	private bool ate = true;
    public GameObject tailPrefab;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    public AudioClip [] audioClips;

    void Start()
    {
        length = intialLength;
        InvokeRepeating(nameof(this.Move), 0.3f, 0.3f);
        audioSource1 = this.gameObject.AddComponent <AudioSource> ();
        audioSource2 = this.gameObject.AddComponent <AudioSource> ();
    }

    void Update()
    {
        Vector2 directionTemp = (Input.GetAxisRaw ("Horizontal") != 0 ^ Input.GetAxisRaw ("Vertical") != 0) ? Vector2.right * (Input.GetAxisRaw ("Horizontal")) + Vector2.up * (Input.GetAxisRaw ("Vertical")) : direction;

        if (direction != directionTemp && direction != -directionTemp) {
            direction = directionTemp;
            Move ();
        }
    }

    private void Move()
    {
        if (GameController.isPaused) return;

        foreach (Transform child in tail) {
            if (transform.position == child.position) {
                GameController.FailGame();
                audioSource2.clip = audioClips [3];
                audioSource2.Play ();
                return;
            }
        }

        AudioPlay ();

        Vector2 currentPos = transform.position;
        transform.Translate(direction);

        if (ate)
        {
            
            var newPart = (GameObject)Instantiate(tailPrefab, currentPos, Quaternion.identity);
            tail.Insert(0, newPart.transform);
            length++;
            ate = false;
        }
        else if (tail.Count != 0)
        {
            tail.Last().position = currentPos;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void AudioPlay () {
        if (!ate) {
            audioSource1.clip = (direction.x != 0) ? audioClips [1] : audioClips [0];
            audioSource1.Play ();
        } else {
            audioSource2.clip = audioClips [2];
            audioSource2.Play ();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.name.StartsWith("Food"))
        {
            ate = true;
            Destroy(collision.gameObject);
            SpawnFood.EatOne();
        }
        else
        {
            GameController.FailGame();
            audioSource2.clip = audioClips [3];
            audioSource2.Play ();
        }
    }
}

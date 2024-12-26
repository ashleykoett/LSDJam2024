using UnityEngine;
using System.Collections;

public class VariableWalk : MonoBehaviour
{

    [SerializeField]
    private AudioSource playerAudio;

    [SerializeField]
    private AudioClip stepEffect;

    public bool playStep = false;

    public float repeatRate = 1f;

    [SerializeField]

    private FPController playerController;

    private Coroutine soundCoroutine;

    private Coroutine walkRandomizer;

    [Header("Walk Rate Settings")]
    
    [SerializeField]
    private Vector2[] allRates;


    void Start()
    {
        playerController = GetComponent<FPController>();
        StartCoroutine(SetWalkrate());
    }

    void Update()
    {
        playStep = playerController.playerWalking;
        // Start or stop the coroutine based on playSound
        if (playStep && soundCoroutine == null)
        {
            soundCoroutine = StartCoroutine(PlaySoundEffect());
        }
        else if (!playStep && soundCoroutine != null )
        {
            StopCoroutine(soundCoroutine);
            soundCoroutine = null;
        }

    }

    private IEnumerator PlaySoundEffect()
    {
        while (playStep)
        {
            if (playerAudio != null && !playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(stepEffect);
            }
            yield return new WaitForSeconds(repeatRate);
        }
    }

      private IEnumerator SetWalkrate()
    {
        while (true)
        {
            RandomizeSpeed();
            yield return new WaitForSeconds(Random.Range(.8f, 5f));
        }
    }

    void RandomizeSpeed()
    {
        Vector2 thisSpeed = allRates[Random.Range(0,allRates.Length)];
        playerController.moveSpeed = thisSpeed.x;
        repeatRate = thisSpeed.y;
    }
}
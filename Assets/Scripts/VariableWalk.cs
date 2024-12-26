using UnityEngine;
using System.Collections;

public class VariableWalk : MonoBehaviour
{

    [SerializeField]
    private AudioSource playerAudio;

    [SerializeField]
    private AudioClip stepEffect;

    [SerializeField] [Range(.8f, 1.2f)]
    private float walkModifier;

    public bool playStep = false;

    public float repeatRate = 1f;

    [SerializeField]

    private FPController playerController;

    private Coroutine soundCoroutine;

    void Start()
    {
        playerController = GetComponent<FPController>();
    }

    void Update()
    {
        playStep = playerController.playerWalking;
        // Start or stop the coroutine based on playSound
        if (playStep && soundCoroutine == null)
        {
            soundCoroutine = StartCoroutine(PlaySoundEffect());
        }
        else if (!playStep && soundCoroutine != null && !playerAudio.isPlaying)
        {
            StopCoroutine(soundCoroutine);
            soundCoroutine = null;
        }
    }

    private IEnumerator PlaySoundEffect()
    {
        while (playStep)
        {
            if (playerAudio != null)
            {
                playerAudio.PlayOneShot(stepEffect);
            }
            yield return new WaitForSeconds(repeatRate);
        }
    }
}
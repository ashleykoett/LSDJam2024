using UnityEngine;
using TMPro;

public class ClockChanger : MonoBehaviour
{

    [SerializeField]
    private TMP_Text clockFace;

    private int twentyFourHour = 23;

    private int sixtyMinute = 59;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartTime()
    {
        int thisHour = Random.Range(0,twentyFourHour);
        int thisMinute = Random.Range(1, sixtyMinute);

    }
}

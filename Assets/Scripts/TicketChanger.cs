using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TicketChanger : MonoBehaviour
{

[SerializeField]
private Animator anim;

[SerializeField]
private TMP_Text gateNumber;

public string[] terminalLetters;

public string[] gateNumbers;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            GenerateGate();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        
        {
            CheckTicket();
        }
    }

    public void GenerateGate()
    {

        string newGate = terminalLetters[(Random.Range(0, terminalLetters.Length))] + gateNumbers[(Random.Range(0, gateNumbers.Length))]; 
        gateNumber.text = newGate;
    }

    public void CheckTicket()
    {
        anim.SetTrigger("ToggleTicket");

    }
}

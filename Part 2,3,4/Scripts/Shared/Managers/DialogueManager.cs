using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;


public enum Speaker
{
    Player,
    Friend,
    Unknown
}

[System.Serializable]
public class DialogueData
{
    public int DialogueID;
    public Speaker speaker;
    [TextArea(2, 5)] public string text;
}


public class DialogueManager : MonoBehaviour
{
    
    public TMP_Text subText;

    public AudioClip typingSFX;

    public bool IsDialogueActive;

    private Coroutine activeDialogueRoutine;


    public void StartDialogue(DialogueData[] dialogues, float letterDelay = 0.025f)
    {
        if(activeDialogueRoutine != null)
        {
            StopCoroutine(activeDialogueRoutine);

            if(GameManager.instance != null && GameManager.instance.audioManager != null)
            {
                GameManager.instance.audioManager.StopDialogueLoop();
            }
        }

        activeDialogueRoutine = StartCoroutine(TypeDialougeRoutine(dialogues, letterDelay));

    }

    public IEnumerator TypeDialougeRoutine(DialogueData[] dialogues, float letterDelay = 0.025f)
    {
        
        IsDialogueActive = true; // tell script we are writing a dialogue

        foreach(DialogueData data in dialogues)
        {
            
            subText.text = $"<mark=#000000>{data.text}</mark>";

            subText.maxVisibleCharacters = 0;
            subText.ForceMeshUpdate();
            int totalVisibleCharacters = subText.textInfo.characterCount;

            if(GameManager.instance != null && GameManager.instance.audioManager != null && typingSFX != null)
            {
                GameManager.instance.audioManager.StartDialogueLoop(typingSFX);
            }

            for(int i = 0; i < totalVisibleCharacters; i++)
            {
                if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    subText.maxVisibleCharacters = totalVisibleCharacters;
                    yield return null;
                    break;
                }

                subText.maxVisibleCharacters = i;
                yield return new WaitForSecondsRealtime(letterDelay);

            }

            if(GameManager.instance != null && GameManager.instance.audioManager != null && typingSFX != null)
            {
                GameManager.instance.audioManager.StopDialogueLoop();
            }

            while (!Input.GetMouseButtonDown(0) || !Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }

            yield return null;


        }

        subText.text = "";
        IsDialogueActive = false;
        activeDialogueRoutine = null;

    }

}

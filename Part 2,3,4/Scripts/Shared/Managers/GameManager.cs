using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;


    [Header("Manager Refrences")]
    public AudioManager audioManager;
    public ObjectiveManager objectiveManager;
    public DialogueManager dialogueManager;


    private void Awake()
    {
        
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject); // It won't destroy This gameobject, when we load the next scene or any other scenes

    }


}

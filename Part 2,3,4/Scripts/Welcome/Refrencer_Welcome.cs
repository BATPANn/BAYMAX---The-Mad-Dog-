using UnityEngine;

public class Refrencer_Welcome : MonoBehaviour
{
    
    public static Refrencer_Welcome instance;


    void Awake()
    {
        if(instance == null) instance = this;
        else { Destroy(gameObject); }
    }


}

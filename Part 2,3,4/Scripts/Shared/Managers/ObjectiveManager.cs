using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public TMP_Text bigRedText;
    public TMP_Text[] smallYellowText;

    public void SetBigRedText(string inputtext)
    {
        if(bigRedText != null)
        {
            bigRedText.text = inputtext;
        }
    }

    public void SetSmallYellowText(int index, string inputtext)
    {
        if(smallYellowText != null && index >= 0 && index < smallYellowText.Length)
        {
            if(smallYellowText[index] != null)
            {
                smallYellowText[index].text = inputtext;
            }
        }
    }

    public void ResetBigRedText()
    {
        if(bigRedText != null)
        {
            bigRedText.text = "";
        }
    }

    public void ResetSmallYellowTexts(int index)
    {
        if(smallYellowText != null && index >= 0 && index < smallYellowText.Length)
        {
            if(smallYellowText[index] != null)
            {
                smallYellowText[index].text = "";
            }
        }
    }

    public void ResetAll()
    {
        ResetBigRedText();
        
        if(smallYellowText != null)
        {
            for(int i = 0; i < smallYellowText.Length; i++) // 0 1 2
            {
                if(smallYellowText[i] != null)
                {
                    smallYellowText[i].text = "";
                }
            }
        }
    }

    // smy[0].text = "";
    // smy[1].text = "";
    // ...


}

using UnityEngine;
using UnityEngine.UI;



public class FadeToBlack : MonoBehaviour
{
    [Tooltip("Put image form canvas\nmake sure canvas image covers everything")]
    public Image fadeToBlackImage;
    
    private Color fader = Color.clear;
    private float colorFader = 0;
    [Tooltip("Multiply with deltaTime\nto get a customizable timer for the fade")]
    [Range(0.01f, 2f)]public float faderSpeed;
    private void Start()
    {
        fadeToBlackImage = GetComponent<Image>();
        Debug.Log("Fade to black in enable: "+fadeToBlackImage);
    }

    public bool FadeIn()
    {
       
        Debug.Log("Amount of times FadeIn is called: " );
        fadeToBlackImage ??= GetComponent<Image>();
        if (fadeToBlackImage.color != Color.clear)
        {
            colorFader -= Time.deltaTime * faderSpeed;
            fadeToBlackImage.color = fader;
            fader = new Color(0f, 0f, 0f, colorFader);
            if(colorFader < Color.clear.a) fadeToBlackImage.color = Color.clear;
        }

        return fadeToBlackImage.color == Color.clear;
    }

    public bool FadeOut()
    {
        Debug.Log("Amount of time FadeOut is called: ");
        fadeToBlackImage ??= GetComponent<Image>();

        if (fadeToBlackImage.color != Color.black)
        {
            colorFader += Time.deltaTime * faderSpeed;
            fadeToBlackImage.color = fader;
            fader = new Color(0f, 0f, 0f, colorFader);
            if(colorFader > Color.black.a) fadeToBlackImage.color = Color.black;
        }

        return fadeToBlackImage.color == Color.black;
    }
}

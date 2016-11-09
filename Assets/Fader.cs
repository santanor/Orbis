using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour {

    public float SourceAlpha;
    public float TargetAlpha;
    public float Delta;
    public Image RenderTarget;
    private float currentAlpha;

    public bool Loop;
    public bool PlayOnAwake;
    private bool isFading;

	// Use this for initialization
	void Start () {
        if (PlayOnAwake)
        {
            isFading = true;
        }
	}

    void Update()
    {
        if (isFading)
        {
            currentAlpha = Mathf.Lerp(SourceAlpha, TargetAlpha, Delta);
            if (Loop)
            {
                if (Mathf.Abs(SourceAlpha - TargetAlpha) <= 0.05f)
                {
                    var swap = SourceAlpha;
                    SourceAlpha = TargetAlpha;
                    TargetAlpha = swap;
                }
            }
            else
            {
                if (Mathf.Abs(SourceAlpha - TargetAlpha) <= 0.05f)
                {
                    currentAlpha = TargetAlpha; 
                }
            }
            RenderTarget.color = new Color(RenderTarget.color.r, RenderTarget.color.g, RenderTarget.color.b, currentAlpha);
        }
    }

    public void StartFade()
    {
        isFading = true;
    }


}

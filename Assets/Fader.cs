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
            RenderTarget.color = new Color(RenderTarget.color.r, RenderTarget.color.g, RenderTarget.color.b, SourceAlpha);
        }
        currentAlpha = SourceAlpha;
	}

    void Update()
    {
        if (isFading)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, TargetAlpha, Delta);
            if (Loop)
            {
                if (Mathf.Abs(SourceAlpha - TargetAlpha) <= Delta)
                {
                    var swap = SourceAlpha;
                    SourceAlpha = TargetAlpha;
                    TargetAlpha = swap;
                    currentAlpha = SourceAlpha;
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

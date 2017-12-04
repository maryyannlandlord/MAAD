using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TransBetween : MonoBehaviour
{

    private List<Material> m_Material;
    private Renderer[] renderers;
    private List<Color> Fossil_colorEnd;
    private List<Color> Alpha_colorEnd; 
    private List<Color> colorStart;
    private List<Color> ogColor; 

    
    public float FadeOutdur = 3.0f;
    public float FadeIndur = 3.0f;
    public float Fossildur = 3.0f;
    public float FossilwaitTime = 0.0f;
    public float FadewaitTime = 0.0f;

    private double t = 0.0;
    private double m = 0.0;
    private double n = 0.0;
    private float timer = 0;
    private float timerMax = 0;

    private bool colStartCheck = false;
    private bool alphaCheck = false;
    private float alpha; 

    // Use this for initialization
    void Start()
    {

        renderers = this.GetComponentsInChildren<Renderer>();
        LoadMaterials();

    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (Time.time < 3)
        {
            Fossilize();
            Debug.Log("time: " + Time.time);
        }
        else if (Time.time < 4)
        { FadeOut(); }
        else if (Time.time < 7)
        {
            FadeIn(); 
        }*/

        ////////////////////NOTE/////////////////////
        // if new function called before animation //
        // is over, colStartCheck != false and     //
        // colStart is not reset. COLOR is WRONG   //
        /////////////////////////////////////////////

    }
    public void LoadMaterials()
    {
        if (m_Material == null)
        {
            m_Material = new List<Material>();
            ogColor = new List<Color>(); 
            

            foreach (Renderer r in renderers) // go through all the renderers found in the children 
            {

                m_Material.AddRange(r.GetComponent<Renderer>().materials); // add all child materials to list 

            }

            foreach (Material j in m_Material)
            {
                float n_alpha = j.color.a; 
               ogColor.Add(new Color((float)j.color.r, (float)j.color.g, (float)j.color.b, n_alpha));
            }

        }
    }

    public void FadeOut() // Can call any time!
    {

        if (colStartCheck == false) updateColStart();

        if (m < FadeOutdur)
        {

            foreach (Material mFade in m_Material)
            {

                mFade.color = new Color(mFade.color.r, mFade.color.g, mFade.color.b, (float)((FadeOutdur - m) / FadeOutdur));
                 m += Time.deltaTime;

            }

        }
        else {
            // reset updateColStart
            colStartCheck = false;
        }
        return;
    }

    public void FadeIn()
    {
        int colFadePos = 0;

        if (colStartCheck == false) updateColStart();

        if (alphaCheck == false)
        {
            alpha = m_Material[0].color.a;
            alphaCheck = true;
        }

        if (n < FadeIndur)
        {

            foreach (Material nFade in m_Material)
            {

                Color fadeInCol = Color.Lerp(colorStart[colFadePos], ogColor[colFadePos], (float)(n / FadeIndur));

                fadeInCol.a = (alpha + (1 - alpha) * ((float) n / FadeIndur));
                nFade.color = fadeInCol; 

                n += Time.deltaTime;

                colFadePos++;
            }

        }
        else {
            alphaCheck = false;
            colStartCheck = false; 
        }

    }

    public void Fossilize()
    {
        int colTransPos = 0;

        // if updateColStart not called then updateColStart 
        if (colStartCheck == false) updateColStart();

        if (t < Fossildur)
        {
            foreach (Material mFossil in m_Material)
            {
                
                float alpha = mFossil.color.a; 

                Color fossilCol = Color.Lerp(colorStart[colTransPos], Fossil_colorEnd[colTransPos], (float)(t / Fossildur));

                fossilCol.a = alpha;
                mFossil.color = fossilCol; 

                t += Time.deltaTime;


                colTransPos++;
            }

        }
        else {
            //reset updateColStart 
            colStartCheck = false; 
        }
        return;
    }


    public void updateColStart() {

        colorStart = new List<Color>();
        Alpha_colorEnd = new List<Color>();
        Fossil_colorEnd = new List<Color>();

        foreach (Material j in m_Material)
        {
            colorStart.Add(j.color);

            Alpha_colorEnd.Add(new Color((float)j.color.r, (float)j.color.g, (float)j.color.b, 0.0f));
            Fossil_colorEnd.Add(new Color((float)0.8778114, (float)0.9044118, (float)0.9044118, 1.0f));
        }
        colStartCheck = true; 
    }


    public bool Wait(float seconds)
    {
        timerMax = seconds;
        timer += Time.deltaTime;

        if (timer >= timerMax)
        { return true; }
        return false;

    }
}

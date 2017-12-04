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

    
    public float Fadedur = 3.0f;
    public float Fossildur = 3.0f;
    public float FossilwaitTime = 0.0f;
    public float FadewaitTime = 0.0f;
    private double t = 0.0;
    private double m = 0.0;
    private float timer = 0;
    private float timerMax = 0;
    private bool colStartCheck = false; 

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
        else if (Time.time < 5)
        { Fading(); }
        else if (Time.time < 7)
        {
            Fossilize(); 
        }*/

    }
    public void LoadMaterials()
    {
        if (m_Material == null)
        {
            m_Material = new List<Material>();

            Fossil_colorEnd = new List<Color>(); 

            foreach (Renderer r in renderers) // go through all the renderers found in the children 
            {

                m_Material.AddRange(r.GetComponent<Renderer>().materials); // add all child materials to list 

            }

        }
    }

    public void Fading()
    {
        int colFadePos = 0;

        // if updateColStart not called then updateColStart 
        if (colStartCheck == false) updateColStart();

        if (m < Fadedur)
        {

            foreach (Material mFade in m_Material)
            {

                mFade.color = new Color(mFade.color.r, mFade.color.g, mFade.color.b, (float)((Fadedur - m) / Fadedur));
                 m += Time.deltaTime;


                colFadePos++;
            }

        }
        else {
            // reset updateColStart
            colStartCheck = false;
        }
        return;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FadeObject : MonoBehaviour
{

    private List<Material> m_Material;
    private Renderer[] renderers;
    private List<Color> colorEnd;
    private List<Color> colorStart;

    
    public double duration = 3.0;
    public float waitTime = 3.0f;
    private double t = 0.0;
    private float timer = 0;
    private float timerMax = 0;

    // Use this for initialization
    void Start()
    {

        renderers = this.GetComponentsInChildren<Renderer>();
        //Debug.Log("length of renderers:" + renderers.Length);
        LoadMaterials();

    }

    // Update is called once per frame
    void Update()
    {

        if (Wait(waitTime))
        { Fading(); }


    }
    public void LoadMaterials()
    {
        if (m_Material == null)
        {
            m_Material = new List<Material>();
            colorStart = new List<Color>();
            colorEnd = new List<Color>();

            foreach (Renderer r in renderers) // go through all the renderers found in the children 
            {

                m_Material.AddRange(r.GetComponent<Renderer>().materials); // add all child materials to list 

            }

            foreach (Material j in m_Material)
            {
                colorStart.Add(j.color);
                colorEnd.Add(new Color((float)j.color.r, (float)j.color.g, (float)j.color.b, 0.0f));
                        

            }
        }
    }

    public void Fading()
    {
        int colFadePos = 0;

        if (t < duration)
        {
            foreach (Material mFade in m_Material)
            {

                mFade.color = Color.Lerp(colorStart[colFadePos], colorEnd[colFadePos], (float)(t / duration));
                //Debug.Log(mFade + ":" + mFade.color);
                t += Time.deltaTime;
                Debug.Log("time delta(fade) : " + Time.deltaTime);
                Debug.Log("t: " + t);
                
                colFadePos++;
            }
           
        }
        return;
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

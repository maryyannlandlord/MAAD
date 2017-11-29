using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Fade : MonoBehaviour
    {
        
        private Material m_Material;
        //private Renderer[] renderers; 
 
        Color colorEnd;
        Color colorStart;
        public double duration = 45.0;
        public float sec = 3.0f;
        private double t = 0.0;
        private float timer = 0;
        private float timerMax = 0;
        private int arrayPos = 0; 
        private int colFadePos = 0;

        //private int matLength; 
        //private int counter = 0; 

        //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////
        //FadeColors fader;
        //private IEnumerator coroutine;
        //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////

        // Use this for initialization
        void Start()
        {



        //renderers = this.GetComponentsInChildren<Renderer>();
        //UpdateMaterials();

        m_Material = this.GetComponent<Renderer>().material;

        colorStart = m_Material.color;
        colorEnd = new Color((float)colorStart.r, (float)colorStart.g, (float)colorStart.b, 0.0f);


        /*
        foreach (Material m in m_Material)
        {

            colorStart[arrayPos] = m.color;
            colorEnd[arrayPos] = new Color((float)colorStart[arrayPos].r, (float)colorStart[arrayPos].g, (float)colorStart[arrayPos].b, 0.0f);
            arrayPos++; 
        }
        */


        //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////
        //fader = this.GetComponent<FadeColors>();
        //coroutine = FadeOut();
        //StartCoroutine(coroutine);
        //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////

    }

    // Update is called once per frame
    void Update()
        {
        //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////
        //fader.FadeOut(true);
        //coroutine = FadeOut();
        //StartCoroutine(coroutine);
        //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////

        if (!Wait(3)) Fading(); 


        }
        /*public void UpdateMaterials()
        {

            if (m_Material == null)
            {
                m_Material = new Material[renderers.Length];

                foreach (Renderer r in renderers) //go through all the renderers found in the children 
                {

                    matLength = (r.GetComponent<Renderer>().material).Length;

                    Debug.Log( r + ": " + matLength);

                }
            }
        }*/

        public void Fading()
        {
            Debug.Log("fading");

            if (t < duration)
            {
                m_Material.color = Color.Lerp(colorStart, colorEnd, (float)(t / duration));
                t += Time.deltaTime;

            /*foreach (Material mFade in m_Material)
            {
                mFade.color = Color.Lerp(colorStart[colFadePos], colorEnd[colFadePos], (float)(t / duration));
                t += Time.deltaTime;
                colFadePos++;
            }*/

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


    //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////
    /*
    private IEnumerator FadeOut()
    {
        Debug.Log("Fading out");
        for (double t = 0.0; t < duration; t += Time.deltaTime)
        {
            m_Material.color = Color.Lerp(colorStart, colorEnd, (float) (t / duration));
            yield return null;
        }
    }*/
    //////////////////////////////////////////////////IGNORE////////////////////////////////////////////////////////////////////

}

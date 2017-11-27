using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Fade : MonoBehaviour
    {
        
        Material[] m_Material;
        Color[] colorEnd;
        Color[] colorStart;
        public double duration = 45.0;
        public float sec = 3.0f;
        private double t = 0.0;
        private float timer = 0;
        private float timerMax = 0;
        private int arrayPos = 0; 
        private int colFadePos = 0;

        private int counter = 0; 

        //FadeColors fader;

        private IEnumerator coroutine;

        // Use this for initialization
        void Start()
        {
            foreach (Renderer r in this.GetComponentsInChildren<Renderer>())
            {
                m_Material = r.sharedMaterials;
                Debug.Log("materials: " + m_Material[counter]);
               // if(counter < m_Material.Length) counter++; 
            }

        foreach (Material m in m_Material) {
            
            colorStart[arrayPos] = m.color;
            colorEnd[arrayPos] = new Color((float)colorStart[arrayPos].r, (float)colorStart[arrayPos].g, (float)colorStart[arrayPos].b, 0.0f);
            arrayPos++; 
        }
            

            //fader = this.GetComponent<FadeColors>();
            //coroutine = FadeOut();
            //StartCoroutine(coroutine);

        }

        // Update is called once per frame
        void Update()
        {
            //fader.FadeOut(true);
            
            //coroutine = FadeOut();
            //StartCoroutine(coroutine);

            //if (!Wait(3)) Fading(); 


        }

        public void Fading()
        {
            Debug.Log("fading");

            if (t < duration)
            {
                foreach (Material mFade in m_Material)
                {
                    mFade.color = Color.Lerp(colorStart[colFadePos], colorEnd[colFadePos], (float)(t / duration));
                    t += Time.deltaTime;
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
    
}

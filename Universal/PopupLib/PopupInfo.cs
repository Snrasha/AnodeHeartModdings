using UnityEngine;

namespace Universal
{
    public class PopupInfo: MonoBehaviour
    {

        private static PopupInfo instance;

        private string currentTextToDisplay;
        private float coldown = 0f;
        public void Awake()
        {
            instance = this;
        }

        
        public void OnDestroy()
        {
            instance = null;
        }
        public void CallInner(string text)
        {


            currentTextToDisplay = text;
            this.enabled = true;
            coldown = 3f;

        }
        public static void Call(string text)
        {
            if (instance != null)
            {
                instance.CallInner(text);
            }

        }

        void OnGUI()
        {
            coldown -= Time.deltaTime;
            if (coldown < 0)
            {
                this.enabled = false;
            }
            GUI.Label(new Rect(10, 10, 150, 100), currentTextToDisplay);

        }
    }
}

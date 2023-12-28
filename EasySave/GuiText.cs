using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EasySave
{
    internal class GuiText: MonoBehaviour
    {

        public string currentTextToDisplay;
        private float coldown = 0f;
      //  private GUIStyle GUIStyle;

        public void Awake()
        {
         //   GUIStyle=new GUIStyle();
           // GUIStyle.
        }

        public void Call(string text)
        {
            currentTextToDisplay = text;
            this.enabled = true;
            coldown = 3f;

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

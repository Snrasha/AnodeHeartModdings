
using UnityEngine;

namespace Universal.ModMenuLib
{
    public abstract class SettinsOptionTogglable: SettingsOption
    {
        public  bool[] flags = new bool[2];

        public void DisableButton()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Equals("Value"))
                {
                    child.gameObject.SetActive(false);
                }
                if (child.gameObject.name.Equals("ArrowLeft"))
                {
                    flags[0] = child.gameObject.activeSelf;
                    child.gameObject.SetActive(false);
                }
                if (child.gameObject.name.Equals("ArrowRight"))
                {
                    flags[1] = child.gameObject.activeSelf;
                    child.gameObject.SetActive(false);
                }
            }

            this.enabled = false;
        }
        public void EnableButton()
        {
            if (this.enabled)
            {
                return;
            }

            foreach (Transform child in transform)
            {
                if (child.gameObject.name.Equals("Value"))
                {
                    child.gameObject.SetActive(true);
                }
                if (child.gameObject.name.Equals("ArrowLeft"))
                {
                    child.gameObject.SetActive(flags[0]);
                }
                if (child.gameObject.name.Equals("ArrowRight"))
                {
                    child.gameObject.SetActive(flags[1]);
                }
            }

            this.enabled = true;
        }
    }
}

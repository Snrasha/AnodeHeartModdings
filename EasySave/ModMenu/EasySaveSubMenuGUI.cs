
using EasySave.Langs;
using UnityEngine;
using Universal.ModMenu;
using Universal.ModMenuLib;


namespace EasySave.ModMenu
{
    public class EasySaveSubMenuGUI : SubModMenuInterface
    {


        public EasySaveLayoutFlag followerLayoutFlag;



        public EasySaveSubMenuGUI()
        {
        }

  


        public bool CheckIfExist(OptionsController optionsController)
        {
            GameObject childMod = null;

            GameObject frame = optionsController.gameObject.transform.GetChild(0).gameObject;
            foreach (Transform child in frame.transform)
            {
                if (child.gameObject.name.Equals(ModMenuGUI.name))
                {
                    childMod = child.gameObject;
                }
            }
            if (childMod != null)
            {

                followerLayoutFlag = childMod.GetComponent<EasySaveLayoutFlag>();

            }


            return followerLayoutFlag != null;
        }

        public void CreateModMenu(OptionsController optionsController)
        {
            GameObject childMod = null;

            GameObject frame = optionsController.gameObject.transform.GetChild(0).gameObject;
            foreach (Transform child in frame.transform)
            {
                if (child.gameObject.name.Equals(ModMenuGUI.name))
                {
                    childMod = child.gameObject;
                }
            }
            if (childMod != null)
            {
                followerLayoutFlag= childMod.GetComponent<EasySaveLayoutFlag>();
            }
            if (followerLayoutFlag == null)
            {

                ModMenuGUI.AddCheckLayout(typeof(EasySaveLayoutFlag));

                GameObject layout=ModMenuGUI.AddLayout("EasySaveLayout");

                ModMenuGUI.CreateText("TextEasyTitle", EasySaveLang.EasySave_title_plugin, ModMenuGUI.overlaytitle, layout);
                ModMenuGUI.CreateText("TextEasyDesc", EasySaveLang.EasySave_desc_plugin, ModMenuGUI.overlayDesc, layout);


                //optionTamas1.enabled = true;
                //optionTamas2.enabled = true;

            }


        }


        public void LoadOptions()
        {

        }

        public void LoadConfigFile()
        {
         
        }
        public void SaveConfigFile()
        {

        }
      
        public void OnExitOptionsMenu()
        {
        }

        public void OnEnterOptionsMenu()
        {
        }
    }


}

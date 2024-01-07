using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universal.ModMenuLib
{
    public interface SubModMenuInterface
    {


        public void LoadConfigFile();
        public void SaveConfigFile();

        public void LoadOptions();

        public bool CheckIfExist(OptionsController optionsController);

        public void CreateModMenu(OptionsController optionsController);

        public void OnExitOptionsMenu();
        public void OnEnterOptionsMenu();
    }
}

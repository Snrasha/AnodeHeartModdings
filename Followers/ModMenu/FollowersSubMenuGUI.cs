using HarmonyLib;
using MonoMod.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using Universal.EventBusLib;
using Universal.ModMenuLib;
using static EventBus;
using static UnityEngine.UIElements.UIRAtlasManager;

namespace Followers.ModMenu
{
    public class FollowersSubMenuGUI : SubModMenuInterface
    {

        public Config config;
        public string pathConfig;
        private string pathFolderConfig = "/../BepInEx/plugins/FollowersConfig";
        private string configname = "/Config.json";

        public OptionTamas1 optionTamas1;
        public OptionTamas2 optionTamas2;
        public FollowerLayoutFlag followerLayoutFlag;

        public static bool EventBusOptionsAdded = false;





        public FollowersSubMenuGUI()
        {
            pathConfig = Application.dataPath + pathFolderConfig;

            LoadOptions();
            UniEventBus.AddTypesToEventBus("Followers", new Type[] { typeof(OptionTamas1), typeof(OptionTamas2) });

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

                followerLayoutFlag = childMod.GetComponent<FollowerLayoutFlag>();

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
                followerLayoutFlag= childMod.GetComponent<FollowerLayoutFlag>();
            }
            if (followerLayoutFlag == null)
            {

                ModMenuGUI.AddCheckLayout(typeof(FollowerLayoutFlag));

                GameObject layout=ModMenuGUI.AddLayout("FollowerLayout");

                ModMenuGUI.CreateText("TextFollowersTitle", "Followers Mod Options", layout);
                optionTamas1 = ModMenuGUI.CreateButton( typeof(OptionTamas1),"FollowersBtn", "Followers",layout).GetComponent<OptionTamas1>();
                optionTamas2 = ModMenuGUI.CreateButton( typeof(OptionTamas2),"ChoiceBtn", "Choice", layout).GetComponent<OptionTamas2>();
                //optionTamas1.enabled = true;
                //optionTamas2.enabled = true;

            }


        }


        public void LoadOptions()
        {
            if (!Directory.Exists(pathConfig))
            {
                Directory.CreateDirectory(pathConfig);
            }
            
            LoadConfigFile();
            
            
        }

        public void LoadConfigFile()
        {
            if (File.Exists(pathConfig+configname))
            {
                try
                {
                    using (Stream stream = File.OpenRead(pathConfig + configname))
                    {
                        byte[] bytes = new byte[stream.Length];

                        stream.Read(bytes, 0, bytes.Length);

                        string str = Encoding.ASCII.GetString(bytes);
                        try
                        {
                            config = JsonConvert.DeserializeObject<Config>(str);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Error parsing {pathConfig}: {ex.Message}");
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                config = new Config();
                config.option_followers = 0;
                config.option_species = Species.Beebee.ToString();
            }
        }
        public void SaveConfigFile()
        {
            try
            {
                string textjson=JsonConvert.SerializeObject(config);
                
                using (Stream stream = File.Open(pathConfig + configname, FileMode.Create))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(textjson);

                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error saving {pathConfig}: {e.Message}");
            }
        }


        public class SubclassEventBus
        {
            public static readonly SubclassEventBus subclassEventBus = new SubclassEventBus();

            internal bool unknownmethod(Type x)
            {
                if (x != typeof(IEventReceiverBase))
                {
                    return typeof(IEventReceiverBase).IsAssignableFrom(x);
                }
                return false;
            }
        }
        public static void AddOptionsToEventBus()
        {
            if (EventBusOptionsAdded)
            {
                return;
            }
            EventBusOptionsAdded = true;

            Type[] types = new Type[] { typeof(OptionTamas1), typeof(OptionTamas2) };


            Dictionary<Type, ClassMap> class_register_map = new Dictionary<Type, ClassMap>();
            Dictionary<Type, Action<IEvent>> cached_raise = new Dictionary<Type, Action<IEvent>>();
            Dictionary<Type, BusMap> dictionary = new Dictionary<Type, BusMap>();


            Type typeFromHandle = typeof(Action<>);
            Type type = typeFromHandle.MakeGenericType(typeof(IEventReceiverBase));
            Type type2 = typeFromHandle.MakeGenericType(typeof(IEvent));
            Type[] array = types;
            Type[] types2 = Assembly.GetAssembly(typeof(OptionsController)).GetTypes();

            foreach (Type type3 in array)
            {
                if (type3 != typeof(IEvent) && typeof(IEvent).IsAssignableFrom(type3))
                {
                    Type type4 = typeof(EventBus<>).MakeGenericType(type3);
                    RuntimeHelpers.RunClassConstructor(type4.TypeHandle);
                    BusMap value = new BusMap
                    {
                        register = (Delegate.CreateDelegate(type, type4.GetMethod("Register")) as Action<IEventReceiverBase>),
                        unregister = (Delegate.CreateDelegate(type, type4.GetMethod("UnRegister")) as Action<IEventReceiverBase>)
                    };
                    MethodInfo method = type4.GetMethod("RaiseAsInterface");
                    cached_raise.Add(type3, (Action<IEvent>)Delegate.CreateDelegate(type2, method));
                }
            }
            array = types2;
            foreach (Type type3 in array)
            {
                if (type3 != typeof(IEvent) && typeof(IEvent).IsAssignableFrom(type3))
                {
                    Type type4 = typeof(EventBus<>).MakeGenericType(type3);
                    RuntimeHelpers.RunClassConstructor(type4.TypeHandle);
                    BusMap value = new BusMap
                    {
                        register = (Delegate.CreateDelegate(type, type4.GetMethod("Register")) as Action<IEventReceiverBase>),
                        unregister = (Delegate.CreateDelegate(type, type4.GetMethod("UnRegister")) as Action<IEventReceiverBase>)
                    };
                    dictionary.Add(type3, value);
                }
            }

            array = types;
            foreach (Type type5 in array)
            {

                if (typeof(IEventReceiverBase).IsAssignableFrom(type5) && !type5.IsInterface)
                {
                   
                    Type[] array2 = type5.GetInterfaces().Where(SubclassEventBus.subclassEventBus.unknownmethod).ToArray();
                    ClassMap classMap = new ClassMap
                    {
                        buses = new BusMap[array2.Length]
                    };
                    for (int j = 0; j < array2.Length; j++)
                    {
                        Type key = array2[j].GetGenericArguments()[0];
                        classMap.buses[j] = dictionary[key];
                    }
                    class_register_map.Add(type5, classMap);

                }
            }

            EventBus.class_register_map.AddRange(class_register_map);
            EventBus.cached_raise.AddRange(cached_raise);
        }

        public void OnExitOptionsMenu()
        {
            FollowersPlugin.UpdateFollowersGroup();
        }

        public void OnEnterOptionsMenu()
        {
            if (optionTamas1 != null)
            {
                optionTamas1.getStartingOption();
            }
        }
    }


}

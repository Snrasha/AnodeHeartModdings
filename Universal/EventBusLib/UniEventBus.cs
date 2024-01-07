using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static EventBus;

namespace Universal.EventBusLib
{
    public static class UniEventBus
    {
        public static bool EventBusOptionsAdded = false;
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

        private static Dictionary<string, Type[]> eventBusDict;


        public static void AddTypesToEventBus(string nameMod, Type[] types)
        {
            if (eventBusDict == null)
            {
                eventBusDict = new Dictionary<string, Type[]>();
            }
            if (eventBusDict.ContainsKey(nameMod))
            {
                eventBusDict[nameMod] = types;
                return;
            }
            eventBusDict.Add(nameMod, types);
        }


        public static void __AddOptionsToEventBus()
        {
            if (EventBusOptionsAdded)
            {
                return;
            }
            EventBusOptionsAdded = true;

            if (eventBusDict == null)
            {
                eventBusDict = new Dictionary<string, Type[]>();
            }

            // Type[] types = new Type[] { typeof(OptionTamas1), typeof(OptionTamas2) };


            Dictionary<Type, ClassMap> class_register_map = new Dictionary<Type, ClassMap>();
            Dictionary<Type, Action<IEvent>> cached_raise = new Dictionary<Type, Action<IEvent>>();
            Dictionary<Type, BusMap> dictionary = new Dictionary<Type, BusMap>();


            Type typeFromHandle = typeof(Action<>);
            Type type = typeFromHandle.MakeGenericType(typeof(IEventReceiverBase));
            Type type2 = typeFromHandle.MakeGenericType(typeof(IEvent));

            Type[] types2 = Assembly.GetAssembly(typeof(OptionsController)).GetTypes();

            foreach (Type[] array2 in eventBusDict.Values)
            {
                foreach (Type type3 in array2)
                {
                    if (type3 != typeof(IEvent) && typeof(IEvent).IsAssignableFrom(type3))
                    {
                        Type type4 = typeof(EventBus<>).MakeGenericType(type3);
                        RuntimeHelpers.RunClassConstructor(type4.TypeHandle);
                        BusMap value = new BusMap
                        {
                            register = Delegate.CreateDelegate(type, type4.GetMethod("Register")) as Action<IEventReceiverBase>,
                            unregister = Delegate.CreateDelegate(type, type4.GetMethod("UnRegister")) as Action<IEventReceiverBase>
                        };
                        MethodInfo method = type4.GetMethod("RaiseAsInterface");
                        cached_raise.Add(type3, (Action<IEvent>)Delegate.CreateDelegate(type2, method));
                    }
                }
            }
            Type[] array;
            array = types2;
            foreach (Type type3 in array)
            {
                if (type3 != typeof(IEvent) && typeof(IEvent).IsAssignableFrom(type3))
                {
                    Type type4 = typeof(EventBus<>).MakeGenericType(type3);
                    RuntimeHelpers.RunClassConstructor(type4.TypeHandle);
                    BusMap value = new BusMap
                    {
                        register = Delegate.CreateDelegate(type, type4.GetMethod("Register")) as Action<IEventReceiverBase>,
                        unregister = Delegate.CreateDelegate(type, type4.GetMethod("UnRegister")) as Action<IEventReceiverBase>
                    };
                    dictionary.Add(type3, value);
                }
            }

            foreach (Type[] array2 in eventBusDict.Values)
            {
                foreach (Type type5 in array2)
                {

                    if (typeof(IEventReceiverBase).IsAssignableFrom(type5) && !type5.IsInterface)
                    {

                        Type[] array3 = type5.GetInterfaces().Where(SubclassEventBus.subclassEventBus.unknownmethod).ToArray();
                        ClassMap classMap = new ClassMap
                        {
                            buses = new BusMap[array3.Length]
                        };
                        for (int j = 0; j < array3.Length; j++)
                        {
                            Type key = array3[j].GetGenericArguments()[0];
                            classMap.buses[j] = dictionary[key];
                        }
                        class_register_map.Add(type5, classMap);

                    }
                }
            }

            EventBus.class_register_map.AddRange(class_register_map);
            EventBus.cached_raise.AddRange(cached_raise);
        }
    }
}

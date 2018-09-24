using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ControlsLibrary.Factories.Concrete.WinForms.WinHelp
{
    public static class ControlExtensions
    {
        public static T Clone<T>(this T controlToClone)
            where T : Control
        {
            PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            T instance = Activator.CreateInstance<T>();

            foreach (PropertyInfo propInfo in controlProperties)
            {
                if (propInfo.CanWrite)
                {
                    if (propInfo.Name != "WindowTarget"
)
                        propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
                }
            }
            if (controlToClone.Controls.Count > 0)
            {
                instance.Controls.AddRange(controlToClone.CopyChildren());
            }
            return instance;
        }

        private static Control[] CopyChildren(this Control src)
        {
            var controls = src.Controls;
            Control[] result = new Control[controls.Count];
            for (var i = 0; i < controls.Count; i++)
            {
                result[i] = controls[i].Clone();
                result[i].Parent = null;
            }

            return result;
        }

        public static Control GetGodfather(this Control control)
        {
            if (control == null) throw new ArgumentNullException(nameof(control));
            while (control.Parent != null)
            {
                control = control.Parent;
            }
            return control;
        }

        private const string PrefixPattern = "^Event";
        private static Regex _regex = new Regex(PrefixPattern);
        private const string SuffixPattern = "Changed$";
        private static Regex _suffixReg = new Regex(SuffixPattern);

        public static void BindingEvents(Control src, Control dst)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));

            Type t = typeof(Control);

            EventHandlerList srcEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(src);
            EventHandlerList dstEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dst);

            foreach (EventInfo eventInfo in t.GetEvents(BindingFlags.Public | BindingFlags.Instance))
            {
                if(eventInfo.Name == "Disposed") continue;
                FieldInfo fieldInfo = t.GetField("Event" + eventInfo.Name, BindingFlags.NonPublic | BindingFlags.Static)
                                   ?? t.GetField("Event" + _suffixReg.Replace(eventInfo.Name, ""), BindingFlags.NonPublic | BindingFlags.Static); //winforms ♥
                object field = fieldInfo.GetValue(null);

                HandlRouter router = new HandlRouter(field, dstEvents, dst.Name, src.Name, eventInfo.Name);
                MethodInfo handlerInfo = router.GetType().GetMethod("Invoke", new Type[] { typeof(object), typeof(EventArgs) });
                Delegate deleg = Delegate.CreateDelegate(eventInfo.EventHandlerType, router, handlerInfo, true);
                //надо создать делегат задано сигнатуры
                //Delegate combine = Delegate.Combine(deleg, dstEvents[field]);
                srcEvents.AddHandler(field, deleg);
                //eventInfo.AddEventHandler(src, deleg);
            }
        }

        public class HandlRouter
        {
            private readonly object _key;
            private readonly EventHandlerList _eventHandlerList;
            public string DestenationName { get; }
            public string SourceName { get; }
            public string EventName { get; }

            public HandlRouter(object key, EventHandlerList eventHandlerList, string destenationName, string sourceName, string eventName)
            {
                _key = key;
                _eventHandlerList = eventHandlerList;
                DestenationName = destenationName;
                SourceName = sourceName;
                EventName = eventName;
            }

            protected HandlRouter()
            {
            }
            //TODO запускать обработчики события источника ДО обработчиков назначения
            public void Invoke(object sender, EventArgs args)
            {
                string srcName = ((Control)sender).Name;
                var handler = _eventHandlerList[_key];
                if(handler != null)
                {
                    var handlers = handler.GetInvocationList();
                    for (var i = handlers.Length - 1; i >= 0; i--)
                    {
                        var @delegate = handlers[i];
                        @delegate.Method.Invoke(@delegate.Target, new[] {sender, args});
                    }
                }
            }
        }

        public static void BubblingFromParent(this Control control)
        {
            BubblingFromParent(control, BindingEvents);
        }
        public static void TunnelingFromParent(this Control control)
        {
            TunnelingFromParent(control, BindingEvents);
        }

        public static void BubblingFromParent(this Control parent, Action<Control, Control> bindingEvents)
        {
            if (parent.Controls.Count == 0) return;
            foreach (Control child in parent.Controls)
            {
                bindingEvents(child, parent);
                child.BubblingFromParent(bindingEvents);
            }
        }
        public static void TunnelingFromParent(this Control parent, Action<Control, Control> bindingEvents)
        {
            if (parent.Controls.Count == 0) return;
            foreach (Control child in parent.Controls)
            {
                bindingEvents(parent, child);
                child.TunnelingFromParent(bindingEvents);
            }
        }
    }
}
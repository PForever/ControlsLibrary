using System;
using System.CodeDom;
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

                HandlerRouter router = new HandlerRouter(field, dstEvents, dst.Name, src.Name, eventInfo.Name);
                MethodInfo handlerInfo = router.GetType().GetMethod("Invoke", new Type[] { typeof(object), typeof(EventArgs) });
                Delegate deleg = Delegate.CreateDelegate(eventInfo.EventHandlerType, router, handlerInfo, true);
                //надо создать делегат задано сигнатуры
                //Delegate combine = Delegate.Combine(deleg, dstEvents[field]);
                srcEvents.AddHandler(field, deleg);
                //eventInfo.AddEventHandler(src, deleg);
            }
        }
        public static void BindingConcreteEvents(this Control src, Control dst)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));

            Type t = typeof(Control);

            EventHandlerList srcEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(src);
            EventHandlerList dstEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dst);

            string[] eventNames = {"MouseMove", "MouseDown", "MouseClick", "MouseUp", "MouseCaptureChanged", "KeyDown", "KeyPress", "KeyUp" };

            foreach (EventInfo eventInfo in t.GetEvents(BindingFlags.Public | BindingFlags.Instance).Where(e => eventNames.Contains(e.Name)))
            {

                FieldInfo fieldInfo = t.GetField("Event" + eventInfo.Name, BindingFlags.NonPublic | BindingFlags.Static)
                                   ?? t.GetField("Event" + _suffixReg.Replace(eventInfo.Name, ""), BindingFlags.NonPublic | BindingFlags.Static);



                object field = fieldInfo.GetValue(null);

                HandlerRouter router = new HandlerRouter(field, dstEvents, dst.Name, src.Name, eventInfo.Name);
                MethodInfo handlerInfo = router.GetType().GetMethod("Invoke", new Type[] { typeof(object), typeof(EventArgs) });
                Delegate deleg = Delegate.CreateDelegate(eventInfo.EventHandlerType, router, handlerInfo, true);
                srcEvents.AddHandler(field, deleg);
            }
        }
        public static void UnbindingConcreteEvents(this Control src, Control dst)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));

            Type t = typeof(Control);

            EventHandlerList srcEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(src);
            EventHandlerList dstEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dst);

            string[] eventNames = { "ControlAdded", "MouseMove", "MouseDown", "MouseClick", "MouseUp", "MouseCaptureChanged", "KeyDown", "KeyPress", "KeyUp" };

            foreach (EventInfo eventInfo in t.GetEvents(BindingFlags.Public | BindingFlags.Instance).Where(e => eventNames.Contains(e.Name)))
            {

                FieldInfo fieldInfo = t.GetField("Event" + eventInfo.Name, BindingFlags.NonPublic | BindingFlags.Static)
                                   ?? t.GetField("Event" + _suffixReg.Replace(eventInfo.Name, ""), BindingFlags.NonPublic | BindingFlags.Static);

                object field = fieldInfo.GetValue(null);
                Delegate deleg = srcEvents[field];
                foreach (Delegate handler in deleg.GetInvocationList())
                {
                    srcEvents.RemoveHandler(field, handler);
                }
                srcEvents.RemoveHandler(field, deleg);
            }
        }

        public static void ForgetYourFather(this Control control)
        {
            UnbindingConcreteEvents(control, control.Parent);
        }

        public class HandlerRouter
        {
            private readonly object _key;
            private readonly EventHandlerList _eventHandlerList;
            public string DestinationName { get; }
            public string SourceName { get; }
            public string EventName { get; }

            public HandlerRouter(object key, EventHandlerList eventHandlerList, string destinationName, string sourceName, string eventName)
            {
                _key = key;
                _eventHandlerList = eventHandlerList;
                DestinationName = destinationName;
                SourceName = sourceName;
                EventName = eventName;
            }

            protected HandlerRouter()
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
            BubblingFromParent(control, BindingConcreteEvents);
        }
        public static void ForgetAll(this Control control)
        {
            control.ForgetYourFather();
            UnbubblingFromParent(control, UnbindingConcreteEvents);
        }

        public static void TunnelingFromParent(this Control control)
        {
            TunnelingFromParent(control, BindingConcreteEvents);
        }

        public static void UnbubblingFromParent(this Control parent, Action<Control, Control> bindingEvents)
        {
            if (parent.Controls.Count == 0) return;
            foreach (Control child in parent.Controls)
            {
                bindingEvents(child, parent);
                child.UnbubblingFromParent(bindingEvents);
            }
        }
        public static void BubblingFromParent(this Control parent, Action<Control, Control> bindingEvents)
        {
            parent.ControlAdded += ParentOnControlAdded;

            if (parent.Controls.Count == 0) return;
            foreach (Control child in parent.Controls)
            {
                bindingEvents(child, parent);
                child.BubblingFromParent(bindingEvents);
            }
        }

        private static void ParentOnControlAdded(object sender, ControlEventArgs args)
        {
            if(!(sender is Control parent)) throw new WtfException();
            BindingConcreteEvents(args.Control, parent);
            args.Control.BubblingFromParent();
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
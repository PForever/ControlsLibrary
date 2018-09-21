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
    //https://stackoverflow.com/questions/10266589/clone-controls-c-sharp-winform
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
                    if (propInfo.Name != "WindowTarget")
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

        private static void BindingEvents(Control src, Control dst)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));

            Type t = typeof(Control);
            EventHandlerList dstEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(dst);

            EventInfo eventInfo = t.GetEvent("MouseMove");
            FieldInfo fieldInfo = t.GetField("EventMouseMove", BindingFlags.NonPublic | BindingFlags.Static);
            object field = fieldInfo.GetValue(null);
            void DstHandlerInvoke(object sender, MouseEventArgs args)
            {
                MouseEventHandler eventHandler = dstEvents[field] as MouseEventHandler;
                eventHandler?.Invoke(sender, args);
            }
            MouseEventHandler handler = DstHandlerInvoke;
            eventInfo.AddEventHandler(src, handler);

            //EventHandlerList parentEvents = (EventHandlerList)t.GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(parent);

            //foreach (EventInfo eventInfo in t.GetEvents(BindingFlags.Public | BindingFlags.Instance))
            //{
            //    FieldInfo fieldInfo = t.GetField("Event" + eventInfo.Name, BindingFlags.NonPublic | BindingFlags.Static)
            //                       ?? t.GetField("Event" + _suffixReg.Replace(eventInfo.Name, ""), BindingFlags.NonPublic | BindingFlags.Static); //winforms ♥
            //    object field = fieldInfo.GetValue(null);

            //    void ParentsHandlerInvoke(object sender, EventArgs args)
            //    {
            //        EventHandler eventHandler = parentEvents[field] as EventHandler;
            //        eventHandler?.Invoke(sender, args);
            //    }

            //    EventHandler handler = ParentsHandlerInvoke;
            //    //надо создать делегат задано сигнатуры
            //    eventInfo.AddEventHandler(child, ParentsHandlerInvoke);
            //}

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
                child.BubblingFromParent(bindingEvents);
            }
        }
    }
}
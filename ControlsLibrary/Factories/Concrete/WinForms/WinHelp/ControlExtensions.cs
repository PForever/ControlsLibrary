using System;
using System.Collections.Generic;
using System.Reflection;
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
    }
}
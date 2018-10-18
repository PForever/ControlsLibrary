using System;

namespace ControlsLibrary.Factories.Concrete.WinForms.WinHelp
{
    /// <summary>
    /// what a terrible failure. Вообще говоря, этого не должно происходить.
    /// </summary>
    internal class WtfException : Exception
    {
        public WtfException() : base()
        {
        }
        public WtfException(string message) : base(message)
        {
        }
        public WtfException(object obj) : base(obj.ToString())
        {
        }
    }
}
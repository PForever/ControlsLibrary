using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using ControlsLibrary.AbstractControllers.TabView.Tab;

namespace ControlsLibrary.AbstractControllers.TabView.Logic
{
    internal class BufferedPage
    {
        private const int TimeOut = 20_000;
        private IDictionary<ITabContent, Task> _timePages;
        public ICollection<ITabContent> Pages => _timePages.Keys;

        public BufferedPage()
        {
            _timePages = new Dictionary<ITabContent, Task>();
        }

        public void Add(ITabContent item)
        {
            Task timeOut = new Task( () =>
            {
                Task.Delay(TimeOut).Wait();
                //item.Dispose();
            });
            //timeOut.ConfigureAwait()
            _timePages.Add(item, timeOut);
        }

        public void Start(ITabContent item)
        {

        }
        public void Stop(ITabContent item)
        {
            //_timePages[item]
        }

        public void Restart(ITabContent item)
        {

        }

        public bool Remove(ITabContent item)
        {
            if (!Pages.Contains(item)) return false;
            _timePages.Remove(item);
            return true;
        }
    }
}
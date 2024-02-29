using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applications.Interfaces
{
    public interface IMessagePublisher
    {
        void PublishMessage(string eventName, string message);
    }
}

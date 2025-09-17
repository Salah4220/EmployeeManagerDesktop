using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Shared.Interfaces
{
    public interface IMessageService
    {
        void ShowMessage(string message, string title = "Info");
    }
}

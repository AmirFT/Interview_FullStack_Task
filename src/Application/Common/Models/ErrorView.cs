using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Application.Common.Models
{
    public class ErrorView
    {
        public ErrorView(string message = null)
        {
            Message = new List<string>();
            if (message != null)
            {
                Message.Add(message);
            }
        }
        public List<string> Message { get; set; }
        public object ModelStateError { get; set; }
    }
}

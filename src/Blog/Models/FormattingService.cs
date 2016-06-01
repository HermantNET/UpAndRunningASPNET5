using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class FormattingService
    {

        public string AsReadableDate(DateTime dateTime)
        {
            return dateTime.ToString("d");
        }
    }
}

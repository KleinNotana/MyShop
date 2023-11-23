using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public partial class Order1
    {
        public string _customerName;


        public string CustomerName
        {
            get
            {
               
                return _customerName;
            }
            
            
        }   
    }
}

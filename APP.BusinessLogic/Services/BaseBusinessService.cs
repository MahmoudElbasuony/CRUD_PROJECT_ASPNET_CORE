using APP.BusinessLogic.Interfaces;
using APP.Entities;
using APP.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP.BusinessLogic.Services
{
    public abstract class BaseBusinessService<T> : IBussinessService<T> 
    {
        public BaseBusinessService()
        {
        }
    }
}

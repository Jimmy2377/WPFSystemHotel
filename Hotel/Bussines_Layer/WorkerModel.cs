using Hotel.Data_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Bussines_Layer
{
    class WorkerModel
    {
        UserPerson userPerson = new UserPerson();
        public bool LoginUser(string user, string pass)
        {
            return userPerson.Login(user, pass);
        }
    }
}
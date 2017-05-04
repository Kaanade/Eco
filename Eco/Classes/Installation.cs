using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Classes
{
    public class Installation
    {
        private int _id;
        private string _nomInstall;

        public Installation(int id, string nomInstall)
        {
            _id = id;
            _nomInstall = nomInstall;
        }
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string NomInstall
        {
            get { return _nomInstall; }
            set { _nomInstall = value; }
        }
    }
}

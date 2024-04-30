using TestCabinetRepository.models;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCabinetRepository.MySql
{
    internal interface ICRUD
    {
        public bool Create(Object obj);
        public bool Update(Object obj);
        public bool Delete(int id);
        public bool Delete(string cin);
        public Object? Retrieve(int id);
        public Object? Retrieve(string cin);
    }
}

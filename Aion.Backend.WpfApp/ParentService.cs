using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aion.Backend.WpfApp
{
    public class ParentService
    {
        private readonly string myConnectionString;
        public ParentService(string connectionString)
        {
            myConnectionString = connectionString;
        }

        public ParentInfoViewModel GetParentInfoByUserId(int id)
        {
            var users = SQLiteMananergment.GetAllData(new User());
            var parents = SQLiteMananergment.GetAllData(new Parent());
            var datas = users.Join(parents, u => u.ParentId, p => p.Id, (u, p) => new { u.Relation, p.Name, p.Phone, p.Email, u.Id }).FirstOrDefault(x => x.Id == id);
            var parentInfo = new ParentInfoViewModel()
            {
                Name = datas.Name,
                Phone = datas.Phone,
                Email = datas.Email,
                Relation = datas.Relation,
            };
            return parentInfo;
        }
    }
}

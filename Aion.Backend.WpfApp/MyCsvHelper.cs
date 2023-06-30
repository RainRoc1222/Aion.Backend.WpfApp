using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Aion.Backend.WpfApp
{
    public class MyCsvHelper
    {
        public void GetCsv(string filePath)
        {
            try
            {
                SQLiteMananergment.DeleteAllData(new User());
                SQLiteMananergment.DeleteAllData(new Parent());

                using (var stream = new StreamReader(filePath))
                {
                    while (stream.Peek() != -1)
                    {
                        var line = stream.ReadLine();
                        if (!line.StartsWith("提交時間") && !string.IsNullOrWhiteSpace(line))
                        {
                            var arr = line.Split(',');

                            var parent = CreateParent(arr);
                            SQLiteMananergment.ParentService.Create(parent);

                            var user = CreateUser(arr);
                            var parentId = SQLiteMananergment.GetAllData(new Parent()).FirstOrDefault(x => x.Name == parent.Name).Id;
                            user.ParentId = parentId;
                            SQLiteMananergment.UserService.Create(user);
                        }
                    }
          
                }
            }
            catch (Exception ex)
            {
               LogMgt.Logger.Info(ex.ToString());
            }
        }

        private User CreateUser(string[] array)
        {
            var firstName = array[1];
            var lastName = array[2];
            var birthday = Convert.ToDateTime(array[3]).ToString("yyyy/MM/dd");
            var age = Convert.ToInt32(array[4]);
            var relation = array[8];
            var address = array[9];
            var style = array[10];

            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Birthday = birthday,
                Age = age,
                Address = address,
                Style = style,
                Relation = relation,
            };
            return user;
        }

        private Parent CreateParent(string[] array)
        {

            var parentName = array[5];
            var phone = array[6];
            var email = array[7];

            var parent = new Parent()
            {
                Name = parentName,
                Phone = phone,
                Email = email
            };

            return parent;
        }

    }
}

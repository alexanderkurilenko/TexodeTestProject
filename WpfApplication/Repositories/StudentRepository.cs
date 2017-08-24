using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using WpfApplication.Models;

namespace DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private List<StudentModel> studentStorage;

        public StudentRepository()
        {
            if (!ReferenceEquals(this.GetAll(), null))
            {
                studentStorage = new List<StudentModel>(this.GetAll());
            }
            else
            {
                studentStorage = new List<StudentModel>();
            }


        }

        public void Add(StudentModel item)
        {
            item.Id = studentStorage.Count + 1;
            studentStorage.Add(item);
            XmlSerializer xml = new XmlSerializer(typeof(List<StudentModel>));
            using (FileStream fs = new FileStream("students.xml", FileMode.Open))
            {
                xml.Serialize(fs, studentStorage);
            }
        }

        public IEnumerable<StudentModel> Find(Predicate<StudentModel> predicate)
        {
            return studentStorage.FindAll(predicate);
        }

        public StudentModel Get(int id)
        {
            return studentStorage.Find(studentModel => studentModel.Id == id);
        }

        public IEnumerable<StudentModel> GetAll()
        {
            List<StudentModel> buffer;
            XmlSerializer xml = new XmlSerializer(typeof(List<StudentModel>));
            using (FileStream fs = new FileStream("students.xml", FileMode.OpenOrCreate))
            {
                if (fs.Length > 0)
                {
                    buffer = (List<StudentModel>)xml.Deserialize(fs);
                }
                else
                {
                    buffer = null;
                }
            }
            return buffer;
        }

        public void Remove(int id)
        {
            var stud = Get(id);
            XmlSerializer xml = new XmlSerializer(typeof(List<StudentModel>));
            if (!ReferenceEquals(stud,null))
            {
                studentStorage.Remove(stud);
            }
            using (FileStream fs = new FileStream("students.xml", FileMode.Create))
            {
                xml.Serialize(fs, studentStorage);
            }
        }

        public void Remove(StudentModel item)
        {
            Remove(item.Id);
        }

        public void Update(StudentModel item)
        {
            //var stud = Get(item.ID);
            var stud = studentStorage.First(studnt => studnt.Id == item.Id);
            stud.Age = item.Age;
            stud.Gender = item.Gender;
            stud.Name = item.Name;
            stud.SurName = item.SurName;
            XmlSerializer xml = new XmlSerializer(typeof(List<StudentModel>));
            using (FileStream fs = new FileStream("students.xml", FileMode.Create))
            {
                xml.Serialize(fs, studentStorage);
            }
        }
    }
}

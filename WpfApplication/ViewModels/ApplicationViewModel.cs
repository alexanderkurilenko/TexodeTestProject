using AutoMapper;

using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApplication.Commands;
using WpfApplication.Models;

namespace WpfApplication.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private StudentModel selectedStudent;
        private readonly IStudentRepository service;

        public ObservableCollection<StudentModel> Students { get; set; }

        public ApplicationViewModel()
        {
            this.service = new StudentRepository();
            Students=new ObservableCollection<StudentModel>(service.GetAll());
        
        }

        public StudentModel SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                
              OnPropertyChanged("SelectedStudent");
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      StudentModel stud = new StudentModel();
                      Students.Insert(Students.Count,stud);
                      service.Add(stud);
                      SelectedStudent = stud;
                  }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        StudentModel stud = obj as StudentModel;
                        if (stud != null)
                        {
                            Students.Remove(stud);
                            service.Remove(stud);
                        }
                    },
                    (obj) => Students.Count > 0));
            }
        }
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (!ReferenceEquals(PropertyChanged, null))
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

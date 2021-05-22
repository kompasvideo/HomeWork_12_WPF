using HomeWork_WPF.Employees;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_WPF
{ 
    public class Repository
    {
        // Список отделов
        public static ObservableCollection<Department> Departments { get; set; }
        // Список сотрудников
        public ObservableCollection<Employee> Employees { get; set; }
        // Список сотрудников для считывания из Json
        private List<Worker> _listEmployees;
        private List<Employee> _listSortEmployees;
        private static int _iterator;

        public Repository()
        {
            #region hide
            //Departments = new ObservableCollection<Department>
            //{
            //    new Department("ОАО 'Лучшие кодеры'", 0,
            //        new ObservableCollection<Department>
            //        {
            //            new Department("Департамент_1", 1, new ObservableCollection<Department>
            //            {
            //                new Department("Департамент_11", 11, new ObservableCollection<Department>()),
            //                new Department("Департамент_12", 12, new ObservableCollection<Department>())
            //            }),
            //            new Department("Департамент_2", 2, new ObservableCollection<Department>
            //            {
            //                new Department("Департамент_21", 21, new ObservableCollection<Department>()),
            //                new Department("Департамент_22", 22, new ObservableCollection<Department>()),
            //                new Department("Департамент_23", 23, new ObservableCollection<Department>()),
            //                new Department("Департамент_24", 24, new ObservableCollection<Department>())
            //            }),
            //            new Department("Департамент_3", 3, new ObservableCollection<Department>
            //            {
            //                new Department("Департамент_31", 31, new ObservableCollection<Department>()),
            //            }),
            //            new Department("Департамент_4", 4, new ObservableCollection<Department>
            //            {
            //                new Department("Департамент_41", 41, new ObservableCollection<Department>()),
            //                new Department("Департамент_42", 42, new ObservableCollection<Department>()),
            //                new Department("Департамент_43", 43, new ObservableCollection<Department>()),
            //                new Department("Департамент_44", 44, new ObservableCollection<Department>()),
            //                new Department("Департамент_45", 45, new ObservableCollection<Department>()),
            //                new Department("Департамент_46", 46, new ObservableCollection<Department>()),
            //                new Department("Департамент_47", 47, new ObservableCollection<Department>()),
            //                new Department("Департамент_48", 48, new ObservableCollection<Department>())
            //            }),
            //            new Department("Департамент_5", 5, new ObservableCollection<Department>
            //            {
            //                new Department("Департамент_51", 51, new ObservableCollection<Department>
            //                {
            //                    new Department("Департамент_511", 511, new ObservableCollection<Department>
            //                    {
            //                        new Department("Департамент_5111", 5111, new ObservableCollection<Department>
            //                        {
            //                            new Department("Департамент_51111", 51111, new ObservableCollection<Department>()),
            //                            new Department("Департамент_51112", 51112, new ObservableCollection<Department>()),
            //                            new Department("Департамент_51113", 51113, new ObservableCollection<Department>()),
            //                            new Department("Департамент_51114", 51114, new ObservableCollection<Department>()),
            //                        }),
            //                        new Department("Департамент_5112", 5112, new ObservableCollection<Department>()),
            //                        new Department("Департамент_5113", 5113, new ObservableCollection<Department>()),
            //                    }),
            //                    new Department("Департамент_512", 512, new ObservableCollection<Department>()),
            //                }),
            //                new Department("Департамент_52", 52, new ObservableCollection<Department>
            //                {
            //                    new Department("Департамент_521", 521, new ObservableCollection<Department>()),
            //                    new Department("Департамент_522", 522, new ObservableCollection<Department>()),
            //                    new Department("Департамент_523", 523, new ObservableCollection<Department>())
            //                }),
            //                new Department("Департамент_53", 53, new ObservableCollection<Department>
            //                {
            //                    new Department("Департамент_531", 531, new ObservableCollection<Department>())
            //                }),
            //                new Department("Департамент_54", 54, new ObservableCollection<Department>()),
            //            })
            //        }
            //    ),
            //};
            //Employees = new ObservableCollection<Employee>
            //{
            //    new Manager("Имя_1","Фамилия_1", 40,0),
            //    new Manager("Имя_2","Фамилия_2", 39,1),
            //    new Manager("Имя_3","Фамилия_3", 38,11),
            //    new Manager("Имя_4","Фамилия_4", 37,12),
            //    new Manager("Имя_5","Фамилия_5", 36,2),
            //    new Manager("Имя_6","Фамилия_6", 35,21),
            //    new Manager("Имя_7","Фамилия_7", 36,22),
            //    new Manager("Имя_8","Фамилия_8", 37,23),
            //    new Manager("Имя_9","Фамилия_9", 38,24),
            //    new Manager("Имя_10","Фамилия_10", 39,3),
            //    new Manager("Имя_11","Фамилия_11", 40,31),
            //    new Manager("Имя_12","Фамилия_12", 39,4),
            //    new Manager("Имя_13","Фамилия_13", 40,41),
            //    new Manager("Имя_14","Фамилия_14", 40,42),
            //    new Manager("Имя_15","Фамилия_15", 40,43),
            //    new Manager("Имя_16","Фамилия_16", 40,44),
            //    new Manager("Имя_17","Фамилия_17", 40,45),
            //    new Manager("Имя_18","Фамилия_18", 40,46),
            //    new Manager("Имя_19","Фамилия_19", 40,47),
            //    new Manager("Имя_20","Фамилия_20", 40,48),
            //    new Manager("Имя_21","Фамилия_21", 40,5),
            //    new Manager("Имя_22","Фамилия_22", 40,51),
            //    new Manager("Имя_23","Фамилия_23", 40,511),
            //    new Manager("Имя_24","Фамилия_24", 40,5111),
            //    new Manager("Имя_25","Фамилия_25", 40,51111),
            //    new Manager("Имя_26","Фамилия_26", 40,51112),
            //    new Manager("Имя_27","Фамилия_27", 40,51113),
            //    new Manager("Имя_28","Фамилия_28", 40,51114),
            //    new Manager("Имя_29","Фамилия_29", 40,5112),
            //    new Manager("Имя_30","Фамилия_30", 40,5113),
            //    new Manager("Имя_31","Фамилия_31", 40,512),
            //    new Manager("Имя_32","Фамилия_32", 40,52),
            //    new Manager("Имя_33","Фамилия_33", 40,521),
            //    new Manager("Имя_34","Фамилия_34", 40,522),
            //    new Manager("Имя_35","Фамилия_35", 40,523),
            //    new Manager("Имя_36","Фамилия_36", 40,53),
            //    new Manager("Имя_37","Фамилия_37", 40,531),
            //    new Manager("Имя_38","Фамилия_38", 40,54),
            //    new Worker("Имя_41","Фамилия_41", 40,0),
            //    new Worker("Имя_42","Фамилия_42", 39,1),
            //    new Worker("Имя_43","Фамилия_43", 38,11),
            //    new Worker("Имя_44","Фамилия_44", 37,12),
            //    new Worker("Имя_45","Фамилия_45", 36,2),
            //    new Worker("Имя_46","Фамилия_46", 35,21),
            //    new Worker("Имя_47","Фамилия_47", 36,22),
            //    new Worker("Имя_48","Фамилия_48", 37,23),
            //    new Worker("Имя_49","Фамилия_49", 38,24),
            //    new Worker("Имя_50","Фамилия_50", 39,3),
            //    new Worker("Имя_51","Фамилия_51", 40,31),
            //    new Worker("Имя_52","Фамилия_52", 39,4),
            //    new Worker("Имя_53","Фамилия_53", 40,41),
            //    new Worker("Имя_54","Фамилия_54", 40,42),
            //    new Worker("Имя_55","Фамилия_55", 40,43),
            //    new Worker("Имя_56","Фамилия_56", 40,44),
            //    new Worker("Имя_57","Фамилия_57", 40,45),
            //    new Worker("Имя_58","Фамилия_58", 40,46),
            //    new Worker("Имя_59","Фамилия_59", 40,47),
            //    new Worker("Имя_60","Фамилия_60", 40,48),
            //    new Worker("Имя_61","Фамилия_61", 40,5),
            //    new Worker("Имя_62","Фамилия_62", 40,51),
            //    new Worker("Имя_63","Фамилия_63", 40,511),
            //    new Worker("Имя_64","Фамилия_64", 40,5111),
            //    new Worker("Имя_65","Фамилия_65", 40,51111),
            //    new Worker("Имя_66","Фамилия_66", 40,51112),
            //    new Worker("Имя_67","Фамилия_67", 40,51113),
            //    new Worker("Имя_68","Фамилия_68", 40,51114),
            //    new Worker("Имя_69","Фамилия_69", 40,5112),
            //    new Worker("Имя_70","Фамилия_70", 40,5113),
            //    new Worker("Имя_71","Фамилия_71", 40,512),
            //    new Worker("Имя_72","Фамилия_72", 40,52),
            //    new Worker("Имя_73","Фамилия_73", 40,521),
            //    new Worker("Имя_74","Фамилия_74", 40,522),
            //    new Worker("Имя_75","Фамилия_75", 40,523),
            //    new Worker("Имя_76","Фамилия_76", 40,53),
            //    new Worker("Имя_77","Фамилия_77", 40,531),
            //    new Worker("Имя_78","Фамилия_78", 40,54),
            //    new Intern("Имя_81","Фамилия_81", 40,0),
            //    new Intern("Имя_82","Фамилия_82", 39,1),
            //    new Intern("Имя_83","Фамилия_83", 38,11),
            //    new Intern("Имя_84","Фамилия_84", 37,12),
            //    new Intern("Имя_85","Фамилия_85", 36,2),
            //    new Intern("Имя_86","Фамилия_86", 35,21),
            //    new Intern("Имя_87","Фамилия_87", 36,22),
            //    new Intern("Имя_88","Фамилия_88", 37,23),
            //    new Intern("Имя_89","Фамилия_89", 38,24),
            //    new Intern("Имя_90","Фамилия_90", 39,3),
            //    new Intern("Имя_91","Фамилия_91", 40,31),
            //    new Intern("Имя_92","Фамилия_92", 39,4),
            //    new Intern("Имя_93","Фамилия_93", 40,41),
            //    new Intern("Имя_94","Фамилия_94", 40,42),
            //    new Intern("Имя_95","Фамилия_95", 40,43),
            //    new Intern("Имя_96","Фамилия_96", 40,44),
            //    new Intern("Имя_97","Фамилия_97", 40,45),
            //    new Intern("Имя_98","Фамилия_98", 40,46),
            //    new Intern("Имя_99","Фамилия_99", 40,47),
            //    new Intern("Имя_110","Фамилия_110", 40,48),
            //    new Intern("Имя_111","Фамилия_111", 40,5),
            //    new Intern("Имя_112","Фамилия_112", 40,51),
            //    new Intern("Имя_113","Фамилия_113", 40,511),
            //    new Intern("Имя_114","Фамилия_114", 40,5111),
            //    new Intern("Имя_115","Фамилия_115", 40,51111),
            //    new Intern("Имя_116","Фамилия_116", 40,51112),
            //    new Intern("Имя_117","Фамилия_117", 40,51113),
            //    new Intern("Имя_118","Фамилия_118", 40,51114),
            //    new Intern("Имя_119","Фамилия_119", 40,5112),
            //    new Intern("Имя_120","Фамилия_120", 40,5113),
            //    new Intern("Имя_121","Фамилия_121", 40,512),
            //    new Intern("Имя_122","Фамилия_122", 40,52),
            //    new Intern("Имя_123","Фамилия_123", 40,521),
            //    new Intern("Имя_124","Фамилия_124", 40,522),
            //    new Intern("Имя_125","Фамилия_125", 40,523),
            //    new Intern("Имя_126","Фамилия_126", 40,53),
            //    new Intern("Имя_127","Фамилия_127", 40,531),
            //    new Intern("Имя_128","Фамилия_128", 40,54),
            //};
            #endregion
            _iterator = 0;
            //SaveManager(Departments);
            //SaveToJson();
            LoadJson();
        }
        /// <summary>
        /// Считывает департаменты и сотрудников из файлов json
        /// </summary>
        private void LoadJson()
        {
            string json = File.ReadAllText("departments.json");
            Departments =  JsonConvert.DeserializeObject<ObservableCollection<Department>>(json);
            json = File.ReadAllText("employees.json");
            //Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
            Employees = new ObservableCollection<Employee>();
            _listEmployees = JsonConvert.DeserializeObject<List<Worker>>(json);
            ListToObservableCollection();
            SaveManager(Departments);
        }

        /// <summary>
        /// Разбивает сотрудников из json на классы
        /// </summary>
        private void ListToObservableCollection()
        {
            foreach(var emp in _listEmployees)
            {
                switch (emp.EEmployee)
                {
                    case EnEmployee.Intern:
                        Intern intern = new Intern(emp.FirstName, emp.LastName,emp.Age, emp.DepartmentId);
                        Employees.Add(intern);
                        break;
                    case EnEmployee.Worker:
                        Worker worker = new Worker(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(worker);
                        break;
                    case EnEmployee.Manager:
                        Manager manager = new Manager(emp.FirstName, emp.LastName, emp.Age, emp.DepartmentId);
                        Employees.Add(manager);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SaveToJson()
        {
            string json = JsonConvert.SerializeObject(Departments);
            File.WriteAllText("departments.json", json);
            json = JsonConvert.SerializeObject(Employees);
            File.WriteAllText("employees.json", json);
        }

        /// <summary>
        /// Назначает департаменту руководителя 
        /// </summary>
        /// <param name="p_departments"></param>
        void SaveManager(ObservableCollection<Department> p_departments)
        {
            foreach (var dep in p_departments)
            {
                dep.manager = Employees[_iterator++] as Manager;
                if (dep.Departments != null) SaveManager(dep.Departments);
            }
        }

        /// <summary>
        /// Возвращяет имя отдела(департамента)
        /// </summary>
        /// <param name="p_depId">Номер отдела(департамента)</param>
        /// <returns></returns>
        public static string GetNameDepartment(uint p_depId)
        {
            foreach (var dep in Departments)
            {
                if (dep.DepartmentId == p_depId)
                    return dep.Name;
                else
                    return SearchDepartment(p_depId, dep.Departments);
            }
            return "";
        }
        /// <summary>
        /// Ищет дочерние департаменты 
        /// </summary>
        /// <param name="p_depId"></param>
        /// <param name="departments"></param>
        private static string SearchDepartment(uint p_depId, ObservableCollection<Department> departments)
        {
            foreach (var dep in departments)
            {
                if (dep.DepartmentId == p_depId)
                {
                    return dep.Name;
                }
                else
                {
                    if (dep.Departments.Count > 0)
                    {
                        string str = SearchDepartment(p_depId, dep.Departments);
                        if (str!= null) return str;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Сортировка сотрудников
        /// </summary>
        /// <param name="name"></param>
        public void Sort(string name)
        {
            _listSortEmployees = Employees.ToList();
            switch (name)
            {
                case "LastName":
                    _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.LastName));
                    break;
                case "FirstName":
                    _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.FirstName));
                    break;
                case "Employee":
                    _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Employee)); 
                    break;
                case "Age":
                    _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Age));
                    break;
                case "Department":
                    _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Department));
                    break;
                case "Salary":
                    _listSortEmployees.Sort(Employee.SortedBy(SortedCriterion.Salary));
                    break;
            }
            Employees.Clear();
            Employees = new ObservableCollection<Employee>(_listSortEmployees);
        }
    }
}

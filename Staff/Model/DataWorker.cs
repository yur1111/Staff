using Staff.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace Staff.Model
{
    public static class DataWorker
    {
        #region GET ALL
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }
        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();
                return result;
            }
        }
        #endregion
        #region CREATE
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Departments.Any(el => el.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPosition = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        public static string CreateUser(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Users.Any(el => el.Name == name && el.SurName == surName && el.Position == position);
                if (!checkIsExist)
                {
                    if (position.MaxNumber > position.PositionUsers.Count)
                    {
                        User newUser = new User
                        {
                            Name = name,
                            SurName = surName,
                            Phone = phone,
                            PositionId = position.Id
                        };
                        db.Users.Add(newUser);
                        db.SaveChanges();
                        result = "Сделано!";
                    }
                    else
                    {
                        result = "Достигнуто максимальное число сотруднников!";
                    }
                }
                return result;
            }
        }
        #endregion
        #region DELETE
        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Сделано! Отдел " + department.Name + " удален";
            }
            return result;
        }
        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + " удалена";
            }
            return result;
        }
        public static string DeleteUser(User user)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Сделано! Сотрудник " + user.Name + " уволен";
            }
            return result;
        }
        #endregion
        #region EDIT
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Такого отела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = "Сделано! Отдел " + department.Name + " изменен";
            }
            return result;
        }
        public static string EditPosition(Position oldPosition, string newName, int newMaxNumber, decimal newSalary, Department newDepartment)
        {
            string result = "Такой позиции не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.DepartmentId = newDepartment.Id;
                db.SaveChanges();
                result = "Сделано! Позиция " + position.Name + " изменена";
            }
            return result;
        }
        public static string EditUser(User oldUser, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого сотрудника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(p => p.Id == oldUser.Id);
                if (user != null)
                {
                    user.Name = newName;
                    user.SurName = newSurName;
                    user.Phone = newPhone;
                    user.PositionId = newPosition.Id;
                    db.SaveChanges();
                    result = "Сделано! Сотрудник " + user.Name + " изменен";
                }
            }
            return result;
        }
        #endregion
        //получение позиции по id позитиции
        public static Position GetPositionById(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        //получение отдела по id отдела
        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        //получение всех пользователей по id позиции
        public static List<User> GetAllUsersByPositionId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = (from user in GetAllUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }
        //получение всех позиций по id отдела
        public static List<Position> GetAllPositionsByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return positions;
            }
        }
    }
}

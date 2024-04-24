using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CMS23MCA.Models
{
    public class EmployeeModel
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StudentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        [Required(ErrorMessage = "PLease Enter Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "PLease Enter Name")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Please Enter Department Name")]
        public String Department { get; set; }
        [Required(ErrorMessage = "Please Enter Salary")]
        [Range(5000, 50000, ErrorMessage = "Enter Salary Between 5K To 50K")]
        public int Salary { get; set; }
        //Retrieve all records from a table
        public List<EmployeeModel> getData()
        {
            List<EmployeeModel> lstEmp = new List<EmployeeModel>();
            SqlDataAdapter apt = new SqlDataAdapter("select * from emp", con);
            DataSet ds = new DataSet();
            apt.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstEmp.Add(new EmployeeModel
                {
                    Id = Convert.ToInt32(dr["Id"].ToString()),
                    Name = dr["name"].ToString(),
                    Department = dr["dept"].ToString(),
                    Salary = Convert.ToInt32(dr["salary"].ToString())
                });
            }
            return lstEmp;
        }
        //Retrieve single record from a table
        public EmployeeModel getData(string Id)
        {
            EmployeeModel emp = new EmployeeModel();
            SqlCommand cmd = new SqlCommand("select * from emp where id='" + Id + "'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.Id = Convert.ToInt32(dr["Id"].ToString());
                    emp.Name = dr["name"].ToString();
                    emp.Department = dr["dept"].ToString();
                    emp.Salary = Convert.ToInt32(dr["salary"].ToString());
                }
            }
            con.Close();
            return emp;
        }

        //Select & Login a record into a database table
        public bool login(EmployeeModel Emp)
        {
            bool res = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from emp where Id = @Id And Name = @Name", con);
            cmd.Parameters.AddWithValue("@Id", Emp.Id);
            cmd.Parameters.AddWithValue("@Name", Emp.Name);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                res = true;
            }
            con.Close();
            return res;
        }
        //Insert a record into a database table
        public bool insert(EmployeeModel emp)
        {
            SqlCommand cmd = new SqlCommand("insert into emp(Name,Dept,Salary) values (@Name, @Dept, @Salary)", con);

            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Dept", emp.Department);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }


        //Delete a record into a database table
        public bool delete(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("delete emp where Id = @Id", con);
            cmd.Parameters.AddWithValue("@Id", Emp.Id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }


        //Update a record into a database table
        public bool update(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("update emp set Name=@name, Dept = @dept, Salary = @salary where ID = @id", con);

            cmd.Parameters.AddWithValue("@name", Emp.Name);
            cmd.Parameters.AddWithValue("@dept", Emp.Department);
            cmd.Parameters.AddWithValue("@salary", Emp.Salary);
            cmd.Parameters.AddWithValue("@id", Emp.Id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

    }
}

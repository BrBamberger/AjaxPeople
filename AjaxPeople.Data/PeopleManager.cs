using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AjaxPeople.Data
{
    public class PeopleManager
    {
        private string _connectionString;
        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAllPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Person";
            connection.Open();
            List<Person> people = new();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }

        public void AddPerson (Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Person (FirstName, LastName, Age) " +
                "VALUES (@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            connection.Open();
            person.Id = (int)(decimal)cmd.ExecuteScalar();
        }

        //public void AddPerson(Person person)
        //{
        //    using var conn = new SqlConnection(_connectionString);
        //    using var cmd = conn.CreateCommand();
        //    cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
        //        "VALUES(@f, @l, @a) SELECT SCOPE_IDENTITY()";
        //    cmd.Parameters.AddWithValue("@f", person.FirstName);
        //    cmd.Parameters.AddWithValue("@l", person.LastName);
        //    cmd.Parameters.AddWithValue("@a", person.Age);
        //    conn.Open();
        //    person.Id = (int)(decimal)cmd.ExecuteScalar();
        //}

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Person WHERE Id = @id";          
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery(); 
            connection.Close();
            
        }

        public void Edit (Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE PERSON SET FirstName = @firstName, LastName =@lastName, Age = @age " +
                "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}

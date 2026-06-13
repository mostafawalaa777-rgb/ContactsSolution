using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName, ref string Email, ref string phone,
            ref string Address, ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * from contacts where ContactID = @ContactID";

            SqlCommand Command = new SqlCommand(query, connection);


            Command.Parameters.AddWithValue("ContactID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    if (reader["ImagePath"] != DBNull.Value)

                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                }
                else
                {
                    IsFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static int AddNewContact(string FirstName, string LastName, string Email, string phone,
                 string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"INSERT INTO Contacts
            (FirstName
           , LastName
           , Email
           , Phone
           , Address
           , DateOfBirth
           , CountryID
           , ImagePath)
     VALUES
           (@FirstName
           ,@LastName
           ,@Email
           ,@Phone
           ,@Address 
           ,@DateOfBirth
           ,@CountryID 
           ,@ImagePath )

           select SCOPE_IDENTITY() ;";


            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue(@"FirstName", FirstName);
            command.Parameters.AddWithValue(@"LastName", LastName);
            command.Parameters.AddWithValue(@"Email", Email);
            command.Parameters.AddWithValue(@"phone", phone);
            command.Parameters.AddWithValue(@"Address", Address);
            command.Parameters.AddWithValue(@"DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue(@"CountryID", CountryID);
            
            if (ImagePath != "")
            {
            command.Parameters.AddWithValue(@"ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue(@"ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                {
                    ContactID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return ContactID;
             
        }

        public static bool UpdateContact(int ID, string FirstName, string LastName,
           string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Contacts  
                            set FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Phone = @Phone, 
                                Address = @Address, 
                                DateOfBirth = @DateOfBirth,
                                CountryID = @CountryID,
                                ImagePath =@ImagePath
                                where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ContactID", ID);
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Phone", Phone);
                command.Parameters.AddWithValue("@Address", Address);
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                command.Parameters.AddWithValue("@CountryID", CountryID);

                if (ImagePath != "")
                {
                    command.Parameters.AddWithValue(@"ImagePath", ImagePath);
                }
                else
                {
                    command.Parameters.AddWithValue(@"ImagePath", System.DBNull.Value);
                }

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteContact(int ContactID)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"Delete Contacts" +
                " where contactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
             
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }

        public static DataTable GetAllContact()
        {
            DataTable DT = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = "Select * from Contacts ";


            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DT.Load(reader);
                }
                reader.Close();
            }
            catch 
            {

            }
            finally
            {
                connection.Close();
            }
            return DT;
        }

        public static bool IsContactExist(int ContactID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = "Select found = 1 from contacts where contactID = @ContactID";

            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                Connection.Open();
                object Result = Command.ExecuteScalar();

                if (Result != null)
                {
                    IsFound = true;
                }
            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }

    }










    }







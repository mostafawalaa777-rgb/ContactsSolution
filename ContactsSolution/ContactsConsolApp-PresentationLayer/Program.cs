using System;
using System.Data;
using System.Diagnostics.Contracts;
using ContactsBusinessLayer;

namespace ContactsConsolApp
{
    internal class Program
    {

        static void TestFindContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            if (Contact1 != null)
            {
                Console.WriteLine(($"Name :{Contact1.FirstName} {Contact1.LastName} "));
                Console.WriteLine($"Email {Contact1.Email} ");
                Console.WriteLine($"Phone {Contact1.Phone} ");
                Console.WriteLine($"Address {Contact1.Address}");
                Console.WriteLine($"DateOfBirth {Contact1.DateOfBirth} ");
                Console.WriteLine($"ImagePath {Contact1.ImagePath} ");
                Console.WriteLine($"CountryID {Contact1.CountryID} ");
            }
            else
            {
                Console.WriteLine("Contact[" + ID + "] not found!");
            }
        }
        
        static void TestAddNewContact()
        {
              clsContact  Contact1 = new clsContact();

            Contact1.FirstName = "Taim";
            Contact1.LastName = "Ayman";
            Contact1.Email = "Taim@a.com";
            Contact1.Phone = "0125482";
            Contact1.Address = "address5";
            Contact1.DateOfBirth = new DateTime(2023, 4, 28 , 10, 30, 0);
            Contact1.CountryID = 3;
            Contact1.ImagePath = "";

            if(Contact1.Save())
            {
                Console.WriteLine("Contact Added Successfully with id=" + Contact1.ID);
            }
        }

        static void TestUpdateContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);
            if (Contact1 != null)
            {
                Contact1.FirstName = "Walaa";
                Contact1.LastName = "Mostafa";
                Contact1.Email = "Walaa@w.com";
                Contact1.Phone = "169215631";
                Contact1.Address = "address7";
                Contact1.DateOfBirth = new DateTime(1995, 4, 25, 10, 30, 0);
                Contact1.CountryID = 3;
                Contact1.ImagePath = "";

                if (Contact1.Save())
                {
                    Console.WriteLine("Contact Updated Successfully ");
                }
            }
        }

        static void TestDeleteContact(int ID)
        {

            if (clsContact.IsContactExist(ID))
            {
                if (clsContact.DeleteContact(ID))
                {
                    Console.WriteLine("Contact Deleted Successfully. ");
                }
                else
                {
                    Console.WriteLine("Failed to delete contact .");
                }
            }
            else
            {
                Console.WriteLine("The Contact with " + ID + " is not Found.");
            }
        }

        static void ListContacts()
        {
            DataTable dt = clsContact.GetAllContacts();
            Console.WriteLine("Contacts Data :");

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]}  {row["LastName"]}");
            }
        }

        static void TestIsContactExist (int ID)
        {

            if (clsContact.IsContactExist(ID))
            {
                Console.WriteLine("Yes, Contact is there.");
            }
            else
            {
                Console.WriteLine("No, Contact is not there.");
            }
        }



        //---Test Country Business



        static void TestFindCountry(int ID)
        {
            clsCountry country1 = clsCountry.Find(ID);
            if (country1 != null)
            {
                Console.WriteLine($"Country Name :{country1.CountryName} ");
            }
            else
            {
                Console.WriteLine("Contact[" + ID + "] not found!");

            }
        }
            
        static void TestFindCountry(string CountryName)
        {
            clsCountry country2 = clsCountry.Find(CountryName);
            if (country2 != null)
            {
                Console.WriteLine("Country [" + CountryName + "] isFound with ID = " + country2.ID);

            }
            else
            {
                Console.WriteLine("Country [" + CountryName + "] is not found.");
            }
        }
            
        static void TestIsCountryExist (int ID)
        {
            if (clsCountry.IsCountryIDExist(ID))
            {
                Console.WriteLine("Yes, Country is there.");
                TestFindCountry(ID);
            }
            else
            {
                Console.WriteLine("No, Country is not there.");
            }
        }

        static void TestIsCountryExist(string CountryName )
        {
            if (clsCountry.IsCountryNameExist(CountryName))
            {
                Console.WriteLine("Yes, Contact is there.");
                TestFindCountry(CountryName);
            }
            else
            {
                Console.WriteLine("No, Contact is not there.");
            }
        }

        static void TestAddNewCountry()
        {
            clsCountry country = new clsCountry();
            country.CountryName = "Cairo";
            if(country.Save())
            {
                Console.WriteLine("Country Added Successfully with id =" + country.ID);
            }
        }
        static void TestUpdateCountry(int ID)
        {
            clsCountry country = clsCountry.Find(ID);

            if (country != null)
            {
                country.CountryName = "SoudyArap";
                if (country.Save())
                {
                    Console.WriteLine("Country Updated Successfully ");
                }
            }
            else
            {
                Console.WriteLine("Country is you want to update is Not found!");
            }
        }

        static void TestDeleteCountry(int ID)
        {
            if (clsCountry.IsCountryIDExist(ID))
            {
                if (clsCountry.DeleteCountry(ID))
                {
                    Console.WriteLine("Contact Deleted Successfully. ");
                }
                else
                {
                    Console.WriteLine("Failed to delete contact .");
                }
            }
            else
            {
                Console.WriteLine("The Contact with " + ID + " is not Found.");
            }

        }

        static void ListCountries()
        {
            DataTable DT = clsCountry.GetAllCountries();

            Console.WriteLine("Contacts Data :");

            foreach (DataRow row in DT.Rows)
            {
                Console.WriteLine($"{row["countryID"]},{row["CountryName"]}");
            }
        }

        static void Main(string[] args )
        {
            //TestFindContact(2);
            //TestAddNewContact();
            // TestUpdateContact(4);
            //TestDeleteContact(16);
            // ListContacts();
            // TestIsContactExist(1);


            //TestFindCountry(3);
            TestFindCountry(100);

            //TestFindCountry("United States");
            // TestFindCountry("UK");

            //TestIsCountryExist(1);
            TestIsCountryExist(100);

            //TestIsCountryExist("United States");
            //TestIsCountryExist("UK");

            //TestAddNewCountry();
            //TestUpdateCountry(7);
            // TestDeleteCountry(8);
            //ListCountries();
            Console.ReadKey();  
        }
    }
}

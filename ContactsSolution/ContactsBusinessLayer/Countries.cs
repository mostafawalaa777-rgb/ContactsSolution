using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1 }

        enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;
        }

        private clsCountry(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }


        public static clsCountry Find(int ID)
        {
            string CountryName = ""; 

            if (clsCountriesData.GetCountryInfoByID(ID, ref CountryName))
            {
                return new clsCountry(ID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateCountry();
            }
            return false;
        }
        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            if (clsCountriesData.GetCountryInfoByName(ref ID, CountryName))
            {
                return new clsCountry(ID, CountryName);
            }
            else
            {
                return null;
            }

        }

        public static bool IsCountryIDExist(int ID)
        {
            return (clsCountriesData.IsCountryExist(ID));
        }

        public static bool IsCountryNameExist(string CountryName)
        {
            return (clsCountriesData.IsCountryExist(CountryName));
        }

        private bool _AddNewCountry()
        {
            this.ID = (clsCountriesData.AddNewCountry(this.CountryName));
            return ( this.ID != -1)  ;
        }

        private bool _UpdateCountry()
        {
            return clsCountriesData.UpdateCountry(this.ID, this.CountryName);
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountriesData.DeleteCountry(ID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }
    }
}

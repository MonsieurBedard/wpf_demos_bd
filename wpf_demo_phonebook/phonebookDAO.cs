﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de retourner une table
        /// </summary>
        /// <returns>Une DataTable</returns>
        public DataTable SelectAll()
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] ";

            SqlParameter[] parameters = new SqlParameter[0];

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public void UpdateRow(ContactModel contact)
        {
            string _query =
                $"UPDATE [Contacts] " +
                $"SET FirstName = @_FirstName, LastName = @_LastName, Email = @_Email, Phone = @_Phone, Mobile = @_Mobile " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[6];

            parameters[0] = new SqlParameter("@_FirstName", SqlDbType.NVarChar);
            parameters[0].Value = contact.FirstName;

            parameters[1] = new SqlParameter("@_LastName", SqlDbType.NVarChar);
            parameters[1].Value = contact.LastName;

            parameters[2] = new SqlParameter("@_Email", SqlDbType.NVarChar);
            parameters[2].Value = contact.Email;

            parameters[3] = new SqlParameter("@_Phone", SqlDbType.NVarChar);
            parameters[3].Value = contact.Phone;

            parameters[4] = new SqlParameter("@_Mobile", SqlDbType.NVarChar);
            parameters[4].Value = contact.Mobile;

            parameters[5] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[5].Value = contact.ContactID;

            conn.ExecuteSelectQuery(_query, parameters);
        }

        public void DeleteRow(int _id)
        {
            string _query =
                $"DELETE FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecuteSelectQuery(_query, parameters);
        }

        public void InsertRow(ContactModel contact)
        {
            string _query =
                $"INSERT INTO [Contacts] (FirstName, LastName, Email, Phone, Mobile) " +
                $"VALUES (@_FirstName, @_LastName, @_Email, @_Phone, @_Mobile) ";

            SqlParameter[] parameters = new SqlParameter[5];

            parameters[0] = new SqlParameter("@_FirstName", SqlDbType.NVarChar);
            parameters[0].Value = contact.FirstName;

            parameters[1] = new SqlParameter("@_LastName", SqlDbType.NVarChar);
            parameters[1].Value = contact.LastName;

            parameters[2] = new SqlParameter("@_Email", SqlDbType.NVarChar);
            parameters[2].Value = contact.Email;

            parameters[3] = new SqlParameter("@_Phone", SqlDbType.NVarChar);
            parameters[3].Value = contact.Phone;

            parameters[4] = new SqlParameter("@_Mobile", SqlDbType.NVarChar);
            parameters[4].Value = contact.Mobile;

            conn.ExecuteSelectQuery(_query, parameters);
        }
    }
}

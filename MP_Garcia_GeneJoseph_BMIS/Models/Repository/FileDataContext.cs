﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_Garcia_GeneJoseph_BMIS.Models.Repository
{
    class FileDataContext
    {

        // Constant txt file names
        private const string ACCOUNTS = "accounts.txt";
        private const string RESIDENTS = "residents.txt";
        private const string FAMILIES = "families.txt";
        private const string SUMMON = "summons.txt";
        private const string AUDIT_TRAILS = "audit_trails.txt";

        // -1- Start Account Operations
        /// <summary>
        /// Reads the accounts.txt and adds it to a list object of Account
        /// </summary>
        /// <returns>Returns a list of Resident model that contains the record of the text file. 
        /// If there are no record, an empty List of Accounts object is returned.</returns>
        public List<Account> ReadAccounts()
        {
            List<Account> accounts = new List<Account>();

            // does not need to display anything, the program would only show empty list in views
            if (!File.Exists(ACCOUNTS))
            {   
                // creates the text file
                StreamWriter temp = File.CreateText(ACCOUNTS);
                temp.Close();
                return accounts;
            }

            bool errorEncountered = false;
            string errMessage = "";

            try
            {
                using (StreamReader reader = new StreamReader(ACCOUNTS))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // txt file contains:id, uname, pword, resId, date, acc status
                        string[] data = line.Split(new[] { "%20" }, StringSplitOptions.None);

                        // temp model
                        Account account = new Account();
                        account.AccountId = int.Parse(data[0]);
                        account.Username = data[1];
                        account.Password = data[2];
                        account.ResidentId = int.Parse(data[3]);
                        account.RegisteredDate = Convert.ToDateTime(data[4]);
                        account.AccountStatus = data[5];

                        accounts.Add(account);
                    }
                }
            }
            catch (FormatException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (IndexOutOfRangeException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return accounts;
        }
        /// <summary>
        /// Writes to account.txt using the list of Account models
        /// </summary>
        /// <param name="accounts">Holds the record of all registered accounts. The program could either have added, edited or removed a record.</param>
        /// <returns>A successfull write action returns true, otherwise, false,</returns>
        public bool SaveAccounts(List<Account> accounts)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (accounts == null)
                return false;
            if (accounts.Count() < 1)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(ACCOUNTS);
                string line;

                foreach (var account in accounts)
                {
                    line = account.AccountId + "%20";
                    line += account.Username + "%20";
                    line += account.Password + "%20";
                    line += account.ResidentId + "%20";
                    line += account.RegisteredDate + "%20";
                    line += account.AccountStatus;
                    writer.WriteLine(line);
                }
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        /// <summary>
        /// Saves a single record of a Model, not list. The method only appends to the text file
        /// </summary>
        /// <param name="newAccount">The new account to be recorded or appended to the text file</param>
        /// <returns>True if there are no error encountered, otherwise, False</returns>
        public bool InsertAccount(Account newAccount)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (newAccount == null)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(ACCOUNTS, true);

                string line;
                line = newAccount.AccountId + "%20";
                line += newAccount.Username + "%20";
                line += newAccount.Password + "%20";
                line += newAccount.ResidentId + "%20";
                line += newAccount.RegisteredDate + "%20";
                line += newAccount.AccountStatus;

                writer.WriteLine(line);
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        // End Account Operations

        // -2- Start Resident Operations
        /// <summary>
        /// Reads the resident.txt and adds it to a list object of Resident
        /// </summary>
        /// <returns>Returns a list of Resident model that contains the record of the text file. 
        /// If there are no record, an empty List of Resident object is returned.</returns>
        public List<Resident> ReadResidents()
        {
            List<Resident> residents = new List<Resident>();

            // does not need to display anything, the program would only show empty list in views
            if (!File.Exists(RESIDENTS))
            {
                // creates the text file
                StreamWriter temp = File.CreateText(RESIDENTS);
                temp.Close();
                return residents;
            }

            bool errorEncountered = false;
            string errMessage = "";

            try
            {
                using (StreamReader reader = new StreamReader(RESIDENTS))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // txt file contains:id, fname, mname, lname, sex, bdate, status, address
                        string[] data = line.Split(new[] { "%20" }, StringSplitOptions.None);

                        // temp resident model
                        Resident resident = new Resident();
                        resident.ResidentId = int.Parse(data[0]);
                        resident.FirstName = data[1];
                        resident.MiddleName = data[2];
                        resident.LastName = data[3];
                        resident.Sex = data[4];
                        resident.Birthdate = Convert.ToDateTime(data[5]);
                        resident.Status = data[6];
                        resident.Address = data[7];

                        residents.Add(resident);
                    }
                }
            }
            catch (FormatException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (IndexOutOfRangeException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return residents;
        }        
        /// <summary>
        /// Writes to resident.txt using the list of Resident models
        /// </summary>
        /// <param name="residents">Holds the record of all resident. The program could either have added, edited or removed a record.</param>
        /// <returns>A successfull write action returns true, otherwise, false,</returns>
        public bool SaveResidents(List<Resident> residents)
        {
            // residents may not be null, meaning it is initialized
            // but it does not contain anything.
            if (residents == null)
                return false;
            if (residents.Count() < 1)
                return false;

            bool errorEncountered = false;
            string errMessage = "";

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(RESIDENTS);

                string line;

                foreach (var resident in residents)
                {
                    line = resident.ResidentId + "%20";
                    line += resident.FirstName + "%20";
                    line += resident.MiddleName + "%20";
                    line += resident.LastName + "%20";
                    line += resident.Sex + "%20";
                    line += resident.Birthdate + "%20";
                    line += resident.Status + "%20";
                    line += resident.Address;
                    writer.WriteLine(line);
                }

                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        /// <summary>
        /// Saves a single record of a Model, not list. The method only appends to the text file
        /// </summary>
        /// <param name="newResident">The new resident to be recorded or appended to the text file</param>
        /// <returns>True if there are no error encountered, otherwise, False</returns>
        public bool InsertResident(Resident newResident)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (newResident == null)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(RESIDENTS, true);

                string line;
                line = newResident.ResidentId + "%20";
                line += newResident.FirstName + "%20";
                line += newResident.MiddleName + "%20";
                line += newResident.LastName + "%20";
                line += newResident.Sex + "%20";
                line += newResident.Birthdate + "%20";
                line += newResident.Status + "%20";
                line += newResident.Address;

                writer.WriteLine(line);
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        // End Resident Operations

        // -3- Start Families Operations
        /// <summary>
        /// Reads the families.txt and adds it to a list object of Family
        /// </summary>
        /// <returns>Returns a list of Family model that contains the record of the text file. 
        /// If there are no record, an empty List of Family object is returned.</returns>
        public List<Family> ReadFamilies()
        {
            List<Family> families = new List<Family>();

            // does not need to display anything, the program would only show empty list in views
            if (!File.Exists(FAMILIES))
            {
                // creates the text file
                StreamWriter temp = File.CreateText(FAMILIES);
                temp.Close();
                return families;
            }

            bool errorEncountered = false;
            string errMessage = "";

            try
            {
                using (StreamReader reader = new StreamReader(FAMILIES))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // txt file contains: famId, parentId, parentId, family numbers
                        string[] data = line.Split(new[] { "%20" }, StringSplitOptions.None);

                        // temp model
                        Family family = new Family();
                        family.FamilyId = int.Parse(data[0]);
                        family.ParentOneId = int.Parse(data[1]);
                        family.ParentTwoId = int.Parse(data[2]);
                        family.FamilyMembers = int.Parse(data[3]);

                        families.Add(family);
                    }
                }
            }
            catch (FormatException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (IndexOutOfRangeException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return families;
        }
        /// <summary>
        /// Writes to families.txt using the list of Family models
        /// </summary>
        /// <param name="families">Holds the record of all families. The program could either have added, edited or removed a record.</param>
        /// <returns>A successfull write action returns true, otherwise, false,</returns>
        public bool SaveFamilies(List<Family> families)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (families == null)
                return false;
            if (families.Count() < 1)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(FAMILIES);
                string line;

                foreach (var family in families)
                {
                    line = family.FamilyId + "%20";
                    line += family.ParentOneId + "%20";
                    line += family.ParentTwoId + "%20";// if the program does not set a value to parenttwoid, C# automatically makes it 0
                    line += family.FamilyMembers;
                    writer.WriteLine(line);
                }
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        /// <summary>
        /// Saves a single record of a Model, not list. The method only appends to the text file
        /// </summary>
        /// <param name="newFamily">The new family to be recorded or appended to the text file</param>
        /// <returns>True if there are no error encountered, otherwise, False</returns>
        public bool InsertFamily(Family newFamily)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (newFamily == null)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(FAMILIES, true);

                string line;
                line = newFamily.FamilyId + "%20";
                line += newFamily.ParentOneId + "%20";
                line += newFamily.ParentTwoId + "%20";// if the program does not set a value to parenttwoid, C# automatically makes it 0
                line += newFamily.FamilyMembers;

                writer.WriteLine(line);
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        // End Families Operations

        // -4- Start Summon Operations
        /// <summary>
        /// Reads the summons.txt and adds it to a list object of Summons
        /// </summary>
        /// <returns>Returns a list of Summon model that contains the record of the text file. 
        /// If there are no record, an empty List of Summon object is returned.</returns>
        public List<Summon> ReadSummons()
        {
            List<Summon> summons = new List<Summon>();

            // does not need to display anything, the program would only show empty list in views
            if (!File.Exists(SUMMON))
            {
                // creates the text file
                StreamWriter temp = File.CreateText(SUMMON);
                temp.Close();
                return summons;
            }

            bool errorEncountered = false;
            string errMessage = "";

            try
            {
                using (StreamReader reader = new StreamReader(SUMMON))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // txt file contains: sumId, incidentDate, reportDate, summary, accId
                        string[] data = line.Split(new[] { "%20" }, StringSplitOptions.None);

                        // temp model
                        Summon summon = new Summon();
                        summon.SummonId = int.Parse(data[0]);
                        summon.IncidentDate = Convert.ToDateTime(data[1]);
                        summon.ReportedDate = Convert.ToDateTime(data[2]);
                        summon.Summary = data[3];
                        summon.AccountId = int.Parse(data[4]);

                        summons.Add(summon);
                    }
                }
            }
            catch (FormatException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (IndexOutOfRangeException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return summons;
        }
        /// <summary>
        /// Writes to summons.txt using the list of Summon models
        /// </summary>
        /// <param name="summons">Holds the record of all summons. The program could either have added or removed a record.</param>
        /// <returns>A successfull write action returns true, otherwise, false,</returns>
        public bool SaveSummons(List<Summon> summons)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (summons == null)
                return false;
            if (summons.Count() < 1)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(SUMMON);
                string line;

                foreach (var summon in summons)
                {
                    line = summon.SummonId + "%20";
                    line += summon.IncidentDate + "%20";
                    line += summon.ReportedDate + "%20";
                    line += summon.Summary + "%20";
                    line += summon.AccountId;
                    writer.WriteLine(line);
                }
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        /// <summary>
        /// Saves a single record of a Model, not list. The method only appends to the text file
        /// </summary>
        /// <param name="newSummon">The new summon to be recorded or appended to the text file</param>
        /// <returns>True if there are no error encountered, otherwise, False</returns>
        public bool InsertSummon(Summon newSummon)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (newSummon == null)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(SUMMON, true);
                string line;

                line = newSummon.SummonId + "%20";
                line += newSummon.IncidentDate + "%20";
                line += newSummon.ReportedDate + "%20";
                line += newSummon.Summary + "%20";
                line += newSummon.AccountId;

                writer.WriteLine(line);
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        // End Summon Opeartions

        // -5- Start Audit Trails Operations
        /// <summary>
        /// Reads the audit_trail.txt and adds it to a list object of Audit Trail
        /// </summary>
        /// <returns>Returns a list of AuditTrail model that contains the record of the text file. 
        /// If there are no record, an empty List of AuditTrail object is returned.</returns>
        public List<AuditTrail> ReadAuditTrails()
        {
            List<AuditTrail> auditTrails = new List<AuditTrail>();

            // does not need to display anything, the program would only show empty list in views
            if (!File.Exists(AUDIT_TRAILS))
            {
                // creates the text file
                StreamWriter temp = File.CreateText(AUDIT_TRAILS);
                temp.Close();
                return auditTrails;
            }

            bool errorEncountered = false;
            string errMessage = "";

            try
            {
                using (StreamReader reader = new StreamReader(AUDIT_TRAILS))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // txt file contains: trailId, message, date, accountId
                        string[] data = line.Split(new[] { "%20" }, StringSplitOptions.None);

                        // temp model
                        AuditTrail auditTrail = new AuditTrail();
                        auditTrail.TrailId = int.Parse(data[0]);
                        auditTrail.Message = data[1];
                        auditTrail.ActionDate = Convert.ToDateTime(data[2]);
                        auditTrail.AccountId = int.Parse(data[3]);

                        auditTrails.Add(auditTrail);
                    }
                }
            }
            catch (FormatException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (IndexOutOfRangeException e)
            {
                // for the contents of the text file, the data might be corrupted or invalid formats
                errorEncountered = true;
                errMessage = "Some data were not read succesfully. Report to the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return auditTrails;
        }
        /// <summary>
        /// Writes to audit_trails.txt using the list of Audit Trail models
        /// </summary>
        /// <param name="auditTrails">Holds the record of all AuditTrail. The program could either have added a record.</param>
        /// <returns>A successfull write action returns true, otherwise, false,</returns>
        public bool SaveAuditTrails(List<AuditTrail> auditTrails)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (auditTrails == null)
                return false;
            if (auditTrails.Count() < 1)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(AUDIT_TRAILS);
                string line;

                foreach (var auditTrail in auditTrails)
                {
                    line = auditTrail.TrailId + "%20";
                    line += auditTrail.Message + "%20";
                    line += auditTrail.ActionDate + "%20";
                    line += auditTrail.AccountId;
                    writer.WriteLine(line);
                }
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        /// <summary>
        /// Saves a single record of a Model, not list. The method only appends to the text file
        /// </summary>
        /// <param name="newAuditTrail">The new audit trail to be recorded or appended to the text file</param>
        /// <returns>True if there are no error encountered, otherwise, False</returns>
        public bool InsertAuditTrail(AuditTrail newAuditTrail)
        {
            bool errorEncountered = false;
            string errMessage = "";

            if (newAuditTrail == null)
                return false;

            try
            {
                // writing the model to the text file
                StreamWriter writer = new StreamWriter(AUDIT_TRAILS, true);

                string line;
                line = newAuditTrail.TrailId + "%20";
                line += newAuditTrail.Message + "%20";
                line += newAuditTrail.ActionDate + "%20";
                line += newAuditTrail.AccountId;

                writer.WriteLine(line);
                writer.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                errorEncountered = true;
                errMessage = "Database access not granted. Please contact the IT immediately.";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorEncountered = true;
                errMessage = "Something went wrong. Unable to retrieve records. Please contact the IT immediately.";
            }

            if (errorEncountered)
                MessageBox.Show(errMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return !errorEncountered;
        }
        // End Audit Trails Operations
    }
}

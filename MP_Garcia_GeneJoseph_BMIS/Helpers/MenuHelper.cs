﻿using MP_Garcia_GeneJoseph_BMIS.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_Garcia_GeneJoseph_BMIS.Helpers
{
    class MenuHelper
    {
        /// <summary>
        /// In the GUI, these menu are the side nav bar options
        /// </summary>
        public static void MenuInput()
        {
            string input;
            do
            {
                Console.WriteLine("\n\t\tMENU");
                Console.WriteLine("\tA. Register New Account");
                Console.WriteLine("\tB. Logout");
                Console.Write("\tSelection >>");
                input = Console.ReadLine().Trim();

                string[] options = { "A", "B" };

                if (options.Contains(input))
                    break;
                Console.WriteLine("\tInvalid selection.");

            } while (true);   
            
            if (input == "A")
            {
                new AccountPresenter().GetRegisterAccount();
            }
            else if (input == "B")
            {
                // clear session
                UserSession.LoggedIn = false;
                UserSession.User = null;

                /* Audit TRAIL RECORD and System PROMPT */

                new AccountPresenter().GetLogin();
            }
        }
    }
}

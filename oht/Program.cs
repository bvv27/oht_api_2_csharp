﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace oht
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //0004
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormExamples());
        }
    }
}

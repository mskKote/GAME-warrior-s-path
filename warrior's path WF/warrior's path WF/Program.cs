﻿using System;
using System.Windows.Forms;

namespace warrior_s_path_WF
{
    static class Program
    {
        // / <summary>
        // / Главная точка входа для приложения.
        // / </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();
            Application.Run(form1);
        }
    }
}

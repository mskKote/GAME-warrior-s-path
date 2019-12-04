using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace warrior_s_path_WF
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();

            //Тут задаём все параметры.
            //В Choice просто задаю все условия для вывода, 
            //а затем уже в методе Choice Result выкидываю необходимый номер.


            GameplayText.AcceptsReturn = true;
            GameplayText.AcceptsTab = true;
            GameplayText.Multiline = true;
            GameplayText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            FormUsing.GamePlayText = GameplayText;

            Button[] buttons = new Button[10] { button1, button2, button3, button4, button5,
                                                button6, button7, button8, button9, button10};

            FormUsing.Buttons = buttons;

            foreach (var button in FormUsing.Buttons)
            {
                button.Visible = false;
            }
            
            _ = new Gameplay(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            ChoiceResult(1);
        }
    }

    //При взаимодействии с любым членом теперь можно 
    //Кстати, на кнопки буду скидывать только имена юнитов.
    //Господи, технически это всё можно заменить на this и уже потом сделать всё в Player.
    //Можно использовать делегаты для таких дел.
    //То есть какой принцип? 
    //Есть 3 функции: функция, подготавливающая условия; Функция, принимающая условия и та, 
    //которая должна быть выполнена в соответствии с результатами
    static public class FormUsing
    {
        //static private void Update(FormUsing formUsing = null)
        //{
            
        //}

        private static Button[] buttons;
        public static Button[] Buttons
        {
            get => buttons; set
            {
                buttons = value;
                //Update();
            }
        }

        private static Button[] buttonNext;
        public static Button[] ButtonNext
        {
            get => buttonNext;
            set
            {
                buttonNext = value;
                //Update();
            }
        }
        //public static void SingleButtonChanger()
        //{
        //    Update();
        //}

        private static TextBox textBox;
        public static TextBox GamePlayText
        {
            get => textBox; set { textBox = value; }
        }
        public static void Moonspeak(string message)
        {
            textBox.Text += message;
        }
        //Тут можно сделать Choice.
        public static int Choice(string[] options)
        {
            switch (options.Length)
            {
                case 2:
                    Buttons[7].Visible = true;
                    Buttons[8].Visible = true;

                    break;
                default:
                    break;
            }
            int choiceNum = -1;
            return choiceNum + 1;
        }

        public static int ChoiceResult(int num)
        { 
        
        }

        private static Label labelRound;
        public static Label LabelRound {
            get => labelRound;
            set
            {
                labelRound.Text = value.Text;
                //Update();
            }
        }
    }

}

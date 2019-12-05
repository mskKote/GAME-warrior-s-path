using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static warrior_s_path_WF.Player;

namespace warrior_s_path_WF
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();

            GameplayText.AcceptsReturn = true;
            GameplayText.AcceptsTab = true;
            GameplayText.Multiline = true;
            GameplayText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            FormUsing.GamePlayText = GameplayText;

            FormUsing.LabelRound = this.labelRound;
            FormUsing.ButtonNext = this.buttonNext;
            FormUsing.Buttons = new Button[10] { button1, button2, button3, button4, button5,
                                                 button6, button7, button8, button9, button10};

            string[] buff = new string[2];
            for (int i = 1; i < buff.Length + 1; i++)
            {
                buff[i - 1] = $"Выбор из {i} варианта";
            }
            FormUsing.Choice(buff);
            //gameSchedules.Add(new GameSchedule(buff));
            gameSchedules.Add(new GameSchedule(FormUsing.ChoiceResult.ToString()));

            //Расписываю геймплей в кое-то веке.
            //gameSchedules.Add(new GameSchedule(plot: true));

            //Player user = new Player();
            //Player computer = new Player();
            //computer.MakeRndArmy(true);
            //gameSchedules.Add(new GameSchedule(plot: true));


            //_ = new Gameplay();
        }
        public static List<GameSchedule> gameSchedules = new List<GameSchedule>();
        public static int counter = 0;
        public void buttonNext_Click(object sender, EventArgs e)
        {

            if (gameSchedules[counter].Choice)
            {
                gameSchedules[counter].MakeChoice();
            }
            else if (gameSchedules[counter].moonspeak)
            {
                gameSchedules[counter].MakeString();
            }
            else if (gameSchedules[counter].plot)
            {
                MoonSpeak(plot[CounterPlot % plot.Length]);
                CounterPlot++;
            }
            else if (gameSchedules[counter].location)
                counter++;
            //Я могу делать проверку на то, есть ли в функции Choice или что-то ещё
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 1;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 2;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 3;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 4;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 5;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 6;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 7;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 8;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button9_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 9;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            FormUsing.ChoiceResult = 10;
            FormUsing.BtnOnOff(FormUsing.ButtonNext);
        }
    }

    static public class FormUsing
    {
        public static int[] BtnsHashArr;
       
        public static void BtnOnOff(Button button)
        {
            //Меняем на противоположное.
            if (button != null && button.Enabled == true)
            {
                button.Enabled = false;
                button.Visible = false;
            }
            else if (button != null)
            {
                button.Enabled = true;
                button.Visible = true;
            }
        }
        private static Button[] buttons;
        public static Button[] Buttons
        {
            get => buttons;
            set
            {
                buttons = value;
            }
        }

        private static Button buttonNext;
        public static Button ButtonNext
        {
            get => buttonNext;
            set
            {
                buttonNext = value;
            }
        }

        private static TextBox textBox;
        public static TextBox GamePlayText
        {
            get => textBox; set { textBox = value; }
        }
        public static void Moonspeak(string message, bool bl = true)
        {
            textBox.Text += message + (bl ? "\r\n" : "");
        }

        //Тут можно сделать Choice.
        public static int ChoiceResult = 0;


        public static int Choice(string[] options)
        {
            FormUsing.BtnOnOff(ButtonNext);

            for (int i = 0; i < options.Length; i++)
            {
                ChangeButton(Buttons[i],
                    new Point(197 + 304 * (i % 5), i >= 5 ? 841 : 748),
                    new Size(304, options.Length > 5 ? 90 : 183),
                    options[i]);
                BtnOnOff(Buttons[i]);
            }
            return 0;
        }
        private static void ChangeButton(Button button, Point location, Size size, string message)
        {
            button.Location = location;
            button.Size = size;
            button.Text = message;
        }
        

        private static Label labelRound;
        public static Label LabelRound
        {
            get => labelRound;
            set
            {
                labelRound = value;
            }
        }
        public static void NextRound()
        {
            int buff = int.Parse(LabelRound.Text.Split(' ')[1]);
            LabelRound.Text = "Round " + (++buff).ToString();
        }
    }

    public class GameSchedule
    {
        //При Choice мы делаем выбор, при plot просто выводим сюжет 
        //при кнопке Next, при location показываем локацию.
        public bool plot = false, location = false, Choice = false, moonspeak = false;
        string[] options;
        string message;
        /// <summary>
        /// Отложенный вывод.
        /// </summary>
        /// <param name="options"></param>
        public void MakeChoice()
        {
            FormUsing.Choice(options);
        }
        public void MakeString()
        {
            FormUsing.Moonspeak(message);
        }

        public GameSchedule(bool plot = false, bool location = false)
        {
            this.plot = plot;
            this.location = location;
        }
        public GameSchedule(string[] options, bool Choice = true)
        {
            this.options = options;
            this.Choice = Choice;
        }
        public GameSchedule(string message, bool moonspeak = true)
        {
            this.message = message;
            this.moonspeak = moonspeak;
        }
    }
}
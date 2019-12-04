using System;
using warrior_s_path_WF.Units;
using static warrior_s_path_WF.Player;
using System.Windows.Forms;
using System.Drawing;

namespace warrior_s_path_WF
{
    class Gameplay
    {
        public Gameplay(Form1 form)
        {
            Play(form);
        }

        static int dinamicOut = 15;
        static int plotOut = 30;
        static void Play(Form1 form)
        {
            // Создаём игроков.
            Player user = new Player(form);
            Player computer = new Player(form);
            computer.MakeRndArmy(true);

            //-----------------------------------------------------------------Эпилог.
            //MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
            //// Выводим опций.
            //MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
            //-----------------------------------------------------------------Выбор воиск.
            //if (Choice(new string[] { "Провести кастинг бойцов самостоятельно.", "Воспользоваться местным HR-агенством." }) == 0)
            //{
            //    MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
            //    CounterPlot++;
            //    /*Запускай метод отбора кандидатов. Рандом --> Вывод.*/
            //    user.ArmyCasting();

            //    MoonSpeak("\r\nПостроить отряд?", ticTac: dinamicOut);
            //    if (Choice(new string[] { "Да", "Нет" }) == 0)
            //        user.ShowArmy();

            //}
            //else
            //{
            //    CounterPlot++;
            //    MoonSpeak(plot[CounterPlot++], ticTac: dinamicOut);
            //    user.MakeRndArmy();
            //    // Thread.Sleep(2500);
            //    MoonSpeak("\r\n\tУзрите же свой отряд.\r\n", ticTac: plotOut);
            //    user.ShowArmy();
            //}

            //-----------------------------------------------------------------Путешествие.
            Choice(new string[] { "Подтвердите отправку в путешествие." });
            //MoonSpeak(plot[CounterPlot++], ticTac: dinamicOut);

            // Сюжет крутиться, пока либо вся команда противника не мертва, либо пока
            // Локации сменяются в зависимости от кол - ва убитых противников. 1-> 1-> 2-> 3-> 3;
            //int[] deadsOnLocation = { 1, 1, 2, 3, 3 };
            bool win = true;

            // Определяем, кто первый начнёт драку.
            //Random random = new Random();
            //int turn = random.Next(0, 2);

            //for (int i = 0; i < deadsOnLocation.Length; i++)
            //{
            //    if (i != 0)
            //    {
            //        // Console.Clear();
            //        MoonSpeak("[&]______________________________________________________________[&]\r\n\r\n", ticTac: 0);
            //    }

            //    MoonSpeak(locations_descriptions[CounterLocation++], ticTac: plotOut);
            //    // Изменяю картинку вот здесь.
            //    // for (int j = 1; j <= 4; j++)
            //    // {
            //    //     string path = @"..\..\Pictures\loc" + j + ".jpg";
            //    //     form.BackgroundImage = Image.FromFile(path);
            //    // }

            //    if (i != 0)
            //    {
            //        MoonSpeak("Ваш отряд", ticTac: plotOut);
            //        user.ShowArmy();
            //    }

            //    if (BattleCircle(user, computer, deadsOnLocation[i], ref turn) == 0)
            //    {
            //        // Проигрыш.
            //        win = false;
            //        break;
            //    }
            //}

            //-----------------------------------------------------------------Концовка.
            //MoonSpeak("[&]______________________________________________________________[&]\r\r", ticTac: 5);
            //MoonSpeak(win ? "\r\n\t\t\tВы выиграли.\rОднако, помните тех, кто сражался за вас."
            //               : "\r\n\t\t\tВы проиграли.\r\n\r\n");

            user.ShowArmy();

            MoonSpeak(plot[win ? plot.Length - 1 : plot.Length - 2], ticTac: dinamicOut);

            string[] result = win ? /*Выигрыш*/ new string[]{ "Вспомнить былое и травить байки с приукрасами.\r\nМожет быть, " +
                                                             "предположить, что было бы при обратном исходе.\r\n",
                                                             "Отмазаться, сказав, что сегодня вы идёте посетить\r\nмогилы павших." }
                                   /*Проигрыш*/: new string[] {"Попросить у бога смерти ещё 1 шанс",
                                                             "Уйти в небытие" };


            // Повтор решения - кнопка рестарт в конце.
            // Сверху справа от TextBox будут выводиться раунды.
            //Restart();
        }
    }
}
/*
 * 
 * 2 метода.
 * 1. Задаю условие и активирую кнопки. Количество кнопок. 
 * 
 * 3 кнопки - enable - 9, 8, 7 и задаю им соответствующие размеры.
 * 1 кнопка - 8 и + по размерам.
 * В остальном случае не изменяю.
 * Всегда растягиваю кнопки по 1520px
 * 
 * 2. Проверяю условие.
 * 
 * И в Choice просто объединяю их.
 * 
 * 
 * Для адекватного вывода сделать так, чтобы таймер работал, пока я не нажал на кнопку.
 */

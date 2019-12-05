using System;
using warrior_s_path_WF.Units;
using static warrior_s_path_WF.Player;
using System.Windows.Forms;
using System.Drawing;

namespace warrior_s_path_WF
{
    class Gameplay
    {
        public Gameplay()
        {
            Play();
        }
       
        static void Play()
        {
            // Создаём игроков.
            Player user = new Player();
            Player computer = new Player();
            computer.MakeRndArmy(true);

            //-----------------------------------------------------------------Эпилог.

            //MoonSpeak(plot[CounterPlot+);
            //-----------------------------------------------------------------Выбор воиск.


            Choice(new string[] { "Провести кастинг бойцов самостоятельно.\r\n", "Воспользоваться местным HR-агенством.\r\n" }); 
            if (true)
            {
                MoonSpeak(plot[CounterPlot++]);
                CounterPlot++;
                /*Запускай метод отбора кандидатов. Рандом --> Вывод.*/
                user.ArmyCasting();
                user.ShowArmy();

            }
            else
            {
                CounterPlot++;
                MoonSpeak(plot[CounterPlot++]);
                user.MakeRndArmy();
                // Thread.Sleep(2500);
                MoonSpeak("\r\n\tУзрите же свой отряд.\r\n");
                user.ShowArmy();
            }

            //-----------------------------------------------------------------Путешествие.
            Choice(new string[] { "Подтвердите отправку в путешествие.\r\n" });
            //MoonSpeak(plot[CounterPlot+);

            // Сюжет крутиться, пока либо вся команда противника не мертва, либо пока
            // Локации сменяются в зависимости от кол - ва убитых противников. 1-> 1-> 2-> 3-> 3;
            int[] deadsOnLocation = { 1, 1, 2, 3, 3 };
            bool win = true;

            //Определяем, кто первый начнёт драку.
            Random random = new Random();
            int turn = random.Next(0, 2);

            for (int i = 0; i < deadsOnLocation.Length; i++)
            {
                if (i != 0)
                {
                    MoonSpeak("[&]______________________________________________________________[&]\r\n\r\n", ticTac: 0);
                }

                MoonSpeak(locations_descriptions[CounterLocation++]) ;

                if (i != 0)
                {
                    MoonSpeak("Ваш отряд\r\n");
                    user.ShowArmy();
                }

                if (BattleCircle(user, computer, deadsOnLocation[i], ref turn) == 0)
                {
                    // Проигрыш.
                    win = false;
                    break;
                }
            }

            //-----------------------------------------------------------------Концовка.
            MoonSpeak("[&]______________________________________________________________[&]\r\r\n", ticTac: 5);
            MoonSpeak(win ? "\r\n\t\t\tВы выиграли.\rОднако, помните тех, кто сражался за вас.\r\n"
                           : "\r\n\t\t\tВы проиграли.\r\n\r\n");

            user.ShowArmy();

            MoonSpeak(plot[win ? plot.Length - 1 : plot.Length - 2]);

            string[] result = win ? /*Выигрыш*/ new string[]{ "Вспомнить былое и травить байки с приукрасами.\r\nМожет быть, " +
                                                              "предположить, что было бы при обратном исходе.\r\n",
                                                              "Отмазаться, сказав, что сегодня вы идёте посетить\r\nмогилы павших.\r\n" }
                                   /*Проигрыш*/: new string[] {"Попросить у бога смерти ещё 1 шанс\r\n\r\n",
                                                             "Уйти в небытие\r\n\r\n" };
        }
    }
}
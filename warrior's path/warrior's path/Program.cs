using System;
using System.Threading;
using warrior_s_path.Units;
using static warrior_s_path.Player;

namespace warrior_s_path
{
    class Program
    {
        static int dinamicOut = 15;
        static int plotOut = 30;
        static void Main()
        {
            Console.Title = "Japanese-style adventure";
            string[] result;

            do
            {
                Restart();
                // Создаём игроков.
                Player user = new Player();
                Player computer = new Player();
                computer.MakeRndArmy(true);

                //-----------------------------------------------------------------Эпилог.
                MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
                // Выводим опций.
                MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
                //-----------------------------------------------------------------Выбор воиск.
                if (Choice(new string[] { "Провести кастинг бойцов самостоятельно.", "Воспользоваться местным HR-агенством." }) == 0)
                {
                    MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
                    CounterPlot++;
                    /*Запускай метод отбора кандидатов. Рандом --> Вывод --> Выбор*/
                    user.ArmyCasting();

                    MoonSpeak("\nПостроить отряд?", ticTac: dinamicOut);
                    if (Choice(new string[] { "Да", "Нет" }) == 0)
                        user.ShowArmy();
                }
                else
                {
                    CounterPlot++;
                    MoonSpeak(plot[CounterPlot++], ticTac: plotOut);
                    user.MakeRndArmy();

                    MoonSpeak("\n\tУзрите же свой отряд.\n", ticTac: plotOut);
                    user.ShowArmy();
                }

                //-----------------------------------------------------------------Путешествие.
                Choice(new string[] { "Подтвердите отправку в путешествие." });
                MoonSpeak(plot[CounterPlot++], ticTac: dinamicOut);

                // Сюжет крутиться, пока либо вся команда противника не мертва, либо пока
                // Локации сменяются в зависимости от кол - ва убитых противников. 1-> 1-> 2-> 3-> 3;
                int[] deadsOnLocation = { 1, 1, 2, 3, 3 };
                bool win = true;

                // Определяем, кто первый начнёт драку.
                Random random = new Random();
                int turn = random.Next(0, 2);

                for (int i = 0; i < deadsOnLocation.Length; i++)
                {
                    //На каждой локации кроме 1-ой выводится армия и описание локации.
                    if (i != 0)
                    {
                        Console.Clear();
                        MoonSpeak("[&]______________________________________________________________[&]\n\n", ticTac: 0);
                        MoonSpeak(locations_descriptions[CounterLocation++], ticTac: plotOut);
                        MoonSpeak("Ваш отряд", ticTac: plotOut);
                        user.ShowArmy();
                    }
                    else MoonSpeak(locations_descriptions[CounterLocation++], ticTac: plotOut);

                    if (BattleCircle(user, computer, deadsOnLocation[i], ref turn) == 0)
                    {
                        // Проигрыш.
                        win = false;
                        break;
                    }
                }

                Console.Clear();
                //-----------------------------------------------------------------Концовка.
                MoonSpeak("[&]______________________________________________________________[&]\n\n", ticTac: 5);
                MoonSpeak(win ? "\n\t\t\tВы выиграли.\nОднако, помните тех, кто сражался за вас."
                               : "\n\t\t\tВы проиграли.\n\n");

                user.ShowArmy();

                MoonSpeak(plot[win ? plot.Length - 1 : plot.Length - 2], ticTac: dinamicOut);

                result = win ? /*Выигрыш*/ new string[]{ "Вспомнить былое и травить байки с приукрасами.\nМожет быть, " +
                                                         "предположить, что было бы при обратном исходе.\n",
                                                         "Отмазаться, сказав, что сегодня вы идёте посетить\nмогилы павших." }
                             /*Проигрыш*/: new string[] {"Попросить у бога смерти ещё 1 шанс",
                                                         "Уйти в небытие" };

            } while (Choice(result) == 0);
        }
    }
}
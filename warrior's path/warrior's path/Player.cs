using System;
using System.Threading;
using warrior_s_path.Units;

namespace warrior_s_path
{
    /// <summary>
    /// Игровой процесс.
    /// </summary>
    class Player
    {
        //----------------------------------Сюжет и описание локаций битвы.
        // Описание локаций.
        public readonly static string[] locations_descriptions = {
                "\n*Узкая дорога между скал*\n\n" +
                "Сквозь пелену падающих листьев,\n" +
                "задыхаясь от ещё не осевшей пыли после землятрясения,\n" +
                "в ущелье проникает солнечный свет.\n" +
                "Часовой вступил на эту узкую дорогу.\n" +
                "Отправьте кого-нибудь разобраться с ним.\n\n",
                // 1 противник.

                "\n*Кузнеца на самом краю деревни*\n\n" +
                "Вы делаете глубокий вдох и ощущаете запах огня и дыма,\n" +
                "сырого каменного пола и полированного металла - запах силы.\n" +
                "Ваши воины воодушевлены,\n" +
                "но и противник не дремлет...\n\n",
                // 1 противник.

                "\n*Улица*\n\n" +
                "Некогда оживлённый центр торговли страдает от тоски\n" +
                "и только хриплый стон ветхих конструкций\n" +
                "и шелест бумажных стен не даёт тишине окончательно взять своё." +
                "\nВас замечают.\n\n",
                // 2 противника.

                "\n*Ворота замка*\n\n" +
                "Величие превратилось в руины. Одному богу известно,\n" +
                "как красные ворота летней резиденции могли уцелеть.\n" +
                "Как будто бы древняя магия не даёт им упать.\n" +
                "Под ними вас ожидают стражи.\n\n",
                // 3 противника.

                "\n*Замок, покои местного господина*\n\n" +
                "Расставленная по фэншую комната на фоне общего хаоса\n" +
                "выглядит до невозможности нелепо. Вы получили нужные документы!\n" +
                "Осталось только уйти.\n" +
                "Шум во дворе наводит волнение.\n" +
                "Вас встречает элитная гвардия,\n" +
                "имеющая своей задачей не допустить мародёров\n" +
                "до ценнейших сведений.\n" +
                "Выход только 1, убежать не получится. Придётся драться\n\n"
                // 3 противника.
        };
        // Сюжетец.
        public readonly static string[] plot = {
                "*В 8 веке нашей эры в Японии после страшного землятрясения*\n\n" +
                "Глава клана Фудзивара узнаёт о том,\n" +
                "что катастрофа потрепала замок другого\n" +
                "влиятельного клана Минамото,\n" +
                "славившегося своими богатствами.\n\n" +
                "Однако эти богатства не трогают душу господина,\n" +
                "поскольку он считает, что это грязные деньги.\n\n" +
                "Если бы было возможно узнать наверняка, тогда клан Минамото\n" +
                "больше не смог бы дальше конкурировать на политической арене Японии,\n" +
                "что позволило бы вашему клану достичь пика своего могущества,\n" +
                "а ваш господин вполне бы смог претендовать на титул сёгуна.\n\n\n" +
                "Господин дал вам 10 Ман поручил Вам собрать отряд\n" +
                "и во чтобы то ни стало добыть документы и любые другие улики,\n" +
                "подтверждающие предположения.\n\n",

                "Вам предстоит выбрать лучших воинов и проникнуть в разрушенный замок.\n",

                "*Ваш господин, придворный бухгалтер и вы собрались для осмотра кандидатов*\n",// Дальше делаю сам кастинг. По 3 случайных бойца. 
                
                "\n*Ястреб унёс депешу в Head Hunter Japan\n" +
                "с щедрой предоплатой в размере 10 тысяч йен*\n" +
                "Спустя 2 дня 10 лучших бойцов сёгуната прибыли к вашим воротам.\n",
                // Показываю всю армию.

                "\tГосподин кивнул вам вслед.\n" +
                "[&]______________________________________________________________[&]\n\n",

                "Путь самурая - это смерть. Безмолвно цветёт сакура.\n\n",

                "Вы получили славу и имение, а ваш господин стал сёгуном.\n\n" +
                "В годовщину победы вас пригласили на званный ужин. Пойти?"
        };
        // Для реплик приказов. Имя + реплика.
        protected readonly string[] order_replicas = {
            ", разберись с ним!",
            ", не дай ему победить.",
            ", я рассчитываю на тебя.",
            ", порви его!",
            ", я выбираю тебя!",
            ", не подведи своего господина!",
            ", аттакуй его.",
            ", вступи в бой!"};

        //----------------------------------Счётчки
        public static int CounterLocation = 0;
        public static int CounterPlot = 0;
        public static int CounterOrders = 0;

        private static int CounterRounds = 1;
        /// <summary>
        /// Главный метод для любого взаимодействия с консолью.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ln"></param>
        /// <param name="ticTac"></param>
        public static void MoonSpeak(string message, bool ln = true, int ticTac = 25)
        {
            try
            {
                foreach (var letter in message)
                {
                    Console.Write(letter);
                    Thread.Sleep(ticTac);
                }
                Console.Write(ln ? "\n" : "");
            }
            catch (InvalidOperationException)
            {
                try
                {
                    Console.WriteLine("Change codification and run programm again");
                    throw;
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Было передано слишком большое/маленькое число.");
                throw;
            }
        }

        private static Random random = new Random();
        /// <summary>
        /// Армия Human, с которой происходят взаимодействия.
        /// </summary>
        private Human[] Army = new Human[10];

        //----------------------------------Вывод персонажей в консоль.
        private void Show3Units(Human unit0, Human unit1 = null, Human unit2 = null)
        {
            if (unit0 == null && unit1 == null && unit2 == null)
            {
                return;
            }

            string visual = "";
            for (int j = 0; j < 8; j++)
            {
                // Вывод 3-х персонажей.
                if (j != 7)
                {
                    visual = string.Concat(" ", unit0 != null ? unit0.Apperance[j] : "",
                                          "\t", unit1 != null ? unit1.Apperance[j] : "",
                                          "\t", unit2 != null ? unit2.Apperance[j] : "");
                    MoonSpeak(visual, ticTac: 0);
                }
                // Вывод имён.
                else
                {
                    string nameArr = "              ";

                    visual = string.Concat(
                         "\r    ", unit0 != null ? unit0.Name + nameArr.Substring(2 + unit0.Name.Length) : "", "  ",
                             "  ", unit1 != null ? unit1.Name + nameArr.Substring(2 + unit1.Name.Length) : "", "  ",
                             "  ", unit2 != null ? unit2.Name + nameArr.Substring(2 + unit2.Name.Length) : "");
                    MoonSpeak(visual, ticTac: 15);
                }
            }
            MoonSpeak("\n");
        }
        public void ShowArmy()
        {
            // Вывод по 3
            for (int i = 0; i < 8; i += 3)
            {
                Show3Units(Army[i + 0], Army[i + 1], Army[i + 2]);
            }
            Show3Units(Army[9]);
        }
        //----------------------------------Создание армии случайно.
        private Human RndUnit(bool isEnemy = false)
        {
            switch (random.NextDouble())
            {
                case double buff when buff < 0.45:
                    return new Fighter(isEnemy: isEnemy);
                case double buff when buff >= 0.45 && buff < 0.75:
                    return new Ninja(isEnemy: isEnemy);
                case double buff when buff >= 0.75:
                    return new Samurai(isEnemy: isEnemy);
                default: throw new Exception();
            }
        }
        public void MakeRndArmy(bool isEnemy = false)
        {
            for (int i = 0; i < 10; i++)
            {
                Army[i] = RndUnit(isEnemy);
            }
        }

        //----------------------------------Cоздание армии по воле игрока.
        protected static double money = 10.0;
        public void ArmyCasting()
        {
            for (int i = 0; i < 10 && money >= 1; i++)
            {
                MoonSpeak($"Количество бойцов: {i}\n", ticTac: 0);

                Human[] buffUnits = { RndUnit(), RndUnit(), RndUnit() };
                Show3Units(buffUnits[0], buffUnits[1], buffUnits[2]);

                MoonSpeak($"Баланс: {money} тысяч иен.", ticTac: 0);
                MoonSpeak("Кандидаты: ", ticTac: 0);

                Army[i] = buffUnits[Choice(new string[] {
                    $"{(buffUnits[0] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[0].Price} тысяч иен.\n",
                    $"{(buffUnits[1] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[1].Price} тысяч иен.\n",
                    $"{(buffUnits[2] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[2].Price} тысяч иен.\n"
                })];

                if (Army[i].Price > money)
                {
                    i--;
                    MoonSpeak("\nВы не можете нанять его.\n" +
                              "У вас нет таких денег, а банки не выдают кредиты на секретные операции.\n" +
                              "Увидев, как неумело их потенциальный командир распоряжается деньгами,\nвоины сбежали в ужасе.", ticTac: 25);
                }
                else money -= Army[i].Price;

                if (money > 0 && money < 1.0)
                {
                    MoonSpeak("\nВы наняли армию и у вас осталось ещё немного денег.\nВаша ЗП увеличилась!", ticTac: 30);
                }
            }
        }
        /// <summary>
        /// Главный метод для выбора чего бы то ни было.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static int Choice(string[] options)
        {
            int choiceNum = -1;
            int shift = 1;

            MoonSpeak("");
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i] == "")
                {
                    shift--;
                }
                else MoonSpeak($"{i + shift}] {options[i]}", ticTac: 0);
            }

            do
            {
                MoonSpeak($"\nВаш выбор [1; {options.Length}] >> ", false, 0);
            } while (!int.TryParse(((char)Console.ReadKey().Key).ToString(), out choiceNum)
            || choiceNum - 1 >= options.Length
            || choiceNum - 1 < 0);

            if (options.Length >= 10 && choiceNum == 1)
            {
                choiceNum = Choice(new string[] {options[0], options[9]}) + 1 == 2 ? 10 : 1;
            }

            Console.WriteLine();
            return choiceNum - 1;
        }

        //----------------------------------Проверяет количество живых и жив ли хоть кто-нибудь.
        public int AliveAmount()
        {
            int amount = 0;
            foreach (var unit in Army)
            {
                if (unit != null && unit.IsAlive())
                {
                    amount++;
                }
            }
            return amount;
        }
        public bool IsSmbAlive()
        {
            return AliveAmount() != 0;
        }

        //----------------------------------Методы, необходимые для битвы.
        /// <summary>
        /// Возвращает случайного живого персонажа из какой-либо команды.
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        private static Human RndAliveUnit(Player team)
        {
            int buffNum;
            do
            {
                buffNum = random.Next(team.Army.Length);

            } while (team.Army[buffNum] == null || !team.Army[buffNum].IsAlive());

            return team.Army[buffNum];
        }
        /// <summary>
        /// Осуществляется выбор персонажа.
        /// </summary>
        /// <returns></returns>
        private Human ChoicedUnit()
        {
            /*В этом методе соблюдается порядок вывода.
                Сперва Fighter --> Ninja --> Samurai
                Затем выбор и раздача приказа.
             */    
            string AliveUnitsDesc = "";
            MoonSpeak("Выберите воина, который пойдет в бой", true, 0);
            Thread.Sleep(100);

            int[] memberOrder = new int[Army.Length];
            int memberCounter = 0;

            // Сперва Fighter
            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Fighter")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "^";
                }
            }
            AliveUnitsDesc = AliveUnitsDesc == "" ? " " : AliveUnitsDesc;
            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);
            AliveUnitsDesc += "\n\nНиндзи:^";
            // Затем Ninja

            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Ninja")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "^";
                }

            }

            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);
            AliveUnitsDesc += "\n\nСамураи:^";

            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Samurai")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "^";
                }
            }
            // И завершает эту фиерию Samurai
            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);

            MoonSpeak("\nВоители:");
            int choice = Choice(AliveUnitsDesc.Split('^'));
            
            // Здесь мы отдаём приказ.
            MoonSpeak("\n" 
                + Army[memberOrder[choice]].Name 
                + order_replicas[CounterOrders% order_replicas.Length]);

            return Army[memberOrder[choice]];
        }

        /// <summary>
        /// Отвечает за то, КТО и КОГО бьёт.
        /// Передаём в него армии.
        /// </summary>
        /// <param name="defender"></param>
        /// <param name="attaker"></param>
        public static void BattleScenario(Player defender, Player attaker)
        {
            // Проверяем, кто есть кто.
            bool IsChoice = !(attaker.Army[0] as Fighter).isEnemy
            && Choice(new string[] { "Выбрать случайного бойца", "Назначить самому" }) == 1;

            // bool IsChoice = false;
            // Рандом, если выбора не было.
            switch (IsChoice ? attaker.ChoicedUnit() : RndAliveUnit(attaker))
            {
                case Human buff when buff.ToString().Split('.')[2] == "Fighter":
                    (buff as Fighter).Attack(RndAliveUnit(defender));
                    break;
                case Human buff when buff.ToString().Split('.')[2] == "Ninja":
                    (buff as Ninja).Attack(RndAliveUnit(defender));
                    break;
                case Human buff when buff.ToString().Split('.')[2] == "Samurai":
                    (buff as Samurai).Attack(RndAliveUnit(defender));
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Здесь запускается битва, где должны умереть
        /// Либо все персонажи user, либо deads компьютера.
        /// Очерёдность регулируется переменной queue.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="computer"></param>
        /// <param name="deads"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static int BattleCircle(Player user, Player computer, int deads, ref int queue)
        {
            if (deads <= 0)
            {
                throw new ArgumentException();
            }

            int prevousAlive = computer.AliveAmount();

            // Начинаем цикл битвы! С рандома!
            for (; prevousAlive - computer.AliveAmount() != deads; queue++)
            {
                // Атака по очереди, пока количество мертвецов не заставит противника отступить или вас проиграть.
                if (CounterRounds % 5 == 0 && Choice(new string[] { "Ускорить вывод", "Оставить как есть" }) == 0)
                {
                    Human.TextSpeedChanger();
                }

                MoonSpeak("Раунд: " + CounterRounds);               

                CounterRounds++;
                BattleScenario(queue % 2 == 0 ? user : computer, // defender
                               queue % 2 == 0 ? computer : user);// attacker
                // А живы ли мы сами?
                if (!user.IsSmbAlive())
                {
                    // Возращаем ещё более секретный код, который говорит, что мы проиграли.
                    return 0;
                }
            }

            MoonSpeak("*Противник несёт потери и отступает*", ticTac: 15);
            // Возращаем секретный код, который говорит, что всё ок.
            return 1;
        }
        /// <summary>
        /// Производит повтор.
        /// Возвращает всё в начальное положение.
        /// </summary>
        public static void Restart()
        {
            CounterLocation = 0;
            CounterPlot = 0;
            CounterRounds = 1;
            money = 10.0;
        }
    }
}
using System;
using System.Threading;
using warrior_s_path_WF.Units;
using System.Windows.Forms;
using System.Drawing;
using static warrior_s_path_WF.FormUsing;

namespace warrior_s_path_WF
{
    /// <summary>
    /// Игровой процесс.
    /// </summary>
    class Player
    {
        //----------------------------------Сюжет и описание локаций битвы.
        // Описание локаций.
        public readonly static string[] locations_descriptions = {
                "\r\n*Узкая дорога между скал*\r\n\r\n" +
                "Сквозь пелену падающих листьев,\r\n" +
                "задыхаясь от ещё не осевшей пыли после землятрясения,\r\n" +
                "в ущелье проникает солнечный свет.\r\n" +
                "Часовой вступил на эту узкую дорогу.\r\n" +
                "Отправьте кого-нибудь разобраться с ним.\r\n\r\n",
                // 1 противник.

                "\r\n*Кузнеца на самом краю деревни*\r\n\r\n" +
                "Вы делаете глубокий вдох и ощущаете запах огня и дыма,\r\n" +
                "сырого каменного пола и полированного металла - запах силы.\r\n" +
                "Ваши воины воодушевлены,\r\n" +
                "но и противник не дремлет...\r\n\r\n",
                // 1 противник.

                "\r\n*Улица*\r\n\r\n" +
                "Некогда оживлённый центр торговли страдает от тоски\r\n" +
                "и только хриплый стон ветхих конструкций\r\n" +
                "и шелест бумажных стен не даёт тишине окончательно взять своё." +
                "\r\nВас замечают.\r\n\r\n",
                // 2 противника.

                "\r\n*Ворота замка*\r\n\r\n" +
                "Величие превратилось в руины. Одному богу известно,\r\n" +
                "как красные ворота летней резиденции могли уцелеть.\r\n" +
                "Как будто бы древняя магия не даёт им упать.\r\n" +
                "Под ними вас ожидают стражи.\r\n\r\n",
                // 3 противника.

                "\r\n*Замок, покои местного господина*\r\n\r\n" +
                "Расставленная по фэншую комната на фоне общего хаоса\r\n" +
                "выглядит до невозможности нелепо. Вы получили нужные документы!\r\n" +
                "Осталось только уйти.\r\n" +
                "Шум во дворе наводит волнение.\r\n" +
                "Вас встречает элитная гвардия,\r\n" +
                "имеющая своей задачей не допустить мародёров\r\n" +
                "до ценнейших сведений.\r\n" +
                "Выход только 1, убежать не получится. Придётся драться\r\n\r\n"
                // 3 противника.
        };
        // Сюжетец.
        public readonly static string[] plot = {
                "*В 8 веке нашей эры в Японии после страшного землятрясения*\r\n\r\n" +
                "Глава клана Фудзивара узнаёт о том,\r\n" +
                "что катастрофа потрепала замок другого\r\n" +
                "влиятельного клана Минамото,\r\n" +
                "славившегося своими богатствами.\r\n\r\n" +
                "Однако эти богатства не трогают душу господина,\r\n" +
                "поскольку он считает, что это грязные деньги.\r\n\r\n" +
                "Если бы было возможно узнать наверняка, тогда клан Минамото\r\n" +
                "больше не смог бы дальше конкурировать на политической арене Японии,\r\n" +
                "что позволило бы вашему клану достичь пика своего могущества,\r\n" +
                "а ваш господин вполне бы смог претендовать на титул сёгуна.\r\n\r\n\r\n" +
                "Господин дал вам 10 Ман поручил Вам собрать отряд\r\n" +
                "и во чтобы то ни стало добыть документы и любые другие улики,\r\n" +
                "подтверждающие предположения.\r\n\r\n" +

                "Вам предстоит выбрать лучших воинов и проникнуть в разрушенный замок.\r\n",

                "*Ваш господин, придворный бухгалтер и вы собрались для осмотра кандидатов*\r\n",// Дальше делаю сам кастинг. По 3 случайных бойца. 
                
                "\r\n*Ястреб унёс депешу в Head Hunter Japan\r\n" +
                "с щедрой предоплатой в размере 10 тысяч йен*\r\n" +
                "Спустя 2 дня 10 лучших бойцов сёгуната прибыли к вашим воротам.\r\n",
                // Показываю всю армию.

                "\tГосподин кивнул вам вслед.\r\n" +
                "[&]______________________________________________________________[&]\r\n\r\n",

                "Путь самурая - это смерть. Безмолвно цветёт сакура.\r\n\r\n",

                "Вы получили славу и имение, а ваш господин стал сёгуном.\r\n\r\n" +
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
        /// Использую статический класс,
        /// позволяющий реализовывать данный функционал по добавлению в TextBox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ln"></param>
        /// <param name="ticTac"></param>
        public static void MoonSpeak(string message, bool ln = true, int ticTac = 0)
        {
            //Создаём подобие делегата.

            //FormUsing.Moonspeak(message, ln);
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
                                          "\t", unit2 != null ? unit2.Apperance[j] + "\r" : "");
                    MoonSpeak(visual, ticTac: 0);
                }
                // Вывод имён.
                else
                {
                    string nameArr = "              ";

                    visual = string.Concat(
                         "\r    ", unit0 != null ? unit0.Name + nameArr.Substring(2 + unit0.Name.Length) : "", "  ",
                             "  ", unit1 != null ? unit1.Name + nameArr.Substring(2 + unit1.Name.Length) : "", "  ",
                             "  ", unit2 != null ? unit2.Name + nameArr.Substring(2 + unit2.Name.Length) + "\r\n" : "");
                    MoonSpeak(visual, ticTac: 15);
                }
            }
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
                MoonSpeak($"\r\nКоличество бойцов: {i}\r", ticTac: 0);

                Human[] buffUnits = { RndUnit(), RndUnit(), RndUnit() };
                Show3Units(buffUnits[0], buffUnits[1], buffUnits[2]);

                MoonSpeak($"Баланс: {money} тысяч иен.", ticTac: 0);
                MoonSpeak("\rКандидаты: \r\n", ticTac: 0);

                Army[i] = buffUnits[Choice(new string[] {
                    $"{(buffUnits[0] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[0].Price} тысяч иен.\r",
                    $"{(buffUnits[1] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[1].Price} тысяч иен.\r",
                    $"{(buffUnits[2] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[2].Price} тысяч иен.\r"
                })];

                if (Army[i].Price > money)
                {
                    i--;
                    MoonSpeak("\rВы не можете нанять его.\r" +
                              "У вас нет таких денег, а банки не выдают кредиты на секретные операции.\r" +
                              "Увидев, как неумело их потенциальный командир распоряжается деньгами,\rвоины сбежали в ужасе.\r", ticTac: 25);
                }
                else money -= Army[i].Price;

                if (money > 0 && money < 1.0)
                {
                    MoonSpeak("\rВы наняли армию и у вас осталось ещё немного денег.\rВаша ЗП увеличилась!\r\n\n", ticTac: 30);
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
            int shift = 1, choiceNum = -1;

            MoonSpeak("");
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i] == "")
                {
                    shift--;
                }
                else MoonSpeak($"{i + shift}] {options[i]}", ticTac: 0);
            }


            if (options.Length >= 10 && choiceNum == 1)
            {
                //choiceNum = Choice(new string[] { options[0], options[options.Length - 1] }) + 1 == 2 ? 10 : 1;
            }

            FormUsing.Choice(options);
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
            MoonSpeak("Выберите воина, который пойдет в бой\r", true, 0);
            Thread.Sleep(100);

            int[] memberOrder = new int[Army.Length];
            int memberCounter = 0;

            // Сперва Fighter
            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Fighter")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "\r\n^";
                }
            }
            AliveUnitsDesc = AliveUnitsDesc == "" ? " " : AliveUnitsDesc;
            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);
            AliveUnitsDesc += "\r\nНиндзи:\r^";
            // Затем Ninja

            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Ninja")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "\r\n^";
                }

            }

            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);
            AliveUnitsDesc += "\r\nСамураи:\r^";

            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Samurai")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "\r\n^";
                }
            }
            // И завершает эту фиерию Samurai
            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);

            MoonSpeak("\r\nВоители:\r");
            
            int choice = Choice(AliveUnitsDesc.Split('^'));

            // Здесь мы отдаём приказ.
            MoonSpeak("\r"
                + Army[memberOrder[choice]].Name
                + order_replicas[CounterOrders % order_replicas.Length]);

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
                MoonSpeak("Раунд: " + CounterRounds + "\r");
                FormUsing.NextRound();

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

            MoonSpeak("*Противник несёт потери и отступает*\r", ticTac: 15);
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

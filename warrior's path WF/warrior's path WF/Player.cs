using System;
using System.Threading;
using warrior_s_path_WF.Units;
using System.Windows.Forms;
using System.Drawing;
using static warrior_s_path_WF.FormUsing;

namespace warrior_s_path_WF
{
    //---------------Класс и методы для игрового процесса.
    class Player
    {
        public Player(Form1 form)
        {
            form1 = form;
        }
        protected static Form1 form1;

        // Описание локаций.
        public readonly static string[] locations_descriptions = {
                "\r\n*Узкая дорога между скал*\r\r\n\r\n" +
                "Сквозь пелену падающих листьев,\r\r\n" +
                "задыхаясь от ещё не осевшей пыли после землятрясения,\r\r\n" +
                "в ущелье проникает солнечный свет.\r\r\n" +
                "Часовой вступил на эту узкую дорогу.\r\r\n" +
                "Отправьте кого-нибудь разобраться с ним.\r\r\n\r\n",
                // 1 противник.

                "\r\n*Кузнеца на самом краю деревни*\r\r\n\r\n" +
                "Вы делаете глубокий вдох и ощущаете запах огня и дыма,\r\r\n" +
                "сырого каменного пола и полированного металла - запах силы.\r\r\n" +
                "Ваши воины воодушевлены,\r\r\n" +
                "но и противник не дремлет...\r\r\n\r\n",
                // 1 противник.

                "\r\n*Улица*\r\n\r\n" +
                "Некогда оживлённый центр торговли страдает от тоски\r\r\n" +
                "и только хриплый стон ветхих конструкций\r\r\n" +
                "и шелест бумажных стен не даёт тишине окончательно взять своё.\r" +
                "\r\nВас замечают.\r\r\n\r\n",
                // 2 противника.

                "\r\n*Ворота замка*\r\r\n\r\n" +
                "Величие превратилось в руины. Одному богу известно,\r\r\n" +
                "как красные ворота летней резиденции могли уцелеть.\r\r\n" +
                "Как будто бы древняя магия не даёт им упать.\r\r\n" +
                "Под ними вас ожидают стражи.\r\r\n\r\n",
                // 3 противника.

                "\r\n*Замок, покои местного господина*\r\r\n\r\n" +
                "Расставленная по фэншую комната на фоне общего хаоса\r\r\n" +
                "выглядит до невозможности нелепо. Вы получили нужные документы!\r\r\n" +
                "Осталось только уйти.\r\r\n" +
                "Шум во дворе наводит волнение.\r\r\n" +
                "Вас встречает элитная гвардия,\r\r\n" +
                "имеющая своей задачей не допустить мародёров\r\r\n" +
                "до ценнейших сведений.\r\r\n" +
                "Выход только 1, убежать не получится. Придётся драться\r\r\n\r\n"
                // 3 противника.
        };
        // Сюжетец.
        public readonly static string[] plot = {
                "*В 8 веке нашей эры в Японии после страшного землятрясения*\r\r\n\r\n" +
                "Глава клана самураев Камуто Хреновато узнаёт о том,\r\r\n" +
                "что катастрофа потрепала замок влиятельного клана Тэрияки,\r\r\n" +
                "славившегося своими богатствами.\r\r\n\r\n" +
                "Однако эти богатства не трогают душу господина,\r\r\n" +
                "поскольку он считает, что это грязные деньги.\r\r\n\r\n" +
                "Если бы было возможно узнать наверняка, тогда клан Тэрияки\r\r\n" +
                "больше не смог бы дальше конкурировать на политической арене Японии,\r\r\n" +
                "что позволило бы вашему клану достичь пика своего могущества,\r\r\n" +
                "а ваш господин вполне сможет претендовать на титул сёгуна.\r\r\n\r\n\r\n" +
                "Господин дал вам 10 Ман поручил Вам собрать отряд\r\r\n" +
                "и во чтобы то ни стало добыть документы и любые другие улики,\r\r\n" +
                "подтверждающие предположения.\r\r\n\r\n",

                "Вам предстоит выбрать лучших воинов и проникнуть в замок.\r\r\n",

                "*Ваш господин, придворный бухгалтер и вы собрались для осмотра кандидатов*\r\r\n",// Дальше делаю сам кастинг. По 3 случайных бойца. 
                
                "*Ястреб унёс депешу в Head Hunter Japan\r\r\n" +
                "с щедрой предоплатой в размере 10 тысяч йен*\r\r\n" +
                "Спустя 2 дня 10 лучших бойцов сёгуната прибыли к вашим воротам.\r\r\n",
                // Показываю всю армию.

                "\tГосподин кивнул вам вслед.\r\r\n" +
                "[&]______________________________________________________________[&]\r\r\n\r\n",

                "Путь самурая - это смерть. Безмолвно цветёт сакура.\r\r\n\r\n",

                "Вы получили славу и имение, а ваш господин стал сёгуном.\r\r\n\r\n" +
                "В годовщину победы вас пригласили на званный ужин. Пойти?\r\r\n"
        };

        // И дальше статические счётчики для каждого текста.
        public static int CounterLocation = 0;
        public static int CounterPlot = 0;

        private static int CounterRounds = 1;
        // Скопированный метод для разговора, ибо жирно будет делать наследование ради 1 метода. Хотя на этот раз он public.
        public static void MoonSpeak(string message, bool ln = true, int ticTac = 25)
        {
            FormUsing.Moonspeak(message);
            Choice(new string[] { "\r\nПодтвердите" });
        }

        private static Random random = new Random();
        private Human[] Army = new Human[10];
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
                    visual = string.Concat("  ", unit0 != null ? unit0.Apperance[j] : "",
                                           "  ", unit1 != null ? unit1.Apperance[j] : "",
                                           "  ", unit2 != null ? unit2.Apperance[j] : "");
                    MoonSpeak(visual + "\r\n", ticTac: 0);
                }
                // Вывод имён.
                else
                {
                    string nameArr = "              ";

                    visual = string.Concat(
                          "\r   ", unit0 != null ? unit0.Name + nameArr.Substring(2 + unit0.Name.Length) : "", "  ",
                             "  ", unit1 != null ? unit1.Name + nameArr.Substring(2 + unit1.Name.Length) : "", "  ",
                             "  ", unit2 != null ? unit2.Name + nameArr.Substring(2 + unit2.Name.Length) : "");
                    MoonSpeak(visual, ticTac: 15);
                }
            }
            MoonSpeak("\r\n");
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

        protected static double money = 10.0;
        public void ArmyCasting()
        {
            for (int i = 0; i < 10 && money >= 1; i++)
            {
                MoonSpeak($"Количество бойцов: {i}\r\r\n", ticTac: 0);

                Human[] buffUnits = { RndUnit(), RndUnit(), RndUnit() };
                Show3Units(buffUnits[0], buffUnits[1], buffUnits[2]);

                MoonSpeak($"\r\nБаланс: {money} тысяч иен.\r\n", ticTac: 0);
                MoonSpeak("Кандидаты: \r\n", ticTac: 0);

                Army[i] = buffUnits[Choice(new string[] {
                    $"{(buffUnits[0] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[0].Price} тысяч иен.\r\r\n",
                    $"{(buffUnits[1] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[1].Price} тысяч иен.\r\r\n",
                    $"{(buffUnits[2] as Fighter).GetCharacterDescription()} Cтоимость: {buffUnits[2].Price} тысяч иен.\r\r\n"
                })];

                if (Army[i].Price > money)
                {
                    i--;
                    MoonSpeak("Вы не можете нанять его.\r\n" +
                              "У вас нет таких денег, а банки не выдают кредиты на секретные операции.\r\n" +
                              "Увидев, как неумело их потенциальный командир распоряжается деньгами,\r\nвоины сбежали в ужасе.\r\n\r\n", ticTac: 25);
                }
                else money -= Army[i].Price;

                if (money > 0 && money < 1.0)
                {
                    MoonSpeak("Вы наняли армию и у вас осталось ещё немного денег.\r\nВаша ЗП увеличилась!\r\n\r\n", ticTac: 30);
                }
            }
        }
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

        private static Human RndAliveUnit(Player team)
        {
            int buffNum;
            do
            {
                buffNum = random.Next(team.Army.Length);

            } while (team.Army[buffNum] == null || !team.Army[buffNum].IsAlive());

            return team.Army[buffNum];
        }
        private Human ChoicedUnit()
        {
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
            AliveUnitsDesc += "\r\n\r\nНиндзи:^";
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
            AliveUnitsDesc += "\r\n\r\nСамураи:^";

            for (int i = 0; i < Army.Length; i++)
            {
                if (Army[i] != null && Army[i].IsAlive() && Army[i].ToString().Split('.')[2] == "Samurai")
                {
                    memberOrder[memberCounter++] = i;
                    AliveUnitsDesc += (Army[i] as Fighter).GetCharacterDescription(false) + "^";
                }
            }
            // И завершает эту фиерию Samurais
            AliveUnitsDesc = AliveUnitsDesc.Remove(AliveUnitsDesc.Length - 1);

            MoonSpeak("\r\nВоители:");

            return Army[memberOrder[Choice(AliveUnitsDesc.Split('^'))]];
        }

        // Отвечает за то, КТО и КОГО бьёт.
        // Передаём в него армии.
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

        // Запуситили битву и установили очередь.
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
                LabelRound.Text = ("Round " + (CounterRounds < 10 ? "00" + CounterRounds.ToString() : "")
            + (CounterRounds >= 10 && CounterRounds < 100 ? "0" + CounterRounds.ToString() : "")
                                   + (CounterRounds >= 100 ? "" + CounterRounds.ToString() : ""));

                if (CounterRounds % 30 == 0 && Choice(new string[] { "Ускорить вывод", "Оставить как есть" }) == 0)
                {
                     Human.TextSpeedChanger();
                }
                
                CounterRounds++;
                BattleScenario(queue % 2 == 0 ? user : computer, // defender
                               queue % 2 == 0 ? computer : user);// attacker
                //  А живы ли мы сами?
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

        public static void Restart()
        {
            CounterLocation = 0;
            CounterPlot = 0;
            CounterRounds = 1;
            money = 10.0;
        }
    }
}

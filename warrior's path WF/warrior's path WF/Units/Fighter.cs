using System;

namespace warrior_s_path_WF.Units
{
    /// <summary>
    /// Класс, реализующий персонажа Fighter.
    /// </summary>
    class Fighter : Human
    {
        /// <summary>
        /// Конструктор, в котором задаются все параметры о персонаже
        /// минимум рандома и его максимум на 1 характеристику.
        /// </summary>
        /// <param name="healthyMin">Здоровье</param>
        /// <param name="healthyMax">Здоровье</param>
        /// <param name="damageMin">Урон</param>
        /// <param name="damageMax">Урон</param>
        /// <param name="guardMin">Щит</param>
        /// <param name="guardMax">Щит</param>
        /// <param name="evadeMin">Уклонение</param>
        /// <param name="evadeMax">Уклонение</param>
        /// <param name="price">Стоимость юнита</param>
        /// <param name="isEnemy">враждебность</param>
        public Fighter(int healthyMin = 50, int healthyMax = 70, /*Здоровье*/
                       int damageMin = 10, int damageMax = 10,  /*Урон*/
                       int guardMin = 3, int guardMax = 6,     /*Щит*/
                       double evadeMin = 0, double evadeMax = 0, /*Ловкость/уклонеине*/
                       double price = 1,/*Стоимость юнита*/
                       bool isEnemy = false) : base(healthyMin, healthyMax, price)
        {
            damage = random.Next(damageMin, damageMax + 1);
            guard = random.Next(guardMin, guardMax + 1);
            evade = random.NextDouble() % (evadeMax - evadeMin) + evadeMin;

            this.isEnemy = isEnemy;

            Apperance = new string[] {
                @"    /|&|\  /+\",
                @"    ( ~_~) \+/",
                "/\"\"\\_{88}\"  | ",
                @"[^^] {88} '.| ",
                @"[^^] {==}   | ",
                @"\__/ |''|   | ",
                @"     |_ |_  | ",
            };

            Tomb = new string[] {
                "              ",
                "     ____     ",
                "    | RIP||   ",
                "    |''''||   ",
                "____|''''||__ ",
                "    //////    ",
                "   //////     "};
        }

        //----------------------------------Параметры персонажа.
        //Враждебность.
        public bool isEnemy;
        //Урон персонажа.
        protected int damage;
        // Защита персонажа. После каждой атаки на этого игрока, защита уменьшается на 1,
        // (вплоть до 0, не может принимать отрицательных значений). 
        protected int guard;
        public int Guard
        {
            set
            {
                if (guard <= 0)
                {
                    guard = 0;
                }
                else guard = value;
            }
            get => guard;
        }
        // Шанс уклонения.
        protected double evade;
        public double Evade
        {
            get => evade;
        }
        /// <summary>
        /// Главный метод проверки выполнимости шанса чего-либо.
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public bool Chance(double chance)
        {
            return random.Next(0, 10) < chance;
        }

        //---------------------------------- Реплики персонажей на все случаи жизни.
        // Для реплик во время атаки помимо стандартных. 
        // Каждые 2 - это удара от каждой из сторон. Имя : реплика!
        protected readonly string[] attack_replicas = {
            "Вступаю в бой!\r\n",
            "Получай!\r\n",
            "За господина!\r\n",
            "Аттакую!\r\n",
            "Выполняю приказ!\r\n",
            "Следую указанию!\r\n",
            "Аааааа!!!\r\n",
            "Омае ва мои шиндеиру!!\r\n",
            "Сейчас ты познаешь свою смерть!\r\n",
            "Что мы говорим богу смерти? Не сегодня!\r\n",
            "Умри, дьявол\r\n!",
            "Не дёргайся! Я только за ушком почешу!!\r\n",
            "Стань же прахом\r\n",
            "Убегай, если ещё стоишь на ногах!\r\n",
            "Готовься к смерти!\r\n",
            "*Сверлит противника взглядом*\r\n",
            "Тебе не справиться со мной!\r\n",
            "С такой рожей тебе место среди бесов!\r\n",
            "Я награждаю тебя своим фирменным надменным взглядом\r\n",
            "Может ты сам убьёшься?\r\n",
            "Я меч во тьме...\r\n",
            "Испытай силу моего оружия!\r\n",
            "Ты не пройдешь\r\n"


        };
        // Если противник повержен. Имя : реплика!
        protected readonly string[] win_replicas = {
            "Победа над противником!\r\n",
            "Противник повержен!\r\n",
            "Противник погиб!\r\n",
            "Враг устранён.\r\n",
            "\"Счастливого пути в АД!\" - последнее, что услышал противник перед смертью.\r\n",
            "Мёртвый противник больше не поднимет свой меч.\r\n",
            "Духи будут помнить это сражение.\r\n",
            "*Тяжёлые вздохи и мёртвый противник*\r\n",
            "ААААА.... Противник мёртв!\r\n",
            "Больше от противника не возникнет хлопот!\r\n"
        };
        // Для реплик во время смерти помимо стандартных. Имя + реплика.
        protected readonly string[] die_replicas = {
            " пал смертью храбрых.\r\n",
            " был молодым, но не удачливым.\r\n",
            " дошёл свой путь до конца.\r\n",
            " по-английски ушёл в отставку.\r\n",
            " исполнил свой долг.\r\n",
            " испустил дух.\r\n",
            " сделал всё, что было в его силах.\r\n",
            " умер, однако, легенды никогда не умирают.\r\n",
            " последний раз пересёкся с вами взглядами.\r\n" };

        //---------------------------------- Счётчики для реплик.
        public static int AttackCounter = 0;
        public static int WinsCounter = 0;
        public static int DiesCounter = 0;
        //---------------------------------- Всё взаимодействие с персонажем.
        /// <summary>
        /// Метод, реализующий атаку.
        /// </summary>
        /// <param name="enemy"></param>
        virtual public void Attack(Human enemy)
        {
            // Заразны ли мы?
            if (this.poisoned)
            {
                this.healthy -= this.healthy * 0.07 < 5 ? 5 : this.healthy * 0.07;
                if (!this.IsAlive())
                {
                    this.Dead("*Умер от яда*");
                    return;
                }
            }
            // Кричим ли мы пафосные монологи? 
            if (!this.isEnemy)
            {
                MoonSpeak("\r\n" + this.Name + ": " + attack_replicas[AttackCounter % attack_replicas.Length] + "\r", ticTac: textSpeed);
                AttackCounter++;
            }

            // А уклонится ли он?
            if (!Chance((enemy as Fighter).Evade))
            {
                // А контратакует ли самурай? А самурай ли?
                if (enemy is Samurai &&
                    Chance((enemy as Samurai).Reliation))
                {
                    ShowMob(new string[] {
                        "\r\n        \\\\     ",
                        @"   /++] \\    ",
                        @"  // />}'\\__ ",
                        @"  {[][]}_/\]  ",
                        @" *[][]'    \' ",
                        @" //  _\\      ",
                        "[.  [.        \r\n"
                    });
                    MoonSpeak("||_________" + ((enemy as Fighter).isEnemy ?
                        "Противник контратаковал!" :
                       $"{enemy.Name} контратаковал!" + "\r\n"), ticTac: textSpeed);
                    (enemy as Samurai).Attack(this);
                }
                else
                {
                    int dmg = this.damage - (enemy as Fighter).Guard;
                    // Описание удара + BattleCry(), нужный по инструкции. И именно в таком порядке.
                    MoonSpeak($"{(isEnemy ? "\r\n" : "")}Из команды {(isEnemy ? "клана Минамото" : "вашего отряда")}\r\n" +
                              $"{this.ToString().Split('.')[2]} {(isEnemy ? "" : Name + " ")}атаковал " +
                              $"{(isEnemy ? Name : "противника")} и нанёс " +
                              $"{(dmg > 0 ? dmg : 0)} урона.\r\n" +
                              $"Оставшееся здоровье: {enemy.Healthy - (dmg > 0 ? dmg : 0)}/{(enemy as Fighter).maxHealthy} (Щит: {(enemy as Fighter).Guard - 1})", ticTac: textSpeed);
                    this.BattleCry();
                    MoonSpeak("");

                    (enemy as Fighter).Healthy -= (dmg > 0 ? dmg : 0);
                    // После успешного удара у противника сбивается щит на 1 единицу.

                    // А атакующий-то ниндзя! А если ядом попрыскает? Фу, противный!
                    if (this is Ninja && Chance((this as Ninja).Poison))
                    {
                        SetPoisoned(enemy);
                        MoonSpeak($"||_________{((enemy as Fighter).isEnemy ? "Противник" : Name)} отравлен!", ticTac: textSpeed);
                    }

                    (enemy as Fighter).Guard -= 1;
                    // Могилку ставим.
                    if (!enemy.IsAlive() && !(enemy as Fighter).isEnemy)
                    {
                        (enemy as Fighter).Dead();
                    }
                }
            }
            else MoonSpeak($"||_________{((enemy as Fighter).isEnemy ? "Противник" : Name)} уклонился!\r", ticTac: textSpeed);
        }
        /// <summary>
        /// Боевой клич, требующийся по заданию.
        /// </summary>
        virtual public void BattleCry()
        {
            MoonSpeak("Хыа!!\r\n", ticTac: 20);
        }
        /// <summary>
        /// Описание персонажа.
        /// </summary>
        /// <param name="showClass"></param>
        /// <returns></returns>
        virtual public string GetCharacterDescription(bool showClass = true)
        {
            return $"{(this.isEnemy ? "Противник" : Name)}: {(showClass ? this.ToString().Split('.')[2] + "," : "")} \r" +
                $"HP:{healthy}/{maxHealthy} Щит: {guard} Урон: {damage}{(poisoned ? " ОТРАВЛЕН" : "")}\r";
        }
        /// <summary>
        /// Смерть персонажа.
        /// </summary>
        /// <param name="deadReason"></param>
        override public void Dead(string deadReason = "")
        {
            if (deadReason != "")
            {
                // *Умер от яда*
                MoonSpeak(deadReason + "\r\n", ticTac: textSpeed);
            }
            if (!this.isEnemy)
            {
                MoonSpeak(Name + die_replicas[DiesCounter % die_replicas.Length] + "\r\n", ticTac: textSpeed);
                DiesCounter++;
            }
            else
            {
                MoonSpeak("// ||================||\\\\\r", ticTac: 0);
                MoonSpeak("/| | Противник умер | |\\\r", ticTac: 5);
                MoonSpeak("||_|________________|_||\r", ticTac: 0);
            }
            base.Dead();
        }
    }
}
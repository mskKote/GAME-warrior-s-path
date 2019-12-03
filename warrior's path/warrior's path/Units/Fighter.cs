using System;

namespace warrior_s_path.Units
{
    /// <summary>
    /// Класс, реализующий персонажа Fighter.
    /// </summary>
    class Fighter : Human
    {
        /// <summary>
        /// Конструктор, в котором задаются все параметры о персонаже.
        /// </summary>
        /// <param name="healthyMin"></param>
        /// <param name="healthyMax"></param>
        /// <param name="damageMin"></param>
        /// <param name="damageMax"></param>
        /// <param name="guardMin"></param>
        /// <param name="guardMax"></param>
        /// <param name="evadeMin"></param>
        /// <param name="evadeMax"></param>
        /// <param name="price"></param>
        /// <param name="isEnemy"></param>
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
                "    // // //     ",
                "   // // //      "};
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
            return random.Next(0, 100) < chance;
        }

        //---------------------------------- Реплики персонажей на все случаи жизни.
        // Для реплик во время атаки помимо стандартных. 
        // Каждые 2 - это удара от каждой из сторон. Имя : реплика!
        protected readonly string[] attack_replicas = {
            "Вступаю в бой!",
            "Получай!",
            "За господина!",
            "Аттакую!",
            "Выполняю приказ!",
            "Следую указанию!",
            "Аааааа!!!",
            "Омае ва мои шиндеиру!!",
            "Сейчас ты познаешь свою смерть!",
            "Что мы говорим богу смерти? Не сегодня!",
            "Умри, дьявол!",
            "Не дёргайся! Я только за ушком почешу!!",
            "Стань же прахом",
            "Убегай, если ещё стоишь на ногах!",
            "Готовься к смерти!",
            "*Сверлит противника взглядом*",
            "Тебе не справиться со мной!",
            "С такой рожей тебе место среди бесов!",
            "Я награждаю тебя своим фирменным надменным взглядом",
            "Может ты сам убьёшься?",
            "Я меч во тьме...",
            "Испытай силу моего оружия!",
            "Ты не пройдешь"


        };
        // Если противник повержен. Имя : реплика!
        protected readonly string[] win_replicas = {
            "Победа над противником!",
            "Противник повержен!",
            "Противник погиб!",
            "Враг устранён.",
            "\"Счастливого пути в АД!\" - последнее, что услышал противник перед смертью.",
            "Мёртвый противник больше не поднимет свой меч.",
            "Духи будут помнить это сражение.",
            "*Тяжёлые вздохи и мёртвый противник*",
            "ААААА.... Противник мёртв!",
            "Больше от противника не возникнет хлопот!"
        };
        // Для реплик во время смерти помимо стандартных. Имя + реплика.
        protected readonly string[] die_replicas = {
            " пал смертью храбрых.",
            " был молодым, но не удачливым.",
            " дошёл свой путь до конца.",
            " по-английски ушёл в отставку.",
            " исполнил свой долг.",
            " испустил дух.",
            " сделал всё, что было в его силах.",
            " умер, однако, легенды никогда не умирают.",
            " последний раз пересёкся с вами взглядами." };

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
                MoonSpeak("\n" + this.Name + ": " + attack_replicas[AttackCounter % attack_replicas.Length] + "\n", ticTac: textSpeed);
                AttackCounter++;
            }

            // А уклонится ли он?
            if (!Chance((enemy as Fighter).Evade))
            {
                // А контратакует ли самурай? А самурай ли?
                if (enemy is Samurai &&
                    Chance((enemy as Samurai).Reliation))
                {
                    MoonSpeak("||_________" + ((enemy as Fighter).isEnemy ?
                        "Противник контратаковал!" :
                       $"{enemy.Name} контратаковал!"), ticTac: textSpeed);
                    (enemy as Samurai).Attack(this);
                }
                else
                {
                    int dmg = this.damage - (enemy as Fighter).Guard;
                    // Описание удара + BattleCry(), нужный по инструкции. И именно в таком порядке.
                    MoonSpeak($"{(isEnemy ? "\n" : "")}Из команды {(isEnemy ? "клана Минамото" : "вашего отряда")}\n" +
                              $"{this.ToString().Split('.')[2]} {(isEnemy ? "" : Name + " ")}атаковал " +
                              $"{(isEnemy ? Name : "противника")} и нанёс " +
                              $"{(dmg > 0? dmg : 0)} урона.\n" +
                              $"Оставшееся здоровье: {enemy.Healthy}", ticTac: textSpeed);
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
            else MoonSpeak($"||_________{((enemy as Fighter).isEnemy ? "Противник" : Name)} уклонился!", ticTac: textSpeed);
        }
        /// <summary>
        /// Боевой клич, требующийся по заданию.
        /// </summary>
        virtual public void BattleCry()
        {
            MoonSpeak("Хыа!!", ticTac: 20);
        }
        /// <summary>
        /// Описание персонажа.
        /// </summary>
        /// <param name="showClass"></param>
        /// <returns></returns>
        virtual public string GetCharacterDescription(bool showClass = true)
        {
            return $"{(this.isEnemy ? "Противник" : Name)}: {(showClass ? this.ToString().Split('.')[2] + "," : "")} \n" +
                $"HP:{healthy}/{maxHealthy} Щит: {guard} Урон: {damage}{(poisoned ? " ОТРАВЛЕН" : "")}";
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
                MoonSpeak(deadReason, ticTac: textSpeed);
            }
            if (!this.isEnemy)
            {
                MoonSpeak(Name + die_replicas[DiesCounter % die_replicas.Length], ticTac: textSpeed);
                DiesCounter++;
            }
            else
            {
                MoonSpeak(@"// ||================||\\", ticTac: 0);
                MoonSpeak(@"/| | Противник умер | |\", ticTac: 5);
                MoonSpeak("||_|________________|_||\n", ticTac: 0);
            }
            base.Dead();
        }
    }
}
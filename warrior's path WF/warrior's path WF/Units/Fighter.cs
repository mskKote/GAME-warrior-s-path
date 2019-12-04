using System;

namespace warrior_s_path_WF.Units
{
    class Fighter : Human
    {
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
            
            //evade = Convert.ToDouble(random.Next(
            //    Convert.ToInt32(evadeMin * 10),     //Минимум
            //    Convert.ToInt32(evadeMax * 10 + 1)))//Максимум
            //    / 10;
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
        public bool isEnemy;

        protected int damage;
        //защита персонажа. После каждой атаки на этого игрока, защита уменьшается на 1,
        //(вплоть до 0, не может принимать отрицательных значений). 
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
        //вероятность уклонения текущего персонажа от удара противника.
        protected double evade;
        public double Evade
        {
            get => evade;
        }
        public bool Chance(double chance)
        {
            return random.Next(0, 100) < chance;
        }

        //Для реплик во время атаки помимо стандартных. 
        //Каждые 2 - это удара от каждой из сторон. Имя : реплика!
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
        //Если противник повержен. Имя : реплика!
        protected readonly string[] win_replicas = {
            "Победа над противником!",
            "Противник повержен!",
            "За господина!",
            "Враг устранён.",
            "Счастливого пути в АД!",
            "Он больше не поднимет свой меч.",
            "Духи будут помнить наше сражение.",
            "*Тяжёлые вздохи*",
            "ААААА....",
            "Больше от него не возникнет хлопот!"
        };
        //Для реплик во время смерти помимо стандартных. Имя + реплика.
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
        //Для реплик во время смерти помимо стандартных.
        protected readonly string[] order_replicas = {
            ", разберись с ним!",
            ", не дай ему победить.",
            ", я рассчитываю на тебя.",
            ", порви его!",
            ", я выбираю тебя!",
            ", не подведи своего господина!",
            ", аттакуй его.",
            ", вступи в бой!"};
        //Статические счётчики.
        public static int AttackCounter = 0;
        public static int WinsCounter = 0;
        public static int DiesCounter = 0;
        public static int OrdersCounter = 0;
        virtual public void Attack(Human enemy)
        {
            //Заразны ли мы?
            if (this.poisoned)
            {
                this.healthy -= this.healthy * 0.07 < 5 ? 5 : this.healthy * 0.07;
                if (!this.IsAlive())
                {
                    this.Dead("*Умер от яда*");
                    return;
                }
            }
            //Кричим ли мы пафосные монологи? 
            if (!this.isEnemy)
            {
                MoonSpeak("\n" + this.Name + ": " + attack_replicas[AttackCounter % attack_replicas.Length] + "\n", ticTac: textSpeed);
                AttackCounter++;
            }

            //А уклонимся?
            if (!Chance((enemy as Fighter).Evade))
            {
                //А контратакует ли самурай? А самурай ли?
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
                    //Описание удара +BattleCry(), нужный по инструкции.И именно в таком порядке.
                    MoonSpeak($"{(isEnemy ? "\n" : "")}Из команды {(isEnemy ? "клана Тэрияки" : "вашего отряда")}\n" +
                              $"{this.ToString().Split('.')[2]} {(isEnemy ? "" : Name + " ")}атаковал " +
                              $"{(isEnemy ? Name : "противника")} и нанёс " +
                              $"{this.damage - (enemy as Fighter).Guard} урона.\n" +
                              $"Оставшееся здоровье: {enemy.Healthy}", ticTac: textSpeed);
                    this.BattleCry();
                    MoonSpeak("");

                    (enemy as Fighter).Healthy -= (this.damage - (enemy as Fighter).Guard);
                    //После успешного удара у противника сбивается щит на 1 единицу.

                    //А атакующий-то ниндзя! А если ядом попрыскает? Фу, противный!
                    if (this is Ninja && Chance((this as Ninja).Poison))
                    {
                        SetPoisoned(enemy);
                        MoonSpeak($"||_________{((enemy as Fighter).isEnemy ? "Противник" : Name)} отравлен!", ticTac: textSpeed);
                    }

                    (enemy as Fighter).Guard -= 1;
                    //Могилку ставим.
                    if (!enemy.IsAlive() && !(enemy as Fighter).isEnemy)
                    {
                        (enemy as Fighter).Dead();
                    }
                }
            }
            else MoonSpeak($"||_________{((enemy as Fighter).isEnemy ? "Противник" : Name)} уклонился!", ticTac: textSpeed);
        }
        //Боевой клич. 
        virtual public void BattleCry()
        {
            MoonSpeak("Хыа!!", ticTac: 20);
        }
        virtual public string GetCharacterDescription(bool showClass = true)
        {
            return $"{(this.isEnemy ? "Противник" : Name)}: {(showClass ? this.ToString().Split('.')[2] + "," : "")} \n" +
                $"HP:{healthy}/{maxHealthy} Щит: {guard} Урон: {damage}{(poisoned ? " ОТРАВЛЕН" : "")}";
        }
        override public void Dead(string deadReason = "")
        {
            if (deadReason != "")
            {
                //*Умер от яда*
                MoonSpeak(deadReason, ticTac: textSpeed);
            }
            if (!this.isEnemy)
            {
                MoonSpeak(Name + die_replicas[DiesCounter % die_replicas.Length], ticTac: textSpeed);
                DiesCounter++;
            }
            else
            {

                MoonSpeak(@"//||================||\\", ticTac: 0);
                MoonSpeak(@"/| | Противник умер | |\", ticTac: 5);
                MoonSpeak("||_|________________|_||\n", ticTac: 0);
            }
            base.Dead();
        }
    }
}
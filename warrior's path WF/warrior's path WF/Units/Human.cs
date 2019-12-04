using System;
using System.Threading;

namespace warrior_s_path_WF.Units
{
    // Сейчас посмотрю, что должно быть 
    // Сейчас в конструкторе я могу задавать имя, здоровье, максимальное здоровье.
    class Human
    {
        protected static Random random = new Random();
        public Human(int healthyMin, int healthyMax, double price)
        {
            Healthy = random.Next(healthyMin, healthyMax + 1);
            maxHealthy = healthy;
            name = names[random.Next(names.Length)];

            this.price = price;
        }

        protected string[] names = {
            "Тетсуя", "Акира", "Хидео(Гений)","Хидеки", "Мизуки",
            "Кавасаки", "Ямадзаки", "Шибадзаки", "Кенничи", "Макото",
            "Мацуда",  "Такаши", "Сатоши", "Ясухиро", "Ишики",
            "Акихиро",  "Акио", "Дайске", "Джиро", "Ичиро",
            "Кеиджи",  "Кеншин", "Кэтсеро", "Кэйташи", "Ходжи",
            "Тошиюки",  "Рэйден", "Дзётаро", "Кэйташи", "Наоки"};
                                   
        // Имя есть у каждого юнита. 
        private string name;
        public string Name
        {
            get => name;
        }
        // И внешность.
        private string[] apperance = new string[7];
        public string[] Apperance
        {
            get => apperance;
            set
            {
                if (value is string[] && value.Length == 7)
                {
                    apperance = value;
                }
            }
        }
        // И могила...
        protected string[] tomb = new string[7];
        public string[] Tomb
        {
            get { return tomb; }
            set
            {
                if (value is string[] && value.Length == 7)
                {
                    // Это можно изменить.
                    tomb = value;
                }
                else throw new ArgumentException();
            }
        }

        public void ShowMob(string[] look)
        {
            foreach (var part in look)
            {
                MoonSpeak(" " + part, ticTac: 0);
            }
        }

        protected bool poisoned = false;
        public static void SetPoisoned(Human defender)
        {
            if (defender is Ninja)
            {
                defender.poisoned = true;
            }
        }

        private double price = 0;
        public double Price
        {
            get => price;
        }

        // При == 0 персонаж “умирает”. 
        protected double healthy;
        protected double maxHealthy;
        public double Healthy
        {
            get => healthy;
            protected set
            {
                healthy = value <= 0 ? 0 : value;
            }
        }
        public bool IsAlive()
        {
            return healthy > 0;
        }
        virtual public void Dead(string deadReason = "")
        {
            apperance = Tomb;
        }

        protected static int textSpeed = 20;
        public static void TextSpeedChanger() {
            textSpeed -= textSpeed <= 0 ? 0 : 5; 
        }
        public static void MoonSpeak(string message, bool ln = true, int ticTac = 20)
        {
            //GameplayText.AppendText(message);
            //Choice(new string[] { "\r\nПодтвердите" });
        }
    }
}
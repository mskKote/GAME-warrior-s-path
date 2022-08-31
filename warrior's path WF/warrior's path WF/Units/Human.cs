using System;
using System.Threading;

namespace warrior_s_path_WF.Units
{
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
            
        //----------------------------------Имя.
        /// <summary>
        /// Массив япоснких имён для воинов.
        /// </summary>
        protected string[] names = {
            "Тетсуя", "Акира", "Хидео","Хидеки", "Мизуки",
            "Кавасаки", "Ямадзаки", "Шибадзаки", "Кенничи", "Макото",
            "Мацуда",  "Такаши", "Сатоши", "Ясухиро", "Ишики",
            "Акихиро",  "Акио", "Дайске", "Джиро", "Ичиро",
            "Кеиджи",  "Кеншин", "Кэтсеро", "Кэйташи", "Ходжи",
            "Тошиюки",  "Рэйден", "Дзётаро", "Кэйташи", "Наоки"};

        private string name;
        public string Name
        {
            get => name;
        }

        //----------------------------------Персонаж.
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

        //----------------------------------Могила персонажа.
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
        /// <summary>
        /// Метод, который выводит персонажа на консоль. 
        /// Это может быть tomb или apperance.
        /// </summary>
        /// <param name="look"></param>
        public void ShowMob(string[] look)
        {
            foreach (var part in look)
            {
                MoonSpeak(" " + part + "\r\n", false);
            }
        }

        //----------------------------------Отравление.
        protected bool poisoned = false;
        /// <summary>
        /// Позволяет задать отравить определённого Human.
        /// </summary>
        /// <param name="defender"></param>
        public static void SetPoisoned(Human defender)
        {
            if (defender is Ninja)
            {
                defender.poisoned = true;
            }
        }

        //----------------------------------Цена
        private double price = 0;
        /// <summary>
        /// Позволяет получить цену персонажа. 
        /// Сама цена задаётся исключительно в конструкторах персонажа.
        /// </summary>
        public double Price
        {
            get => price;
        }

        //----------------------------------Здоровье персонажа.
        protected double healthy;
        protected double maxHealthy;
        /// <summary>
        /// Отвечает за взаимодействие со здоровьем персонажа.
        /// </summary>
        public double Healthy
        {
            get => healthy;
            protected set
            {
                healthy = value <= 0 ? 0 : value;
            }
        }
        /// <summary>
        /// Позволяет проверить, жив ли персонаж.
        /// </summary>
        /// <returns></returns>
        public bool IsAlive()
        {
            return healthy > 0;
        }
        /// <summary>
        /// Убивает персонажа.
        /// </summary>
        /// <param name="deadReason"></param>
        virtual public void Dead(string deadReason = "")
        {
            Healthy = 0;
            apperance = Tomb;
        }

        //----------------------------------Взаимодействие с консолью [Требуется по заданию]
        protected static int textSpeed = 20;
        /// <summary>
        /// Позволяет ускорять вывод текста.
        /// </summary>
        public static void TextSpeedChanger()
        {
            textSpeed -= textSpeed <= 0 ? 0 : 2;
        }
        /// <summary>
        /// Главный метод для любого взаимодействия с консолью.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ln"></param>
        /// <param name="ticTac"></param>
        public static void MoonSpeak(string message, bool ln = true, int ticTac = 20)
        {
            FormUsing.Moonspeak(message, ln);
        }
    }
}
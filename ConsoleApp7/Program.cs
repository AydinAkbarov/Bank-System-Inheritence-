using System;
using System.Collections.Generic;

namespace BankInterface
{
    interface IOperation
    {
        void Process();
    }

    interface IClient
    {
        void ShowInfo();
    }

    interface ICredit
    {
        void CalculatePercent();
        void Payment();
    }

    interface IManager
    {
        void Organize();
        void CalculateSalaries();
    }

    interface IWorker
    {
        void AddOperation(Operation operation);
    }

    interface ICEO
    {
        void Control();
        void Organize();
        void MakeMeeting();
        void DecreasePercentage(double percent);
    }

    interface IBank
    {
        double CalculateProfit();
        void ShowClientCredit(string fullName);
        void PayCredit(Client client, double money);
        void ShowAllCredit();
    }

    class Operation : IOperation
    {
        public string GUID { get; set; }
        public string ProcessName { get; set; }
        public DateTime DateTime { get; set; }

        public Operation(string processName)
        {
            GUID = Guid.NewGuid().ToString();
            ProcessName = processName;
            DateTime = DateTime.Now;
        }

        public void Process()
        {
            Console.WriteLine($"{ProcessName} ");
        }
    }

    class Client : IClient
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string LiveAddress { get; set; }
        public string WorkAddress { get; set; }
        public double Salary { get; set; }

        public Client(string name, string surname, int age, string liveAddress, string workAddress, double salary)
        {
            GUID = Guid.NewGuid().ToString();
            Name = name;
            Surname = surname;
            Age = age;
            LiveAddress = liveAddress;
            WorkAddress = workAddress;
            Salary = salary;
        }

        public void ShowInfo()
        {
            Console.WriteLine($@"
Ad: {Name}
Soyad: {Surname}
Yas: {Age}
Yasayis unvani: {LiveAddress}
Is unvani: {WorkAddress}
Maas: {Salary}");
        }
    }

    class Credit : ICredit
    {
        public string GUID { get; set; }
        public Client Client { get; set; }
        public double Amount { get; set; }
        public double Percent { get; set; }
        public int Months { get; set; }

        public Credit(Client client, double amount, double percent, int months)
        {
            GUID = Guid.NewGuid().ToString();
            Client = client;
            Amount = amount;
            Percent = percent;
            Months = months;
        }

        public void CalculatePercent()
        {
            double percent = Amount * (Percent / 100);
            Console.WriteLine($"Kredit faizi: {percent}");
        }

        public void Payment()
        {
            double monthly = Amount / Months;
            Console.WriteLine($"Ayliq odenis: {monthly}");
        }
    }

    class Manager : IManager
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public Manager(string name, string surname, int age, string position, double salary)
        {
            GUID = Guid.NewGuid().ToString();
            Name = name;
            Surname = surname;
            Age = age;
            Position = position;
            Salary = salary;
        }

        public void Organize()
        {
            Console.WriteLine("Menecer organizasiya teskil etdi");
        }

        public void CalculateSalaries()
        {
            Console.WriteLine("Maas hesablanir");
        }
    }

    class Worker : IWorker
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<Operation> Operations { get; set; } = new List<Operation>();

        public Worker(string name, string surname, int age, string position, double salary, string startTime, string endTime)
        {
            GUID = Guid.NewGuid().ToString();
            Name = name;
            Surname = surname;
            Age = age;
            Position = position;
            Salary = salary;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void AddOperation(Operation operation)
        {
            Operations.Add(operation);
            Console.WriteLine($"{operation.ProcessName}");
        }
    }

    class CEO : ICEO
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public CEO(string name, string surname, int age, string position, double salary)
        {
            GUID = Guid.NewGuid().ToString();
            Name = name;
            Surname = surname;
            Age = age;
            Position = position;
            Salary = salary;
        }

        public void Control()
        {
            Console.WriteLine("CEO banki yoxlayir");
        }

        public void Organize()
        {
            Console.WriteLine("CEO organizasiya teskil etdi");
        }

        public void MakeMeeting()
        {
            Console.WriteLine("CEO gorus kecirdi");
        }

        public void DecreasePercentage(double percent)
        {
            Console.WriteLine($"CEO faizi {percent}% azaltdi");
        }
    }

    class Bank : IBank
    {
        public string Name { get; set; }
        public double Budget { get; set; }
        public double Profit { get; set; }
        public CEO Ceo { get; set; }
        public List<Worker> Workers { get; set; } = new List<Worker>();
        public List<Manager> Managers { get; set; } = new List<Manager>();
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<Credit> Credits { get; set; } = new List<Credit>();

        public Bank(string name, double budget)
        {
            Name = name;
            Budget = budget;
        }

        public double CalculateProfit()
        {
            double totalProfit = 0;
            foreach (var credit in Credits)
            {
                totalProfit += credit.Amount * (credit.Percent / 100);
            }
            Profit = totalProfit;
            return Profit;
        }

        public void ShowClientCredit(string fullName)
        {
            bool found = false;
            foreach (var credit in Credits)
            {
                string clientFullName = credit.Client.Name + " " + credit.Client.Surname;
                if (clientFullName == fullName)
                {
                    if (!found)
                    {
                        Console.WriteLine($"{fullName} ucun kredit:");
                        found = true;
                    }
                    Console.WriteLine($"Mebleg: {credit.Amount}");
                    Console.WriteLine($"Faiz: {credit.Percent}%");
                    Console.WriteLine($"Muddet: {credit.Months}");
                }
            }
            if (!found)
            {
                Console.WriteLine("Kredit tapilmadi !");
            }
        }

        public void PayCredit(Client client, double money)
        {
            var credit = Credits.Find(c => c.Client.GUID == client.GUID);
            if (credit != null)
            {
                double aylıq = credit.Amount / credit.Months;
                if (money >= aylıq)
                {
                    credit.Amount -= aylıq;
                    credit.Months--;
                    Console.WriteLine($"Odenis tamalandi. Qaliq: {credit.Amount}");
                }
                else
                {
                    Console.WriteLine("Mebleg kifayet qeder deyil !");
                }
            }
            else
            {
                Console.WriteLine("Kredit tapilmadi !");
            }
        }

        public void ShowAllCredit()
        {
            if (Credits.Count > 0)
            {
                foreach (var credit in Credits)
                {
                    Console.WriteLine($@"
Musteri: {credit.Client.Name} {credit.Client.Surname}
Mebleg: {credit.Amount}
Faiz: {credit.Percent}%
Muddet: {credit.Months}
------------------------");
                }
            }
            else
            {
                Console.WriteLine("Kredit yoxdu !");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank("Kapital Bank", 1000000);

            CEO ceo = new CEO("Elvin", "Mammadov", 45, "CEO", 10200);
            bank.Ceo = ceo;

            Manager manager = new Manager("Nigar", "Aliyeva", 36, "Menecer", 5500);
            bank.Managers.Add(manager);

            Worker worker = new Worker("Kamran", "Rahimov", 27, "Isci", 2200, "09:00", "18:00");
            bank.Workers.Add(worker);

            Client client1 = new Client("Ramil", "Hasanov", 32, "Sumqayit", "Baku", 3200);
            Client client2 = new Client("Ilaha", "Quliyeva", 29, "Ganja", "Baku", 2800);
            bank.Clients.AddRange(new[] { client1, client2 });

            bank.Credits.AddRange(new[]
            {
                new Credit(client1, 6000, 14, 10),
                new Credit(client2, 3500, 13, 8)
            });

            Console.WriteLine($@"
=== Bank melumati ===
Bank adi: {bank.Name}
Bank budcesi: {bank.Budget}
Bank qazanci: {bank.CalculateProfit()}
");

            Console.WriteLine("=== CEO ===");
            ceo.Control();
            ceo.Organize();
            ceo.MakeMeeting();
            ceo.DecreasePercentage(2);

            Console.WriteLine("\n=== Menecer ===");
            manager.Organize();
            manager.CalculateSalaries();

            Console.WriteLine("\n=== Musteriler ===");
            client1.ShowInfo();
            Console.WriteLine("\n-------------\n");
            client2.ShowInfo();

            Console.WriteLine("\n=== Kreditler ===");
            bank.ShowAllCredit();

            Console.WriteLine("\n=== Musteri Kredit ===");
            bank.ShowClientCredit("Ramil Hasanov");

            Console.WriteLine("\n=== Kredit Odenisi ===");
            bank.PayCredit(client1, 700);

            Console.WriteLine("\n=== Qalan Kreditler ===");
            bank.ShowAllCredit();
        }
    }
}

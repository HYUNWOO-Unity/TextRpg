namespace 숙제제작
{  
    namespace TextRPG
    {
        class Program
        {
            static void Main(string[] args)
            {
                Game game = new Game();
                game.Start();
            }
        }

        public class Game
        {
            private Player player;
            private List<Item> inventory;
            private Shop shop;

            public Game()
            {
                inventory = new List<Item>();
                shop = new Shop();
            }

            public void Start()
            {
                Console.WriteLine("헬반도 마을에 오신 여러분 환영합니다.");
                Console.Write("닉네임을 입력해주세요: ");
                string playerName = Console.ReadLine();
                string job = ChooseJob();

                player = new Player(playerName, job);
                InitializeInventory(job);

                while (true)
                {
                    ShowMainMenu();
                }
            }

            private string ChooseJob()
            {
                Console.WriteLine("직업 목록:");
                Console.WriteLine("1. 전사");
                Console.WriteLine("2. 마법사");
                Console.WriteLine("3. 궁수");
                Console.Write("원하시는 직업의 번호를 입력해주세요: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        return "전사"; // 기본 공격력 15, 방어력 10
                    case "2":
                        return "마법사"; // 기본 공격력 10, 방어력 5
                    case "3":
                        return "궁수"; // 기본 공격력 12, 방어력 7
                    default:
                        Console.WriteLine("잘못된 입력입니다. 전사로 설정합니다.");
                        return "전사";
                }
            }

            private void InitializeInventory(string job)
            {
                switch (job)
                {
                    case "전사":
                        inventory.Add(new Item("기본 검", 2, 0, "쉽게 볼 수 있는 기본 검입니다.", false));
                        break;
                    case "마법사":
                        inventory.Add(new Item("기본 지팡이", 3, 0, "기본 마법을 사용할 수 있는 지팡이입니다.", false));
                        break;
                    case "궁수":
                        inventory.Add(new Item("기본 활", 4, 0, "정확한 조준을 도와주는 활입니다.", false));
                        break;
                }
            }

            private void ShowMainMenu()
            {
                Console.WriteLine("\n1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.Write("원하시는 행동을 입력해주세요: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayStatus();
                        break;
                    case "2":
                        ManageInventory();
                        break;
                    case "3":
                        shop.ShowShop(player);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

            private void DisplayStatus()
            {
                Console.WriteLine("\n**상태 보기**");
                Console.WriteLine($"Lv. {player.Level:D2}");
                Console.WriteLine($"{player.Name} ({player.Job})");
                Console.WriteLine($"공격력 : {player.AttackPower}");
                Console.WriteLine($"방어력 : {player.Defense}");
                Console.WriteLine($"체 력 : {player.Health}");
                Console.WriteLine($"Gold : {player.Gold} G");
                Console.WriteLine("0. 나가기");
                Console.ReadLine(); // 대기
            }

            private void ManageInventory()
            {
                Console.WriteLine("\n**인벤토리**");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    string equipped = inventory[i].IsEquipped ? "[E]" : " ";
                    Console.WriteLine($"- {i + 1} {equipped}{inventory[i].Name} | 공격력 +{inventory[i].AttackPower} | 방어력 +{inventory[i].Defense} | {inventory[i].Description}");
                }
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    ManageEquipment();
                }
                else if (choice != "0")
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            private void ManageEquipment()
            {
                Console.WriteLine("\n**인벤토리 - 장착 관리**");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.Count; i++)
                {
                    string equipped = inventory[i].IsEquipped ? "[E]" : " ";
                    Console.WriteLine($"- {i + 1} {equipped}{inventory[i].Name} | 공격력 +{inventory[i].AttackPower} | 방어력 +{inventory[i].Defense} | {inventory[i].Description}");
                }
                Console.WriteLine("0. 나가기");

                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int index) && index > 0 && index <= inventory.Count)
                {
                    ToggleEquipment(index - 1);
                }
                else if (choice != "0")
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }

            private void ToggleEquipment(int index)
            {
                var item = inventory[index];
                item.IsEquipped = !item.IsEquipped;
                Console.WriteLine($"{item.Name}이(가) {(item.IsEquipped ? "장착되었습니다." : "장착 해제되었습니다.")}");
            }
        }

        public class Player
        {
            public int Level { get; private set; }
            public string Name { get; }
            public string Job { get; }
            public int AttackPower { get; private set; }
            public int Defense { get; private set; }
            public int Health { get; private set; }
            public int Gold { get; private set; }

            public Player(string name, string job)
            {
                Level = 1;
                Name = name;
                Job = job;
                InitializeStats(job);
                Gold = 5000;
            }

            private void InitializeStats(string job)
            {
                switch (job)
                {
                    case "전사":
                        AttackPower = 15;
                        Defense = 10;
                        Health = 100;
                        break;
                    case "마법사":
                        AttackPower = 10;
                        Defense = 5;
                        Health = 80;
                        break;
                    case "궁수":
                        AttackPower = 12;
                        Defense = 7;
                        Health = 90;
                        break;
                    default:
                        AttackPower = 10;
                        Defense = 5;
                        Health = 80;
                        break;
                }
            }
        }

        public class Item
        {
            public string Name { get; }
            public int AttackPower { get; }
            public int Defense { get; }
            public string Description { get; }
            public bool IsEquipped { get; set; }

            public Item(string name, int attackPower, int defense, string description, bool isEquipped)
            {
                Name = name;
                AttackPower = attackPower;
                Defense = defense;
                Description = description;
                IsEquipped = isEquipped;
            }
        }

        public class Shop
        {
            private List<ShopItem> shopItems;

            public Shop()
            {
                shopItems = new List<ShopItem>
            {
                new ShopItem("기본 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000, false),
                new ShopItem("헬 갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, false),
                new ShopItem("지옥의 갑옷", 0, 15, "지옥의 왕이 사용했다는 전설의 갑옷입니다.", 3500, false),
                new ShopItem("헬 창", 7, 0, "지옥의 전사들이 사용했다는 전설의 창입니다.", 2000, false),
                new ShopItem("헬 지팡이", 9, 0, "지옥의 마법사 가 사용 했다는 전설의 지팡이 입니다.", 2000, false),
                new ShopItem("헬 활", 8, 0, "지옥의 궁수들이 사용했다는 전설의 활입니다.", 2000, false)
            };
            }

            public void ShowShop(Player player)
            {
                Console.WriteLine("\n**상점**");
                Console.WriteLine($"[보유 골드]\n{player.Gold} G");
                Console.WriteLine("[아이템 목록]");
                foreach (var item in shopItems)
                {
                    string status = item.IsPurchased ? "구매완료" : $"{item.Price} G";
                    Console.WriteLine($"- {item.Name} | 방어력 +{item.Defense} | 공격력 +{item.AttackPower} | {item.Description} | {status}");
                }
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    BuyItem(player);
                }
            }

            private void BuyItem(Player player)
            {
                Console.WriteLine("\n구매할 아이템 번호를 입력하세요:");
                for (int i = 0; i < shopItems.Count; i++)
                {
                    if (!shopItems[i].IsPurchased)
                    {
                        Console.WriteLine($"{i + 1}. {shopItems[i].Name} - {shopItems[i].Price} G");
                    }
                }

                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int index) && index > 0 && index <= shopItems.Count)
                {
                    var item = shopItems[index - 1];
                    if (item.IsPurchased)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player.Gold >= item.Price)
                    {
                      
                        item.IsPurchased = true;
                        Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
                    }
                    else
                    {
                        Console.WriteLine("Gold 가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public class ShopItem
        {
            public string Name { get; }
            public int AttackPower { get; }
            public int Defense { get; }
            public string Description { get; }
            public int Price { get; }
            public bool IsPurchased { get; set; }

            public ShopItem(string name, int attackPower, int defense, string description, int price, bool isPurchased)
            {
                Name = name;
                AttackPower = attackPower;
                Defense = defense;
                Description = description;
                Price = price;
                IsPurchased = isPurchased;
            }
        }
    }




}

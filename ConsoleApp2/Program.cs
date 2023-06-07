using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Project2;

namespace Project2
{


    class Class1
    {


        // Start in your house, choose some gear that is in the house
        // Go to the forest with gear
        // Encounter 3 battles. First battle is one goblin.
        // Second battle is two goblins.
        // Third battle is a Troll that uses a hard hitting move that requires "Brace" (a move) to be used.
        // Arrive at some location. Game ends.

        // Character Properties; HP, DEF, ATK Value.
        // You can Attack, Brace (defend), Item, Ability
        // Turn based combat.
        // Enemy Properties; HP, DEF, Atk Value
        // They will follow some sort of script, eg 75% to Atk normally, 25% to use hard-hitting move.
    }

    public class MonsterStats
    {
        public int _enemyHp;
        public int _enemyDef;
        public int _enemyAtk;
        public MonsterStats(int enemyHp, int enemyDef, int enemyAtk)
        {
            _enemyHp = enemyHp;
            _enemyDef = enemyDef;
            _enemyAtk = enemyAtk;
        }
 

        public int MonsterHp
        {
            get { return _enemyHp; }
            set { _enemyHp = value; }
        }
        public int MonsterDef
        {
            set { _enemyDef = value; }
            get { return _enemyDef; }
        }
        public int MonsterAtk
        { 
            set { _enemyAtk = value; }
            get { return _enemyAtk; }
        }

        public void GoblinTurn(MonsterStats enemy, PlayerStats player, int brace)
        {
            Console.WriteLine("Goblin strikes you!");
            if ((enemy.MonsterAtk - player.PlayerDef)/brace <= 0)
            {
                Console.WriteLine("He does no damage.");
            }
            else
            {
                player.PlayerHp -= ((enemy.MonsterAtk - player.PlayerDef) / brace);
            }
        }

        public void TrollTurn(MonsterStats enemy, PlayerStats player, int brace)
        {
            Random rnd = new Random();
            int num = rnd.Next(0, 4);
            if (num == 3)
            {
                Console.WriteLine("Troll prepares to hit you VERY hard.");
                player.PlayerTurn(player, enemy, out brace);
                if (((enemy.MonsterAtk*4) - player.PlayerDef) / brace <= 0)
                {
                    Console.WriteLine("He did no damage?");
                }
                else
                {
                    player.PlayerHp -= (((enemy.MonsterAtk*4) - player.PlayerDef) / brace);
                }
            }
            else
            {
                Console.WriteLine("Troll hits you.");
                if ((enemy.MonsterAtk - player.PlayerDef) / brace <= 0)
                {
                    Console.WriteLine("He did no damage?");
                }
                else
                {
                    player.PlayerHp -= ((enemy.MonsterAtk - player.PlayerDef) / brace);
                }
            }
        }
    }

    public class PlayerStats 
    {
        public int _playerHp;
        public int _playerDef;
        public int _playerAtk;
        public string _playerWeapon;
        public string _playerArmourChest;
        public string _playerArmourGlove;

        public PlayerStats(int playerHp, int playerDef, int playerAtk, string playerWeapon, string playerArmourChest, string playerArmourGlove)
        {
            _playerHp = playerHp;
            _playerDef = playerDef;
            _playerAtk = playerAtk;
            _playerWeapon = playerWeapon;
            _playerArmourChest = playerArmourChest;
            _playerArmourGlove = playerArmourGlove;
        }

       

        public int PlayerHp
        {
            get { return _playerHp; }
            set { _playerHp = value; }
        }

        public int PlayerDef
        {
            get { return _playerDef; }
            set { _playerDef = value; }
        }
        public int PlayerAtk
        {
            get { return _playerAtk; }
            set { _playerAtk = value; }
        }
        public string PlayerWeapon
        {
            get { return _playerWeapon; }
            set { _playerWeapon = value; }

        }

        public string PlayerArmourChest
        {
            get { return _playerArmourChest;}
            set { _playerArmourChest = value;}
        }

        public string PlayerArmourGlove
        {
            get { return _playerArmourGlove; }
            set { _playerArmourGlove = value; }
        }
        public string FullStats(PlayerStats player)
        {
            return $"{player.PlayerHp} Health, {player.PlayerDef} Defense, {player.PlayerAtk} Attack, {player.PlayerWeapon} Weapon, {player.PlayerArmourChest}," +
                $"{player.PlayerArmourGlove}.";
        }

        public void PlayerTurn(PlayerStats player, MonsterStats enemy, out int braceTaken) 
        {
            braceTaken = 1;
            Console.WriteLine("Player's Turn.");
            Console.WriteLine("Choose your action: \n 1. Attack \n 2. Brace (80% Damage Reduction)");
            var playerBattleChoice = Console.ReadLine();
            playerBattleChoice = playerBattleChoice.ToString();
            if (playerBattleChoice == "1")
            {
                if (player.PlayerAtk - enemy.MonsterDef <= 0)
                {
                    Console.WriteLine("No damage done!");
                }
                else
                {
                    enemy.MonsterHp -= player.PlayerAtk - enemy.MonsterDef;
                    if (enemy.MonsterHp <= 0)
                    {
                        return;
                    }
                    Console.WriteLine($"You hit him for {player.PlayerAtk - enemy.MonsterDef} damage. His HP is now {enemy.MonsterHp}.");
                }
            }
            else if (playerBattleChoice == "2")
            {
                braceTaken = 5;
            }
            else
            {
                Console.WriteLine("No Action Taken/Invalid Action");
            }
        }
       
     }
    class gameStart
    {
        static void Main()
        {
            MonsterStats battleGoblinA = new MonsterStats(30, 2, 2);
            MonsterStats battleGoblinB = new MonsterStats(30, 2, 2);
            MonsterStats battleGoblinC = new MonsterStats(30, 2, 2);
            MonsterStats troll = new MonsterStats(50, 2, 3);
            string[,] equipmentTable = new string[,] {{"0", "Nothing Equipped", "0", "0", "0" }, { "1", "Sword", "0", "0", "10" },
                { "2", "Bow", "0", "0", "10" }, { "3", "Spear", "0", "0", "12" },
                { "4", "Greatsword", "0", "0", "20" }, { "5", "Leather Chest", "15", "1", "0" },
                { "6", "Leather Gloves", "30", "0", "0" }};
            PlayerStats player = new PlayerStats(80, 0, 0, "No Weapon", "No Armour Chest Piece", "No Gloves");
            Console.WriteLine("Pick a weapon; \n 1. A Sword \n 2. A bow \n 3. A Spear \n 4. A Greatsword");
            var playerWeaponChoice = Console.ReadLine();
            playerWeaponChoice = playerWeaponChoice.ToString();
            for (int i = 0; i < 5; i++)
            {
                if (equipmentTable[i, 0] == playerWeaponChoice)
                {
                    player.PlayerWeapon = equipmentTable[i, 1];
                    player.PlayerHp += Int32.Parse(equipmentTable[i, 2]);
                    player.PlayerDef += Int32.Parse(equipmentTable[i, 3]);
                    player.PlayerAtk += Int32.Parse(equipmentTable[i, 4]);
                }
                else
                {
                    continue;
                }
            }
            Console.WriteLine(player.PlayerWeapon);
            Console.WriteLine("Pick your armour; \n 1. Leather Chest \n 2. Leather Gloves");
            var playerArmourChoice = Console.ReadLine();
            playerArmourChoice = playerArmourChoice + 4;
            playerArmourChoice = playerArmourChoice.ToString();
            if(playerArmourChoice == "5")
            {
                player.PlayerArmourChest = equipmentTable[5, 1];
                player.PlayerHp += Int32.Parse(equipmentTable[5, 2]);
                player.PlayerDef += Int32.Parse(equipmentTable[5, 3]);
                player.PlayerAtk += Int32.Parse(equipmentTable[5, 4]);
            }
            else
            {
                player.PlayerArmourGlove = equipmentTable[6, 1];
                player.PlayerHp += Int32.Parse(equipmentTable[6, 2]);
                player.PlayerDef += Int32.Parse(equipmentTable[6, 3]);
                player.PlayerAtk += Int32.Parse(equipmentTable[6, 4]);
            }
            Console.WriteLine($"Your final stats are; {player.FullStats(player)}");

            //First battle starts - 1 goblin
            int looper = 0;
            while (looper == 0)
            {
                int braceTaken;
                
                player.PlayerTurn(player, battleGoblinA, out braceTaken);
                if(battleGoblinA.MonsterHp <= 0)
                {
                    break;
                }
                Console.WriteLine("Opponent's Turn");
                battleGoblinA.GoblinTurn(battleGoblinA, player, braceTaken);
                Console.WriteLine($"Your current HP is; {player.PlayerHp}");
                
            }
            Console.WriteLine("You defeated the enemy.");
            Console.WriteLine("First battle over.");

            Console.WriteLine("Second battle begins. Two goblins.");
            looper = 0;
            while (looper == 0)
            {
                int braceTaken = 0;
                
                if(battleGoblinC.MonsterHp > 0 && battleGoblinB.MonsterHp > 0)
                {
                    Console.WriteLine("Who to attack? \n 1. Goblin A \n 2. Goblin B.");
                    var chosenEnemy = Console.ReadLine();
                    if (chosenEnemy == "1")
                    {
                        player.PlayerTurn(player, battleGoblinC, out braceTaken);
                    }
                    else
                    {
                        player.PlayerTurn(player, battleGoblinB, out braceTaken);
                    }
                }
                else
                {
                    if(battleGoblinC.MonsterHp <= 0)
                    {
                        player.PlayerTurn(player, battleGoblinB, out braceTaken);
                    }
                    else
                    {
                        player.PlayerTurn(player, battleGoblinC, out braceTaken);
                    }
                }
                if (battleGoblinC.MonsterHp <= 0 && battleGoblinB.MonsterHp <= 0)
                {
                        break;
                }
                Console.WriteLine("Opponent's Turn");
                battleGoblinC.GoblinTurn(battleGoblinC, player, braceTaken);
                battleGoblinB.GoblinTurn(battleGoblinB, player, braceTaken);
                Console.WriteLine($"Your current HP is; {player.PlayerHp}");


            }
            Console.WriteLine("Second battle over!");
            Console.WriteLine("Third battle start; Troll.");
            looper = 0;
            while (looper == 0)
            {
                int braceTaken;

                player.PlayerTurn(player, troll, out braceTaken);
                if (troll.MonsterHp <= 0)
                {
                    break;
                }
                Console.WriteLine("Opponent's Turn");
                battleGoblinA.TrollTurn(troll, player, braceTaken);
                Console.WriteLine($"Your current HP is; {player.PlayerHp}");

            }
            Console.WriteLine("Troll defeated.");
        }
    }
}


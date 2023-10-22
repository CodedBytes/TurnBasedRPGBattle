using System;
using System.Threading;
using TurnBased_RPG_Battle_System.Audios;

namespace TurnBased_RPG_Battle_System.Classes
{
    public class Game
    {
        // Instantiating a basic player with no status.
        private Player player = new Player();

        // Instantiating the audio
        AudioHandler audio = new AudioHandler();


        /// <summary>
        /// Responsable for starting the game!
        /// </summary>
        public void Start()
        {
            // Filling a new player with basic stats
            player.PlayerDefaultValues(35, 1, 10, 7, 2, 3);

            // Playing the bgm
            audio.PlaySound(@"init.wav");

            // Initating game!
            Console.WriteLine("System::Game -> Hello player and welcome to my turn based rpg battle style game!");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> This game provides you one of the ways a turn based battle game can be made.");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> Here i'll be using my logic to reproduce the most accurate system i can code.");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> But first of all, would you please tell us about your name?");

            // Define player name.
            PlayerName:;
            Console.Write("System::User -> ");
            if (!verifyingInput(Console.ReadLine(), 1)) goto PlayerName;

            // Choosing Class section
            Console.WriteLine("\n\nSystem::Game -> Now, in order to proceed with the game, tell us about you class!");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> Theres a few classes you can use for now:");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> 1. Warrior    2. Magician");
            Thread.Sleep(2000);

            // Process class choice  
            chooseClass:;
            Console.Write("System::Game -> Please, choose your class: ");
            if (!verifyingInput(Console.ReadLine(), 2)) goto chooseClass;

            // Proceeding to the next part.
            Console.WriteLine("\nSystem::Game -> Alright, so.. your name is {0} hm ?", player.returnName());
            Thread.Sleep(3000);
            Console.WriteLine("\nSystem::Game -> The main goal of this game, as i said before,\n"
            + "is to reproduce the most accurated turn based battle system as possible.");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> Using my own programing logic!");
            Thread.Sleep(3000);
            Console.WriteLine("System::Game -> Let's get started!");

            // Stoping the music
            audio.StopSound();
            Thread.Sleep(5000);

            // Finding an enemy section
            restart:;
            Console.Clear();
            audio.PlaySound(@"pre-battle.wav");
            Console.WriteLine("System::Battle -> In this area, you can choose the amout of enemies,\n"
            + " and what enemy type you want to battle with.");
            Thread.Sleep(3000);
            Console.WriteLine("System::Battle -> Let's choose how many enemies you want to battle with (Maximun of 4 - The dificulty may be higher!).");
            
            // Making the choice
            Enemies:;
            Console.Write("System::User -> ");
            string enemies = Console.ReadLine();

            // Treating the convertion from string to number (byte).
            byte finalEnemiesNumber;
            if (enemies != "")
            {
                finalEnemiesNumber = byte.Parse(enemies);
                if (!verifyingChoice(finalEnemiesNumber)) goto Enemies;
            }
            else goto Enemies;

            // Defining the player level by how many monsters are on the field. (above 1)
            if(finalEnemiesNumber > 1) player.levelUp(Convert.ToByte(finalEnemiesNumber - 1));

            // Creating enemy object
            Enemy[] enemy = new Enemy[finalEnemiesNumber];

            // Choosing monster
            Console.WriteLine("System::Battle -> Now that you've selected how many enemies you want to face,\n"
            + "lets choose their type!");
            Thread.Sleep(3000);
            Console.WriteLine("System::Battle -> For now there are just one type of monster you can select:\n"
            + "The Skeleton.");
            Thread.Sleep(3000);
            Console.WriteLine("System::Battle -> The system will automatically select it as your opponent!");

            // Choosing skeleton and creating it
            for (var i = 0; i < finalEnemiesNumber; i++)
            {
                enemy[i] = new Enemy();
                enemy[i].setName("Skeleton");

                // Filling enemies with basic stats
                enemy[i].EnemyDefaultValues(i+1, 30, 10, 9, 3, 3);
            }
            audio.StopSound();
            Thread.Sleep(2000);


            // Battle
            Console.Clear();
            audio.PlaySound(@"battle.wav");
            Console.WriteLine("System::Battle -> Let's begin the battle!\n");
            Thread.Sleep(4000);

            // UI
            Console.Clear();
            UI:;

            // Checks if the player is already dead before making his move.
            if(player.returnHp() <= 0) goto gameOver;

            // Player turn
            DrawPlayerBattleUI(finalEnemiesNumber, enemy);
            playerChoice:;
            Console.Write("Battle::{0} -> ", player.returnName());
            string choice = Console.ReadLine();

            // Treating no number and maximun number of options -> [4]
            byte finalChoice;
            if (choice != "")
            {
                finalChoice = byte.Parse(choice);
                if (!verifyingChoice(finalChoice, finalEnemiesNumber, enemy)) goto playerChoice;
                else { Console.Clear(); goto enemyTurn; }// Jumps to enemy turn!
            }
            else
            {
                Console.WriteLine("System::Battle -> Sorry, but you can't enter a blanck option. Please select a currect one.");
                Thread.Sleep(2000);
                goto playerChoice; 
            }


            // Enemy Turn
            enemyTurn:;

            // Checks if the enemy with the turn to make a move is already dead.
            for(var i = 0; i < finalEnemiesNumber; i++)
            {
                if (enemy[i].returnHp() != 0) enemyTurnHandler(enemy[i]);
                Thread.Sleep(1700);// Time between enemy turns.
            }

            // Checks if all the enemies are dead.
            switch (finalEnemiesNumber)
            {
                case 1: if (enemy[0].returnHp() == 0) goto end; break;
                case 2: if (enemy[0].returnHp() == 0 && enemy[1].returnHp() == 0) goto end; break;
                case 3: if (enemy[0].returnHp() == 0 && enemy[1].returnHp() == 0 && enemy[2].returnHp() == 0) goto end; break;
                case 4: if (enemy[0].returnHp() == 0 && enemy[1].returnHp() == 0 && enemy[2].returnHp() == 0 && enemy[3].returnHp() == 0) goto end; break;
            }
            goto UI;// return to player's turn

            // Ending game
            end:;
            Console.WriteLine("System::Game -> Congrats! You've defeated all the enemies on the field !");
            Thread.Sleep(2000);
            Console.WriteLine("System::Game -> Would you like to restart the entire game?");
            Thread.Sleep(2000);
            Console.WriteLine("System::{0} -> y - Yes, n - No : ", player.returnName());
            string option = Console.ReadLine();

            // Processing the options
            switch (option)
            {
                case "y": goto restart;
                case "n": Console.WriteLine("System::Game -> Thanks for playing the game!"); Thread.Sleep(1700); Environment.Exit(0); break;
            }

            // Game over
            gameOver:;
            Console.WriteLine("System::Game -> Too bad :(. The enemy(ies) took you down :(");
            Thread.Sleep(2000);
            Console.WriteLine("System::Game -> Would you like to restart the entire game?");
            Thread.Sleep(2000);
            Console.WriteLine("System::{0} -> y - Yes, n - No : ", player.returnName());
            string gameOverOption = Console.ReadLine();

            // Processing the options
            switch (gameOverOption)
            {
                case "y": goto restart;
                case "n": Console.WriteLine("System::Game -> Thanks for playing the game!"); Thread.Sleep(1700); Environment.Exit(0); break;
            }

        }

        /// <summary>
        /// Responsable for verifying player inputs.
        /// </summary>
        /// <param name="_stringInput">input string</param>
        /// <param name="_option">Option</param>
        /// <returns>Return positive if the validation was succeeded and false if its not succeeded.</returns>
        public bool verifyingInput(string _stringInput, byte _option)
        {
            if (_stringInput == null || _stringInput.Length == 0)
            {
                Console.WriteLine("System::Game -> Hmm.. maybe you'd pressed a button by accident?");
                Thread.Sleep(3000);
                Console.WriteLine("System::Game -> Please, try again!");
                Thread.Sleep(3000);

                // Returns to a label.
                return false;
            }
            else
            {
                // actions based on the option given to the method 
                switch (_option)
                {
                    case 1:// Apply Name
                        {
                            player.setName(_stringInput);
                            return true;
                        }

                    case 2:// Apply Class
                        {
                            player.setPlayerClass(Convert.ToByte(_stringInput));
                            return true;
                        }

                    default: return false;
                }
            }
        }

        /// <summary>
        /// Responsable to verify if the choise was empty or not valid and do things based on that.
        /// </summary>
        /// <param name="_stringInput">input string</param>
        /// <returns>Return positive if the validation was succeeded and false if its not succeeded.</returns>
        public bool verifyingChoice(byte _stringInput)
        {
            if (_stringInput > 4 || _stringInput == 0)
            {
                Console.WriteLine("System::Game -> Hmm.. maybe the choise you selected exceeds the maximun number permited.");
                Thread.Sleep(3000);
                Console.WriteLine("System::Game -> Please, try again!");
                Thread.Sleep(2000);

                // Returns false
                return false;
            }
            else return true;
        }
        public bool verifyingChoice(byte _stringInput, byte _enemiesNumber, Enemy[] _enemy)
        {
            if (_stringInput > 4 || _stringInput == 0)
            {
                Console.WriteLine("System::Game -> Hmm.. maybe the choise you selected exceeds the maximun number permited,\n"
                + "or maybe its not listed in the action list.");
                Thread.Sleep(3000);
                Console.WriteLine("System::Game -> Please, try again!");
                Thread.Sleep(2000);

                // Returns false
                return false;
            }
            else if (_enemiesNumber != 0)// Battle mode
            {
                // If the player is defending himself already, return to normal.
                // This makes the player defends himself for just one turn!
                if (player.returnDefending()) player.setDefending(false);

                // Reading options
                switch (_stringInput)
                {
                    case 1:// Attack the enemy
                    {
                        if (_enemiesNumber > 1)
                        {
                            Console.WriteLine("System::Battle -> Which enemy do you want to attack?");
                            Thread.Sleep(1000);

                            _chooseEnemy:;
                            Console.Write("Battle::{0} -> ", player.returnName());
                            byte _choice = byte.Parse(Console.ReadLine());

                            // Verify if the choosen enemy is already dead.
                            if (_enemy[_choice - 1].returnHp() == 0)
                            {
                                Console.WriteLine("System::Battle -> Sorry, but the enemy you're trying to attack is already dead, Please choose another one!");
                                Thread.Sleep(2000);
                                goto _chooseEnemy;
                            }
                            else
                            {
                                // Deals damage to the choosen enemy
                                player.AttackEnemy(_enemy, _choice - 1);

                                // Log
                                Console.WriteLine("System::Battle -> Player {0} deals {1} of damage to the enemy {2}!", player.returnName(),
                                player.returnDamage(_enemy, _choice - 1), _enemy[_choice - 1].returnName());

                                Thread.Sleep(2000);
                                return true;
                            }
                        }
                        else
                        {
                            // If theres only one enemy, Attack him!
                            player.AttackEnemy(_enemy, 0);

                            // Log
                            Console.WriteLine("System::Battle -> Player {0} deals {1} of damage to the enemy {2}!", player.returnName(),
                            player.returnDamage(_enemy, 0), _enemy[0].returnName());
                            
                            Thread.Sleep(2000);
                            return true;
                        }
                    }

                    case 2:// Skills [WIP]
                    {
                        Console.WriteLine("System::Battle -> Sorry, but this option is under maintenance for now, please choose another one!");
                        Thread.Sleep(2000);
                        return true;
                    }

                    case 3: player.Defend(); Thread.Sleep(2000); return true;// Player defends himself

                    case 4:// Flee
                    {
                        // Getting random enemy index for flee probability
                        Random r = new Random();
                        byte enemyIndex;
                        switch(_enemy.Length)
                        {
                            case 1: enemyIndex = 1; break;
                            case 2: enemyIndex = Convert.ToByte(r.Next(0,1)); break;
                            case 3: enemyIndex = Convert.ToByte(r.Next(0,2)); break;
                            case 4: enemyIndex = Convert.ToByte(r.Next(0,3)); break;
                            default: enemyIndex = 0; break;
                        } 

                        
                        // Probability of 95%
                        if(player.returnAgility() > _enemy[enemyIndex].returnAgility())
                        {
                            byte flee = Convert.ToByte(r.Next(0, 100));
                            if (flee > 5)
                            {
                                Console.WriteLine("System::Battle -> You've fleed from the battle!");
                                Thread.Sleep(2000);
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("System::Battle -> You couldn't escape from the battle..");
                                Thread.Sleep(2000);
                                return false;
                            }
                        }
                        else
                        {
                            // 40% of flee rate.
                            byte flee = Convert.ToByte(r.Next(0, 100));
                            if(flee > 60)
                            {
                                return true;
                            }else return false;
                        }
                    }

                    default: return false;
                }
            }
            else return false;
        }


        /// <summary>
        /// Used to print the battle UI.
        /// </summary>
        /// <param name="finalNumber">Number of enemies to display</param>
        /// <param name="enemy">Enemy object</param>
        public void DrawPlayerBattleUI(byte finalNumber, Enemy[] enemy)
        {
            switch (finalNumber)
            {
                case 1:
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                  RPG BATTLE SYSTEM!                      --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--   1. {0}                                            --", enemy[0].returnName());
                    Console.WriteLine("--   hp: {0}/{1}                                            --", enemy[0].returnHp(), enemy[0].returnMaxHp());
                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--  * What do you want to do now?                           --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("         {0} - HP: {1}/{2} - SP: {3}/{4}           ", player.returnName(), player.returnHp(), player.returnMaxHp(),
                    player.returnSp(), player.returnMaxSp());

                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--      1. Attack   2. Skills   3. Defend   4. Flee         --");
                    Console.WriteLine("--------------------------------------------------------------");
                    break;
                }

                case 2:
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                  RPG BATTLE SYSTEM!                      --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--   1. {0}  2. {1}                               --", enemy[0].returnName(), enemy[1].returnName());
                    Console.WriteLine("--   hp: {0}/{1}     hp: {2}/{3}                                --", enemy[0].returnHp(), enemy[0].returnMaxHp(),
                    enemy[1].returnHp(), enemy[1].returnMaxHp());

                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--  * What do you want to do now?                           --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("         {0} - HP: {1}/{2} - SP: {3}/{4}           ", player.returnName(), player.returnHp(), player.returnMaxHp(),
                    player.returnSp(), player.returnMaxSp());

                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--      1. Attack   2. Skills   3. Defend   4. Flee         --");
                    Console.WriteLine("--------------------------------------------------------------");
                    break;
                }

                case 3:
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                  RPG BATTLE SYSTEM!                      --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--   1. {0}  2. {1}  3. {2}                  --", enemy[0].returnName(), enemy[1].returnName(), enemy[2].returnName());
                    Console.WriteLine("--   hp: {0}/{1}  hp: {2}/{3}  hp: {4}/{5}                           --",
                    enemy[0].returnHp(), enemy[0].returnMaxHp(), enemy[1].returnHp(), enemy[1].returnMaxHp(), enemy[2].returnHp(), enemy[2].returnMaxHp());
                    
                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--  * What do you want to do now?                           --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("         {0} - HP: {1}/{2} - SP: {3}/{4}           ", player.returnName(), player.returnHp(), player.returnMaxHp(),
                    player.returnSp(), player.returnMaxSp());

                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--      1. Attack   2. Skills   3. Defend   4. Flee         --");
                    Console.WriteLine("--------------------------------------------------------------");
                    break;
                }

                case 4:
                {
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                  RPG BATTLE SYSTEM!                      --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--   1. {0}  2. {1}  3. {2}  4. {3}     --", enemy[0].returnName(), enemy[1].returnName(), enemy[2].returnName(), enemy[3].returnName());
                    Console.WriteLine("--   hp: {0}/{1}  hp: {2}/{3}  hp: {4}/{5}  hp: {6}/{7}                  --",
                    enemy[0].returnHp(), enemy[0].returnMaxHp(), enemy[1].returnHp(), enemy[1].returnMaxHp(), enemy[2].returnHp(), enemy[2].returnMaxHp(),
                    enemy[3].returnHp(), enemy[3].returnMaxHp());

                    Console.WriteLine("--                                                          --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--  * What do you want to do now?                           --");
                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("         {0} - HP: {1}/{2} - SP: {3}/{4}           ", player.returnName(), player.returnHp(), player.returnMaxHp(),
                    player.returnSp(), player.returnMaxSp());

                    Console.WriteLine("--------------------------------------------------------------");
                    Console.WriteLine("--      1. Attack   2. Skills   3. Defend   4. Flee         --");
                    Console.WriteLine("--------------------------------------------------------------");
                    break;
                }

                default: break;// To ensure that there will be no option accepted above 4.
            }
        }

        /// <summary>
        /// Enemy battle handler.
        /// </summary>
        /// <param name="_enemiesNumber">Number of enemies</param>
        /// <param name="_enemy">Enemy to make the move</param>
        public void enemyTurnHandler(Enemy _enemy)
        {
            // Random
            Random r = new Random();

            // Enemy decides what he will do based on their hp.
            // Depending on their hp number, the probability of take a defending position increase.

            // Resets the defense action after 1 turn if he was already defending before.
            if (_enemy.returnDefending()) _enemy.setDefending(false);

            // If the enemy's hp is under 20% he will start to defend himself a little more offtem
            if (_enemy.returnHp() < _enemy.returnMaxHp() * (20 / 100))
            {
                // 60% of prob of defending himself
                byte probability = Convert.ToByte(r.Next(0, 100));
                if (probability > 40)
                {
                    _enemy.setDefending(true);
                    Console.WriteLine("Sytem::Battle -> Enemy {0} defended himself", _enemy.returnName());
                }
                else
                {
                    _enemy.AttackEnemy(player);
                    Console.WriteLine("Sytem::Battle -> Enemy {0} attacks the player {1}, dealing {2} of damage!",
                    _enemy.returnName(), player.returnName(), _enemy.returnDamage(player));
                }
            }
            else
            {
                // 39% of prob of defending himself
                byte probability = Convert.ToByte(r.Next(0, 100));
                if (probability > 61)
                {
                    _enemy.setDefending(true);
                    Console.WriteLine("Sytem::Battle -> Enemy {0} defended himself", _enemy.returnName());
                }
                else
                {
                    _enemy.AttackEnemy(player);
                    Console.WriteLine("Sytem::Battle -> Enemy {0} attacks the player {1}, dealing {2} of damage!",
                    _enemy.returnName(), player.returnName(), _enemy.returnDamage(player));
                }
            }
        }
    }
}


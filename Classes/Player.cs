using System;

namespace TurnBased_RPG_Battle_System.Classes
{
    public class Player
    {
        // Variables
        private string Name { get; set; }
        private byte PlayerClass { get; set; }
        private byte PlayerLevel { get; set; }
        private byte Hp { get; set; }
        private byte MaxHp { get; set; }
        private byte Sp { get; set; }
        private byte MaxSp { get; set; }
        private byte Power { get; set; }
        private byte Defense { get; set; }
        private byte Agility { get; set; }
        private bool IsDefending { get; set; }

        // Skill matrix
        private string[,] SkillList { get; set; }
        private byte[,] SkillDamage { get; set; }


        /// <summary>
        /// Responsable for giving the player the basic values.
        /// </summary>
        /// <param name="hp">HP</param>
        /// <param name="sp">SP</param>
        /// <param name="power">Player Power (Multiplier)</param>
        /// <param name="defense">Player defense</param>
        /// <param name="agility">Player agility</param>
        public void PlayerDefaultValues(byte hp, byte lvl,byte sp, byte power, byte defense, byte agility)
        {
            PlayerLevel = lvl;
            setHp(hp);
            setMaxHp(hp);
            setSp(sp);
            setMaxSp(sp);
            setPower(power);
            setDefense(defense);
            setAgility(agility);

            // Initiating skills
            //InitiateSkills();
        }

        /// <summary>
        /// Applies the damage to the enemy.
        /// </summary>
        /// <param name="_enemy">Enemy target.</param>
        /// <param name="_index">Enemy index on the matrix</param>
        public void AttackEnemy(Enemy[] _enemy, int _index)
        {
            // Calculates the final damage based on the defended trigger
            if (_enemy[_index].returnDefending())
            {
                // Checks if the resulting damage would not bypass the acutal hp number
                if (Convert.ToByte(returnPower() - (_enemy[_index].returnDefense() * 2)) < _enemy[_index].returnHp())
                    _enemy[_index].setHp(Convert.ToByte(_enemy[_index].returnHp() - (returnPower() - (_enemy[_index].returnDefense() * 2))));
                else _enemy[_index].setHp(0);
            }
            else
            { 
                if(Convert.ToByte(returnPower() - _enemy[_index].returnDefense()) < _enemy[_index].returnHp())
                    _enemy[_index].setHp(Convert.ToByte(_enemy[_index].returnHp() - (returnPower() - _enemy[_index].returnDefense())));
                else _enemy[_index].setHp(0);
            }
        }

        /// <summary>
        /// Responsable to calc the demage dealed to the enemy
        /// </summary>
        /// <param name="_enemy"></param>
        /// <returns>Return the damage dealed to the enemy</returns>
        public byte returnDamage(Enemy[] _enemy, int _index)
        {
            // Calculates the final damage based on the defended trigger
            if (_enemy[_index].returnDefending())
            {
                return Convert.ToByte(returnPower() - (_enemy[_index].returnDefense() * 2));
            }
            else return Convert.ToByte(returnPower() - _enemy[_index].returnDefense());
        }

        /// <summary>
        /// Defends the player from enemies attacks
        /// </summary>
        public void Defend()
        {
            setDefending(true);
            Console.WriteLine("System -> Player {0} defended himself!", returnName());
        }

        /// <summary>
        /// Initiates player skills [UNDER TESTS]
        /// </summary>
        public void InitiateSkills()
        {
            // Skill name
            this.SkillList = new string[2, 1]{ {"Slash"},{"Triple Slash"} };

            // Skill Damages [Multiplier]
            this.SkillDamage = new byte[2, 1]{ {2},{3} };
        }

        /// <summary>
        /// Level up the player.
        /// </summary>
        /// <param name="_levels">How many levels you want to level him up</param>
        public void levelUp(byte _levels)
        {
            // Random object to randomize the player stats.
            Random random = new Random();

            // Looping for more than 1 level up.
            for (var i = 1; i <= _levels; i++)
            {
                // Player level
                PlayerLevel = Convert.ToByte(PlayerLevel + i);

                // HP
                byte _lvlUpStatsRange_Hp = Convert.ToByte(returnMaxHp() + random.Next(12, 28));
                setHp(_lvlUpStatsRange_Hp);
                setMaxHp(returnHp());

                // SP
                byte _lvlUpStatsRange_Sp = Convert.ToByte(returnMaxSp() + random.Next(6, 10));
                setSp(_lvlUpStatsRange_Sp);
                setMaxSp(returnSp());

                //Power defende and agility
                setPower(Convert.ToByte(returnPower() + Convert.ToByte(random.Next(2, 5))));
                setDefense(Convert.ToByte(returnDefense() + Convert.ToByte(random.Next(2, 4))));
                setAgility(Convert.ToByte(returnAgility() + Convert.ToByte(random.Next(1, 3))));
            }
        }

        #region Encapsulating_Variables
        /// <summary>
        /// Return the players name.
        /// </summary>
        /// <returns></returns>
        public string returnName() { return this.Name; }

        /// <summary>
        /// Return player class
        /// </summary>
        /// <returns></returns>
        public byte returnPlayerClass() { return this.PlayerClass; }

        /// <summary>
        /// Returns players actual Hp.
        /// </summary>
        /// <returns></returns>
        public byte returnHp() { return this.Hp; }

        /// <summary>
        /// Returns players maximun Hp.
        /// </summary>
        /// <returns></returns>
        public byte returnMaxHp() { return this.MaxHp; }

        /// <summary>
        /// Returns players Sp
        /// </summary>
        /// <returns></returns>
        public byte returnSp() { return this.Sp; }

        /// <summary>
        /// Returns players maximun Sp
        /// </summary>
        /// <returns></returns>
        public byte returnMaxSp() { return this.MaxSp; }

        /// <summary>
        /// Returns players power to deal damage.
        /// </summary>
        /// <returns></returns>
        public byte returnPower() { return this.Power; }

        /// <summary>
        /// Returns players defense
        /// </summary>
        /// <returns></returns>
        public byte returnDefense() { return this.Defense; }

        /// <summary>
        /// Returns players Agility
        /// </summary>
        /// <returns></returns>
        public byte returnAgility() { return this.Agility; }

        /// <summary>
        /// Returns if player is defending or not
        /// </summary>
        /// <returns></returns>
        public bool returnDefending() { return this.IsDefending; }

        /// <summary>
        /// Sets a class to the player
        /// </summary>
        /// <param name="_playerClass">Name for the player</param>
        public void setPlayerClass(byte _playerClass) { this.PlayerClass = _playerClass; }

        /// <summary>
        /// Sets a player name.
        /// </summary>
        /// <param name="_name">Name for the player</param>
        public void setName(string _name) { this.Name = _name; }

        /// <summary>
        /// Sets the actual hp for the player [damage calc phase.]
        /// </summary>
        /// <param name="_hp">Actual hp</param>
        public void setHp(byte _hp) { this.Hp = _hp; }

        /// <summary>
        /// Sets the maximun hp for the player.
        /// </summary>
        /// <param name="_maxHp">Maximun Hp</param>
        public void setMaxHp(byte _maxHp) { this.MaxHp = _maxHp; }

        /// <summary>
        /// Sets player actual sp.
        /// </summary>
        /// <param name="_sp">Actual Sp</param>
        public void setSp(byte _sp) { this.Sp = _sp; }

        /// <summary>
        /// Sets the maximun sp for the player.
        /// </summary>
        /// <param name="_maxSp">Maximun Sp</param>
        public void setMaxSp(byte _maxSp) { this.MaxSp = _maxSp; }

        /// <summary>
        /// Sets the power to the player.
        /// </summary>
        /// <param name="_power">Player power to be given</param>
        public void setPower(byte _power) { this.Power = _power; }

        /// <summary>
        /// Sets the defense for the player.
        /// </summary>
        /// <param name="_defense">Player defense to be given</param>
        public void setDefense(byte _defense) { this.Defense = _defense; }

        /// <summary>
        /// Sets the agility for the player.
        /// </summary>
        /// <param name="_agility">Player agility to be given</param>
        public void setAgility(byte _agility) { this.Agility = _agility; }

        /// <summary>
        /// Sets the IsDefending? trigger of the player 
        /// </summary>
        /// <param name="_defending"></param>
        public void setDefending(bool _defending) { IsDefending = _defending; }
        #endregion
    }
}

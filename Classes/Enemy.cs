using System;

namespace TurnBased_RPG_Battle_System.Classes
{
    public class Enemy
    {
        // Variables
        private string Name { get; set; }
        public int ID { get; set; }
        private byte EnemyClass { get; set; }
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


        /// <summary>
        /// Responsable for giving the enemy the basic values.
        /// </summary>
        /// <param name="hp">HP</param>
        /// <param name="sp">SP</param>
        /// <param name="power">Enemy Power (Multiplier)</param>
        /// <param name="defense">Enemy defense</param>
        /// <param name="agility">Enemy agility</param>
        public void EnemyDefaultValues(int id, byte hp, byte sp, byte power, byte defense, byte agility)
        {
            ID = id;
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
        public void AttackEnemy(Player _enemy)
        {
            // Calculates the final damage based on the defended trigger
            if (_enemy.returnDefending())
            {
                // Checks if the resulting damage would not bypass the acutal hp number
                if (Convert.ToByte(returnPower() - (_enemy.returnDefense() * 2)) < _enemy.returnHp())
                    _enemy.setHp(Convert.ToByte(_enemy.returnHp() - (returnPower() - (_enemy.returnDefense() * 2))));
                else _enemy.setHp(0);
            }
            else
            {
                if (Convert.ToByte(returnPower() - _enemy.returnDefense()) < _enemy.returnHp())
                    _enemy.setHp(Convert.ToByte(_enemy.returnHp() - (returnPower() - _enemy.returnDefense())));
                else _enemy.setHp(0);
            }
        }

        /// <summary>
        /// Responsable to calc the demage dealed to the enemy [LOG PURPOSES]
        /// </summary>
        /// <param name="_enemy"></param>
        /// <returns>Return the damage dealed to the enemy</returns>
        public byte returnDamage(Player _enemy)
        {
            // Calculates the final damage based on the defended trigger
            if (_enemy.returnDefending())
            {
                return Convert.ToByte(returnPower() - (_enemy.returnDefense() * 2));
            }
            else return Convert.ToByte(returnPower() - _enemy.returnDefense());
        }

        /// <summary>
        /// Defends the enemy from players attacks
        /// </summary>
        public void Defend()
        {
            setDefending(true);
            Console.WriteLine("System -> Enemy {0} defended himself!", returnName());
        }

        /// <summary>
        /// Initiates enemy skills [UNDER TESTS]
        /// </summary>
        public void InitiateSkills()
        {
            // Skill name
            SkillList = new string[2, 2] 
            { 
                { 
                   "Crush",
                    "1"
                },
                { 
                    "Scratch",
                    "2"
                }
            };
        }

        #region Encapsulating_Variables
        /// <summary>
        /// Return the enemy name.
        /// </summary>
        /// <returns></returns>
        public string returnName() { return Name; }

        /// <summary>
        /// Return player class
        /// </summary>
        /// <returns></returns>
        public byte returnPlayerClass() { return EnemyClass; }

        /// <summary>
        /// Returns enemy actual Hp.
        /// </summary>
        /// <returns></returns>
        public byte returnHp() { return Hp; }

        /// <summary>
        /// Returns enemy maximun Hp.
        /// </summary>
        /// <returns></returns>
        public byte returnMaxHp() { return MaxHp; }

        /// <summary>
        /// Returns enemy Sp
        /// </summary>
        /// <returns></returns>
        public byte returnSp() { return Sp; }

        /// <summary>
        /// Returns enemy maximun Sp
        /// </summary>
        /// <returns></returns>
        public byte returnMaxSp() { return MaxSp; }

        /// <summary>
        /// Returns enemy power to deal damage.
        /// </summary>
        /// <returns></returns>
        public byte returnPower() { return Power; }

        /// <summary>
        /// Returns enemy defense
        /// </summary>
        /// <returns></returns>
        public byte returnDefense() { return Defense; }

        /// <summary>
        /// Returns enemy Agility
        /// </summary>
        /// <returns></returns>
        public byte returnAgility() { return Agility; }

        /// <summary>
        /// Returns if player is defending or not
        /// </summary>
        /// <returns></returns>
        public bool returnDefending() { return this.IsDefending; }

        /// <summary>
        /// Sets a class to the enemy
        /// </summary>
        /// <param name="_enemyClass">Name for the enemy</param>
        public void setEnemyClass(byte _enemyClass) { EnemyClass = _enemyClass; }

        /// <summary>
        /// Sets a enemy name.
        /// </summary>
        /// <param name="_name">Name for the enemy</param>
        public void setName(string _name) { Name = _name; }

        /// <summary>
        /// Sets the actual hp for the enemy [damage calc phase.]
        /// </summary>
        /// <param name="_hp">Actual hp</param>
        public void setHp(byte _hp) { Hp = _hp; }

        /// <summary>
        /// Sets the maximun hp for the enemy.
        /// </summary>
        /// <param name="_maxHp">Maximun Hp</param>
        public void setMaxHp(byte _maxHp) { MaxHp = _maxHp; }

        /// <summary>
        /// Sets enemy actual sp.
        /// </summary>
        /// <param name="_sp">Actual Sp</param>
        public void setSp(byte _sp) { Sp = _sp; }

        /// <summary>
        /// Sets the maximun sp for the enemy.
        /// </summary>
        /// <param name="_maxSp">Maximun Sp</param>
        public void setMaxSp(byte _maxSp) { MaxSp = _maxSp; }

        /// <summary>
        /// Sets the power to the enemy.
        /// </summary>
        /// <param name="_power">enemy power to be given</param>
        public void setPower(byte _power) { Power = _power; }

        /// <summary>
        /// Sets the defense for the enemy.
        /// </summary>
        /// <param name="_defense">enemy defense to be given</param>
        public void setDefense(byte _defense) { Defense = _defense; }

        /// <summary>
        /// Sets the agility for the enemy.
        /// </summary>
        /// <param name="_agility">enemy agility to be given</param>
        public void setAgility(byte _agility) { Agility = _agility; }

        /// <summary>
        /// Sets the IsDefending? trigger of the enemy 
        /// </summary>
        /// <param name="_defending"></param>
        public void setDefending(bool _defending) { IsDefending = _defending; }
        #endregion
    }
}

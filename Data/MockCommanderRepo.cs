using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {

        public Command GetCommandById(int id)
        {
            return new Command{Id=0, HowTo="Boil", Line="Linea", Platform="Plata"};
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil", Line="Linea",Platform="Plata" },
                new Command{Id=1, HowTo="Uno", Line="Linea1",Platform="Plataform" },
                new Command{Id=2, HowTo="Dos", Line="Linea2",Platform="Plata" },
                new Command{Id=3, HowTo="Tres", Line="Linea3",Platform="Plat" },
            };

            return commands;
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}
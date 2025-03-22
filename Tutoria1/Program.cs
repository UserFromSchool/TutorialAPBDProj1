namespace Tutoria1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Container creation
            Console.WriteLine("Creating containers...");
            var containerLiquid = new LiquidContainer(100, 10, 200, 50);
            var containerRefr = new RefrigeratedContainer(100, 10, 200, 50,
                "Banana", 10.5);
            var containerGas = new GasContainer(100, 10, 200, 70);
            Console.WriteLine(containerLiquid);
            Console.WriteLine(containerRefr);
            Console.WriteLine(containerGas);
            
            // Liquid container actions
            Console.WriteLine("Liquid container actions...");
            containerGas.LoadCargo(22.4);
            containerLiquid.LoadCargo(25.4, true);
            containerLiquid.EmptyCargo();
            containerLiquid.LoadCargo(49.5, false);
            containerLiquid.EmptyCargo();
            containerLiquid.LoadCargo(22.3, true);
            Console.WriteLine();
            
            // Gas container actions
            Console.WriteLine("Gas container actions...");
            var load = 25.4;
            Console.WriteLine($"Loading {load} cargo...");
            containerGas.LoadCargo(load);
            containerGas.EmptyCargo();
            Console.WriteLine($"After emptying left is {containerGas.CargoMass}");
            Console.WriteLine();
            containerGas.LoadCargo(10.3);
            
            // Product container actions
            Console.WriteLine("Refrigerated container actions...");
            try
            {
                containerRefr.LoadCargo(40.4, "Apple");  // Incorrect cargo
            }
            catch (KeyNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
            containerRefr.LoadCargo(40.4, "Banana");
            Console.WriteLine();
            
            // Creating a ship
            Console.WriteLine("Ship actions...");
            var ship = new Ship(100, 2, 50);
            var containers = new List<Container>();
            Console.WriteLine(ship);
            Console.WriteLine("Adding list of containers...");
            containers.Add(containerLiquid);
            containers.Add(containerGas);
            ship.LoadContainers(containers);
            Console.WriteLine(ship);
            Console.WriteLine("Replacing container...");
            try
            {
                ship.ReplaceContainer(containerRefr, containerLiquid.GetSerialNumber());
            }
            catch (Exception exception)  // After replacement cargo is too big so it is aborted
            {
                Console.WriteLine(exception.Message);
            }
            Console.WriteLine(ship);
            Console.WriteLine("Removing container...");
            ship.RemoveContainer(containerLiquid.GetSerialNumber());  // Since replacement was aborted, we have to remove
            Console.WriteLine(ship);
            Console.WriteLine("Transferring container container...");
            var ship2 = new Ship(99, 2, 50);
            Console.WriteLine(ship2);
            Console.WriteLine(ship);
            ship.TransferContainer(containerGas.GetSerialNumber(), ship2);
            Console.WriteLine(ship2);
            Console.WriteLine(ship);
            Console.WriteLine("Replacing container...");
            Console.WriteLine(ship2);
            ship2.ReplaceContainer(containerRefr, containerGas.GetSerialNumber());
            Console.WriteLine(ship2);
        }
    }
}
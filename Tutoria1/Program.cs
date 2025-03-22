namespace Tutoria1
{
    public class Program
    {
        private static List<Ship> _ships = new List<Ship>();
        private static List<Container> _availableContainers = new List<Container>();
        
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

            RunContainerManagementSystem();
        }

        private static void RunContainerManagementSystem()
        {
            bool running = true;
            
            while (running)
            {
                Console.Clear();
                DisplayMenu();
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                switch (keyInfo.KeyChar)
                {
                    case '1':
                        AddShip();
                        break;
                    case '2':
                        RemoveShip();
                        break;
                    case '3':
                        AddContainerToShip();
                        break;
                    case '4':
                        RemoveContainerFromShip();
                        break;
                    case '5':
                        TransferContainerBetweenShips();
                        break;
                    case '6':
                        DisplayAllShips();
                        break;
                    case '7':
                        CreateNewContainer();
                        break;
                    case '8':
                        RemoveAvailableContainer();
                        break;
                    case '0':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
            
            Console.WriteLine("Thank you for using the Container Management System!");
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("======= Container Management System =======");
            Console.WriteLine("1. Add a new ship");
            Console.WriteLine("2. Remove a ship");
            Console.WriteLine("3. Add a container to a ship");
            Console.WriteLine("4. Remove a container from a ship");
            Console.WriteLine("5. Transfer a container between ships");
            Console.WriteLine("6. Display all ships");
            Console.WriteLine("7. Create a new container");
            Console.WriteLine("8. Remove a container");
            Console.WriteLine("0. Exit");
            Console.WriteLine("==========================================");
            Console.Write("Please select an option: ");
        }

        private static void AddShip()
        {
            Console.Clear();
            Console.WriteLine("======= Add a New Ship =======");
            
            try
            {
                Console.Write("Enter max speed: ");
                double maxSpeed = Convert.ToDouble(Console.ReadLine());
                
                Console.Write("Enter max containers: ");
                int maxContainers = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("Enter max weight: ");
                double maxWeight = Convert.ToDouble(Console.ReadLine());
                
                Ship newShip = new Ship(maxSpeed, maxContainers, maxWeight);
                _ships.Add(newShip);
                
                Console.WriteLine("Ship added successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Invalid input format. Please enter numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void RemoveShip()
        {
            Console.Clear();
            Console.WriteLine("======= Remove a Ship =======");
            
            if (_ships.Count == 0)
            {
                Console.WriteLine("There are no ships to remove.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            for (int i = 0; i < _ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Ship with {_ships[i].MaxContainers} max containers and {_ships[i].MaxWeight} max weight");
            }
            
            try
            {
                Console.Write("Enter the number of the ship to remove: ");
                int shipIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (shipIndex >= 0 && shipIndex < _ships.Count)
                {
                    _ships.RemoveAt(shipIndex);
                    Console.WriteLine("Ship removed successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid ship number.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void AddContainerToShip()
        {
            Console.Clear();
            Console.WriteLine("======= Add Container to Ship =======");
            
            if (_ships.Count == 0)
            {
                Console.WriteLine("There are no ships available.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            if (_availableContainers.Count == 0)
            {
                Console.WriteLine("There are no available containers.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Available Ships:");
            for (int i = 0; i < _ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Ship with {_ships[i].MaxContainers} max containers and {_ships[i].MaxWeight} max weight");
            }
            
            try
            {
                Console.Write("Select a ship (number): ");
                int shipIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (shipIndex < 0 || shipIndex >= _ships.Count)
                {
                    Console.WriteLine("Invalid ship selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                Console.WriteLine("\nAvailable Containers:");
                for (int i = 0; i < _availableContainers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_availableContainers[i].GetSerialNumber()} (Cargo Mass: {_availableContainers[i].CargoMass})");
                }
                
                Console.Write("Select a container (number): ");
                int containerIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (containerIndex < 0 || containerIndex >= _availableContainers.Count)
                {
                    Console.WriteLine("Invalid container selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                Container selectedContainer = _availableContainers[containerIndex];
                _ships[shipIndex].LoadContainer(selectedContainer);
                _availableContainers.RemoveAt(containerIndex);
                
                Console.WriteLine("Container added to ship successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void RemoveContainerFromShip()
        {
            Console.Clear();
            Console.WriteLine("======= Remove Container from Ship =======");
            
            if (_ships.Count == 0)
            {
                Console.WriteLine("There are no ships available.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Available Ships:");
            for (int i = 0; i < _ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Ship with {_ships[i].MaxContainers} max containers and {_ships[i].MaxWeight} max weight");
            }
            
            try
            {
                Console.Write("Select a ship (number): ");
                int shipIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (shipIndex < 0 || shipIndex >= _ships.Count)
                {
                    Console.WriteLine("Invalid ship selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                var containerSerialNumbers = _ships[shipIndex].GetContainersSerialNumbers();
                
                if (containerSerialNumbers.Count == 0)
                {
                    Console.WriteLine("This ship has no containers.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                Console.WriteLine("\nContainers on this ship:");
                for (int i = 0; i < containerSerialNumbers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {containerSerialNumbers[i]}");
                }
                
                Console.Write("Select a container to remove (number): ");
                int containerIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (containerIndex < 0 || containerIndex >= containerSerialNumbers.Count)
                {
                    Console.WriteLine("Invalid container selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                string serialNumber = containerSerialNumbers[containerIndex];
                Container removedContainer = _ships[shipIndex].RemoveContainer(serialNumber);
                _availableContainers.Add(removedContainer);
                
                Console.WriteLine($"Container {serialNumber} removed from ship and added to available containers.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void TransferContainerBetweenShips()
        {
            Console.Clear();
            Console.WriteLine("======= Transfer Container Between Ships =======");
            
            if (_ships.Count < 2)
            {
                Console.WriteLine("You need at least two ships to transfer containers.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Source Ships:");
            for (int i = 0; i < _ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Ship with {_ships[i].MaxContainers} max containers and {_ships[i].MaxWeight} max weight");
            }
            
            try
            {
                Console.Write("Select source ship (number): ");
                int sourceShipIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (sourceShipIndex < 0 || sourceShipIndex >= _ships.Count)
                {
                    Console.WriteLine("Invalid ship selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                var containerSerialNumbers = _ships[sourceShipIndex].GetContainersSerialNumbers();
                
                if (containerSerialNumbers.Count == 0)
                {
                    Console.WriteLine("The source ship has no containers to transfer.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                Console.WriteLine("\nContainers on the source ship:");
                for (int i = 0; i < containerSerialNumbers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {containerSerialNumbers[i]}");
                }
                
                Console.Write("Select a container to transfer (number): ");
                int containerIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (containerIndex < 0 || containerIndex >= containerSerialNumbers.Count)
                {
                    Console.WriteLine("Invalid container selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                Console.WriteLine("\nDestination Ships:");
                List<int> destinationShipIndices = new List<int>();
                int displayIndex = 1;
                
                for (int i = 0; i < _ships.Count; i++)
                {
                    if (i != sourceShipIndex)
                    {
                        Console.WriteLine($"{displayIndex}. Ship with {_ships[i].MaxContainers} max containers and {_ships[i].MaxWeight} max weight");
                        destinationShipIndices.Add(i);
                        displayIndex++;
                    }
                }
                
                Console.Write("Select destination ship (number): ");
                int destinationSelectionIndex = Convert.ToInt32(Console.ReadLine()) - 1;
                
                if (destinationSelectionIndex < 0 || destinationSelectionIndex >= destinationShipIndices.Count)
                {
                    Console.WriteLine("Invalid destination ship selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
                
                int destinationShipIndex = destinationShipIndices[destinationSelectionIndex];
                
                string serialNumber = containerSerialNumbers[containerIndex];
                _ships[sourceShipIndex].TransferContainer(serialNumber, _ships[destinationShipIndex]);
                
                Console.WriteLine($"Container {serialNumber} transferred successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void DisplayAllShips()
        {
            Console.Clear();
            Console.WriteLine("======= All Ships =======");
            
            if (_ships.Count == 0)
            {
                Console.WriteLine("There are no ships to display.");
            }
            else
            {
                for (int i = 0; i < _ships.Count; i++)
                {
                    Console.WriteLine($"Ship #{i + 1}:");
                    Console.WriteLine(_ships[i]);
                    Console.WriteLine();
                }
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        
        private static void CreateNewContainer()
        {
            Console.Clear();
            Console.WriteLine("======= Create a New Container =======");
            Console.WriteLine("Select container type:");
            Console.WriteLine("1. Liquid Container");
            Console.WriteLine("2. Gas Container");
            Console.WriteLine("3. Refrigerated Container");
            
            try
            {
                int containerTypeChoice = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("Enter height (cm): ");
                double height = Convert.ToDouble(Console.ReadLine());
                
                Console.Write("Enter tare weight (kg): ");
                double tareWeight = Convert.ToDouble(Console.ReadLine());
                
                Console.Write("Enter depth (cm): ");
                double depth = Convert.ToDouble(Console.ReadLine());
                
                Console.Write("Enter max payload (kg): ");
                double maxPayload = Convert.ToDouble(Console.ReadLine());
                
                Container newContainer;
                switch (containerTypeChoice)
                {
                    case 1:
                        newContainer = new LiquidContainer(height, tareWeight, depth, maxPayload);
                        break;
                        
                    case 2:
                        newContainer = new GasContainer(height, tareWeight, depth, maxPayload);
                        break;
                        
                    case 3:
                        Console.Write("Enter product name: ");
                        string product = Console.ReadLine();
                        
                        Console.Write("Enter temperature: ");
                        double temperature = Convert.ToDouble(Console.ReadLine());
                        
                        newContainer = new RefrigeratedContainer(height, tareWeight, depth, maxPayload, product, temperature);
                        break;
                        
                    default:
                        Console.WriteLine("Invalid container type selection.");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        return;
                }
                
                Console.Write("Do you want to load cargo? (Y/N): ");
                string loadCargoChoice = Console.ReadLine().Trim().ToUpper();
                
                if (loadCargoChoice == "Y")
                {
                    Console.Write("Enter cargo mass (kg): ");
                    double cargoMass = Convert.ToDouble(Console.ReadLine());
                    
                    if (newContainer is LiquidContainer liquidContainer)
                    {
                        Console.Write("Is the cargo hazardous? (Y/N): ");
                        string isHazardous = Console.ReadLine().Trim().ToUpper();
                        liquidContainer.LoadCargo(cargoMass, isHazardous == "Y");
                    }
                    else if (newContainer is RefrigeratedContainer refrigeratedContainer)
                    {
                        refrigeratedContainer.LoadCargo(cargoMass, ((RefrigeratedContainer)newContainer).Product);
                    }
                    else
                    {
                        newContainer.LoadCargo(cargoMass);
                    }
                }
                
                _availableContainers.Add(newContainer);
                Console.WriteLine($"Container {newContainer.GetSerialNumber()} created and added to available containers.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter valid numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        
        private static void RemoveAvailableContainer()
        {
            Console.Clear();
            Console.WriteLine("======= Remove Available Container =======");
    
            if (_availableContainers.Count == 0)
            {
                Console.WriteLine("There are no available containers to remove.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Available Containers:");
            for (int i = 0; i < _availableContainers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_availableContainers[i].GetSerialNumber()} (Cargo Mass: {_availableContainers[i].CargoMass})");
            }
    
            try
            {
                Console.Write("Select a container to remove (number): ");
                int containerIndex = Convert.ToInt32(Console.ReadLine()) - 1;
        
                if (containerIndex < 0 || containerIndex >= _availableContainers.Count)
                {
                    Console.WriteLine("Invalid container selection.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
        
                string removedContainerSerial = _availableContainers[containerIndex].GetSerialNumber();
                _availableContainers.RemoveAt(containerIndex);
        
                Console.WriteLine($"Container {removedContainerSerial} removed from available containers.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
    
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
namespace Tutoria1;

public class Ship
{

   private List<Container> Containers;
   public readonly double MaxSpeed;
   public readonly int MaxContainers;
   public readonly double MaxWeight;

   public Ship(double maxSpeed, int maxContainers, double maxWeight)
   {
      MaxSpeed = maxSpeed;
      MaxContainers = maxContainers;
      MaxWeight = maxWeight;
      Containers = new List<Container>();
   }

   public void LoadContainer(Container container)
   {
      if (Containers.Count == MaxContainers)
      {
         throw new OverflowException("Too many containers on the ship!");
      }

      double allWieight = Containers.Sum(c => c.CargoMass) + container.CargoMass;
      
      if (allWieight > MaxWeight)
      {
         throw new OverflowException("Too heavy load on the ship!");
      }

      if (Containers.FindIndex(c => c.Equals(container)) != -1)
      {
         throw new Exception("Tried to load the container onto the ship, which already exists!");
      }
      Containers.Add(container);
   }
   
   public void LoadContainers(List<Container> containers)
   {
      containers.ForEach(LoadContainer);
   }

   public Container RemoveContainer(string serialNumber)
   {
      Container container = GetContainer(serialNumber);
      Containers.Remove(container);
      return container;
   }

   public Container GetContainer(string serialNumber)
   {
      Container? container = Containers.Find(c => c.GetSerialNumber() == serialNumber);
      if (container == null)
      {
         throw new KeyNotFoundException("Can't restore container." +
                                        $"No container with the serial number {serialNumber} was found on the ship!");
      }
      return container;
   }

   public void ReplaceContainer(Container newContainer, string oldSerialNumber)
   {
      var oldContainer = RemoveContainer(oldSerialNumber);
      try
      {
         LoadContainer(newContainer);
      }
      catch (Exception)
      {
         LoadContainer(oldContainer);
         throw;
      }
   }

   public void TransferContainer(string serialNumber, Ship ship)
   {
      var container = RemoveContainer(serialNumber);
      try
      {
         ship.LoadContainer(container);
      }
      catch
      {
         LoadContainer(container);
         throw;
      }
   }

   public override string ToString()
   {
      var description = "";
      description += "Ship stats:\n";
      description += $"MaxSpeed: {MaxSpeed}\n";
      description += $"MaxContainers: {MaxContainers}\n";
      description += $"MaxWeight: {MaxWeight}\n";
      description += "Ship stores following containers:\n\n";
      Containers.ForEach(c => description += $"{c}\n");
      return description;
   }

   public List<string> GetContainersSerialNumbers()
   {
      return Containers.Select(c => c.GetSerialNumber()).ToList();
   }
}
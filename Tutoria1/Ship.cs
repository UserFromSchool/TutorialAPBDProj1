namespace Tutoria1;

public class Ship
{
    
   public List<Container> Containers = new List<Container>();
   public readonly double MaxSpeed;
   public readonly int MaxContainers;
   public readonly double MaxWeight;

   public Ship(double maxSpeed, int maxContainers, double maxWeight)
   {
      MaxSpeed = maxSpeed;
      MaxContainers = maxContainers;
      MaxWeight = maxWeight;
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
      
      Containers.Add(container);
   }
   
   public void LoadContainer(List<Container> containers)
   {
      containers.ForEach(c => LoadContainer(c));
   }
   

}
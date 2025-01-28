using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAOEF
{
    public class DAOSqlite : DbContext, Interfaces.IDAO
    {
        public DbSet<BO.Producer> Producers { get; set; }
        public DbSet<BO.Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlite(@"Filename=N:\Programing\Wizualne\CarApp2\DAOEF\carsdb8.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BO.Car>()
                .HasOne(c => c.Producer)
                .WithMany(p => p.Cars)
                .HasForeignKey(c => c.ProducerId)
                .IsRequired();
        }
        public void AddCar(ICar car)
        {
            BO.Car c = car as BO.Car;
            Cars.Add(c);
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            Producers.Add(p);
        }

        public ICar CreateNewCar()
        {
            return new BO.Car();
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IEnumerable<ICar> GetAllCars()
        {
            var cars = Cars.Include("Producer").ToList();
            return Cars;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public void RemoveCar(ICar car)
        {
            //throw new NotImplementedException();
            Cars.Remove(car as BO.Car);
        }

        public void RemoveProducer(IProducer producer)
        {
            //throw new NotImplementedException();
            Producers.Remove(producer as BO.Producer);
        }

        public void UpdateCar(ICar car)
        {
            throw new NotImplementedException();
        }

        void IDAO.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}
